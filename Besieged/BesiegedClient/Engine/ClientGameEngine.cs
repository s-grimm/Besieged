using BesiegedClient.Engine.Dialog;
using BesiegedClient.Engine.State;
using Framework.BesiegedMessages;
using Framework.ServiceContracts;
using Framework.Utilities;
using Framework.Utilities.Xml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BesiegedClient.Engine.State.InGameEngine;

namespace BesiegedClient.Engine
{
    public class ClientGameEngine
    {
        
        private IGameState m_CurrentGameState;
        private IGameState m_PreviousGameState;
        private static ClientGameEngine m_Instance = null;
        private Client m_ClientCallback = new Client();
        private IBesiegedServer m_BesiegedServer;
        private NetTcpBinding m_TcpBinding;
        private DuplexChannelFactory<IBesiegedServer> m_DuplexChannelFactory;
        private string m_ClientId;
        private string m_GameId;

        public MainWindow m_CurrentWindow { get; set; }
        public Dimensions ClientDimensions { get; set; }
        public Canvas Canvas { get; set; }
        public MonitoredValue<bool> IsServerConnected { get; set; }
        public MonitoredValue<bool> AllPlayersReady { get; set; }
        public ObservableCollection<GameInfoMessage> GamesCollection { get; set; }
        public ObservableCollection<PlayerInfoMessage> PlayerCollection { get; set; }
        public ObservableCollection<string> ChatMessageCollection { get; set; }
        public bool IsGameCreator { get; set; }
        public string ClientID { get { return m_ClientId; } }
        private ClientGameEngine() 
        {
            m_TcpBinding = new NetTcpBinding(SecurityMode.None,true)
                {
                    ReliableSession = { InactivityTimeout = new TimeSpan(0, 2, 0) },
                    SendTimeout = new TimeSpan(0, 2, 0),
                    ReceiveTimeout = new TimeSpan(0, 2, 0),
                    OpenTimeout = new TimeSpan(0, 1, 0),
                    CloseTimeout = new TimeSpan(0, 1, 0),
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas =
                    {
                        MaxArrayLength = 2147483647,
                        MaxBytesPerRead = 2147483647,
                        MaxStringContentLength = 2147483647,
                        MaxDepth = 2147483647,
                    },
                };

            GamesCollection = new ObservableCollection<GameInfoMessage>();
            PlayerCollection = new ObservableCollection<PlayerInfoMessage>();
            ChatMessageCollection = new ObservableCollection<string>();

            AllPlayersReady = new MonitoredValue<bool>(false);
            IsServerConnected = new MonitoredValue<bool>(false);
            IsServerConnected.ValueChanged += (from, to) =>
            {
                if (!to)
                {
                    Action postRender = () => RenderMessageDialog.RenderMessage("Unable to establish connection to server");
                    ChangeState(m_PreviousGameState, postRender);
                }
                else
                {
                    Task.Factory.StartNew(() => ChangeState(MultiplayerMenuState.Get()), CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
                }
            };

            EndpointAddress endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/BesiegedServer/BesiegedMessage", ClientSettings.Default.ServerIP, ClientSettings.Default.ServerPort));
            m_DuplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_ClientCallback, m_TcpBinding, endpointAddress);
            m_BesiegedServer = m_DuplexChannelFactory.CreateChannel();

            m_DuplexChannelFactory.Faulted += (s, ev) => MessageBox.Show("Its faulted");

            var MessageSubject = new Subject<BesiegedMessage>();

            var messagePublisher = m_ClientCallback.MessageQueue
                     .GetConsumingEnumerable()
                     .ToObservable(TaskPoolScheduler.Default)
                     .Subscribe(MessageSubject);

            //var messagePublisher = observableMessages.Publish();

            // All generic client messages are handled here
            var genericServerMessageSubscriber = MessageSubject
                .Where(message => message is GenericClientMessage)
                .Subscribe(message =>
                {
                    var genericMessage = message as GenericClientMessage;
                    switch (genericMessage.MessageEnum)
                    {
                        case ClientMessage.ClientMessageEnum.ConnectSuccessful:
                            m_ClientId = genericMessage.ClientId;
                            IsServerConnected.Value = true;
                            break;
                        case ClientMessage.ClientMessageEnum.AllPlayersReady:
                            AllPlayersReady.Value = true;
                            break;
                        case ClientMessage.ClientMessageEnum.PlayerNotReady:
                            AllPlayersReady.Value = false;
                            break;

                        case ClientMessage.ClientMessageEnum.StartGame:
                            ChangeState(PlayingGameState.Get());
                            break;
                        case ClientMessage.ClientMessageEnum.GameDisbanded:
                            if (IsGameCreator)
                            {
                                ChangeState(MultiplayerMenuState.Get());
                            }
                            else
                            {
                                Action disbandedAction = () =>
                                {
                                    RenderMessageDialog.RenderMessage("The game creator has disbanded the game");
                                };
                                ChangeState(MultiplayerMenuState.Get(), disbandedAction);
                            }
                            IsGameCreator = false;
                            ResetLobby();
                            break;
                        case ClientMessage.ClientMessageEnum.GameNotFound:
                            Action notFoundAction = () =>
                            {
                                RenderMessageDialog.RenderMessage("The game you are trying to reach was not found");
                            };
                            ChangeState(MultiplayerMenuState.Get(), notFoundAction);
                            break;
                        case ClientMessage.ClientMessageEnum.RemoveGame:
                            Action removeGameAction = () =>
                            {
                                var game = GamesCollection.FirstOrDefault(x => x.GameId == message.GameId);
                                if (game != null)
                                {
                                    GamesCollection.Remove(game);
                                }
                            };
                            ExecuteOnUIThread(removeGameAction);
                            break;
                        case ClientMessage.ClientMessageEnum.RemovePlayer:
                            Action removePlayerAction = () =>
                            {
                                var player = PlayerCollection.FirstOrDefault(x => x.ClientId == message.ClientId);
                                if (player != null)
                                {
                                    PlayerCollection.Remove(player);
                                }
                            };
                            ExecuteOnUIThread(removePlayerAction);
                            break;
                        case ClientMessage.ClientMessageEnum.TransitionToLoadingState:
                            Action loadingAction = () => ClientGameEngine.Get().ChangeState(LoadingState.Get());
                            ExecuteOnUIThread(loadingAction);
                            break;
                        case ClientMessage.ClientMessageEnum.TransitionToMultiplayerMenuState:
                            IsGameCreator = false;
                            ResetLobby();
                            Action multiplayerAction = () => ClientGameEngine.Get().ChangeState(MultiplayerMenuState.Get());
                            ExecuteOnUIThread(multiplayerAction);
                            break;
                        case ClientMessage.ClientMessageEnum.ActiveTurn:
                            InGameEngine.Get().ActivateTurn();
                            break;
                        case ClientMessage.ClientMessageEnum.WaitingForTurn:
                            InGameEngine.Get().DeActivateTurn();
                            break;
                        default:
                            throw new Exception("Unhandled GenericClientMessage was received: " + genericMessage.MessageEnum.ToString());
                    }
                });

            // All other server messages are handled here
            var m_ServerMessageSubscriber = MessageSubject
                .Where(message => message is ClientMessage && !(message is GenericClientMessage))
                .Subscribe(message =>
                {
                    if (message is ClientChatMessage)
                    {
                        Action action = () => ChatMessageCollection.Add((message as ClientChatMessage).Contents);
                        ExecuteOnUIThread(action);
                    }

                    else if (message is PlayerInfoMessage)
                    {
                        PlayerInfoMessage player = PlayerCollection.Where(x => x.ClientId == (message as PlayerInfoMessage).ClientId).FirstOrDefault();
                        Action action = () =>
                        {
                            if (player != null)
                            {
                                PlayerCollection.Remove(player);
                            }
                            PlayerCollection.Add(message as PlayerInfoMessage);
                        };
                        ExecuteOnUIThread(action);
                    }

                    else if (message is PlayerGameInfoMessage)
                    {
                        m_GameId = (message as PlayerGameInfoMessage).GameId;
                        if ((message as PlayerGameInfoMessage).IsCreator)
                        {
                            IsGameCreator = true;
                        }
                        ChangeState(PregameLobbyState.Get());
                    }

                    else if (message is GameInfoMessage)
                    {
                        GameInfoMessage game = GamesCollection.Where(x => x.GameId == (message as GameInfoMessage).GameId).FirstOrDefault();
                        Action action = () =>
                        {
                            if (game != null)
                            {
                                GamesCollection.Remove(game);
                            }
                            GamesCollection.Add(message as GameInfoMessage);
                        };
                        ExecuteOnUIThread(action);
                    }

                    else if (message is ErrorDialogMessage)
                    {
                        Action action = () =>
                        {
                            RenderMessageDialog.RenderMessage((message as ErrorDialogMessage).Contents);
                        };
                        ChangeState(m_PreviousGameState, action);
                        ResetLobby();
                    }
                    else if (message is ClientGameStateMessage)
                    {
                        ClientGameStateMessage mem = message as ClientGameStateMessage;
                        Action action = () =>
                        {
                            if (mem.State != null)
                            {
                                InGameEngine.Get().Board = mem.State;
                            }
                        };
                        ExecuteOnUIThread(action);
                    }
                }); 
        }
        
