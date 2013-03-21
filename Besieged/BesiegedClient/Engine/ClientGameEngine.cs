using BesiegedClient.Engine.State;
using BesiegedClient.Rendering;
using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public Dimensions ClientDimensions { get; set; }
        public Canvas Canvas { get; set; }
        public MonitoredValue<bool> IsServerConnected { get; set; }
        public ObservableCollection<CommandNotifyGame> CurrentGameCollection { get; set; }

        private ClientGameEngine() 
        {
            m_TcpBinding = new NetTcpBinding(SecurityMode.None);
            m_TcpBinding.OpenTimeout = new TimeSpan(0, 0, 10);
            m_TcpBinding.CloseTimeout = new TimeSpan(0, 0, 10);
            m_TcpBinding.SendTimeout = new TimeSpan(0, 0, 10);
            m_TcpBinding.ReceiveTimeout = new TimeSpan(0, 0, 10);

            CurrentGameCollection = new ObservableCollection<CommandNotifyGame>();
            
            IsServerConnected = new MonitoredValue<bool>(true);
            IsServerConnected.ValueChanged += (from, to) =>
            {
                if (!to)
                {
                    Action postRender = () =>
                    {
                        RenderMessageDialog.RenderMessage("Unable to establish connection to server");
                    };
                    ChangeState(m_PreviousGameState, postRender);
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        ChangeState(MultiplayerMenuState.Get());
                    }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
                }
            };

            EndpointAddress endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/BesiegedServer/BesiegedMessage", ClientSettings.Default.ServerIP, ClientSettings.Default.ServerPort));
            m_DuplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_ClientCallback, m_TcpBinding, endpointAddress);
            m_BesiegedServer = m_DuplexChannelFactory.CreateChannel();

            m_DuplexChannelFactory.Faulted += (s, ev) =>
            {
                MessageBox.Show("Its faulted");
            };

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = m_ClientCallback.MessageQueue.Take();
                    ProcessMessage(command);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void ProcessMessage(Command command)
        {
            if (command is CommandConnectionSuccessful)
            {
                CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                m_ClientId = commandConnectionSuccessful.ClientId;
                IsServerConnected.Value = true;
            }
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

        public void SendMessageToServer(string command)
        {
            //Task.Factory.StartNew(() =>
            //{
                try
                {
                    m_BesiegedServer.SendCommand(command);
                }
                catch (Exception)
                {
                    IsServerConnected.Value = false;
                }
            //});
        }
    }
}