        public static ClientGameEngine Get()
        {
            if (m_Instance == null)
            {
                m_Instance = new ClientGameEngine();
            }
            return m_Instance;
        }

        public void SetGameCanvas(Canvas gameCanvas)
        {
            Canvas = gameCanvas;
            ClientDimensions = new Dimensions()
            {
                Height = (int)Canvas.Height,
                Width = (int)Canvas.Width
            };
        }
       
        public void ChangeState(IGameState gameState)
        {
            Task.Factory.StartNew(() =>
            {
                Canvas.Children.Clear();
                m_PreviousGameState = m_CurrentGameState;
                m_CurrentGameState = gameState;
                m_CurrentGameState.Render();
            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        public void ChangeState(IGameState gameState, Action postRender)
        {
            Task.Factory.StartNew(() =>
            {
                Canvas.Children.Clear();
                m_PreviousGameState = m_CurrentGameState;
                m_CurrentGameState = gameState;
                m_CurrentGameState.Render();
                postRender.Invoke();
            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        public void ExecuteOnUIThread(Action action)
        {
            Task.Factory.StartNew(action.Invoke, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        public void SendMessageToServer(BesiegedMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                message.ClientId = m_ClientId;
                message.GameId = message.GameId ?? m_GameId;
                string serializedCommand = message.ToXml();
                try
                {
                    m_BesiegedServer.SendMessage(serializedCommand);
                }
                catch (Exception)
                {
                    IsServerConnected.Value = false;
                }
            });
        }

        public void ResetLobby()
        {
            Action clear = () =>
            {
                PlayerCollection.Clear();
                ChatMessageCollection.Clear();
            };
            ExecuteOnUIThread(clear);
        }
    }
}
