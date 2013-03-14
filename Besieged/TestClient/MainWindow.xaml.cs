﻿using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client m_Client = new Client();
        private string m_ClientId;
        private bool m_IsServerConnectionEstablished = false;
        private TaskScheduler m_TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private IBesiegedServer m_BesiegedServer;

        public ObservableCollection<CommandNotifyGame> GameLobbyCollection { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
            DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_Client, new WSDualHttpBinding(), endpointAddress);
            m_BesiegedServer = duplexChannelFactory.CreateChannel();

            // Subscribe in a separate thread to preserve the UI thread
            Task.Factory.StartNew(() =>
            {
                CommandConnect commandConnect = new CommandConnect();
                m_BesiegedServer.SendCommand(commandConnect.ToXml());
            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = m_Client.MessageQueue.Take();
                    ProcessMessage(command);
                }
            }, TaskCreationOptions.LongRunning);

            GameLobbyCollection = new ObservableCollection<CommandNotifyGame>();
            DataContext = this;
        }

        public void ProcessMessage(Command command)
        {
            try
            {
                if (command is CommandConnectionSuccessful)
                {
                    CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                    m_ClientId = commandConnectionSuccessful.ClientId;
                    m_IsServerConnectionEstablished = true;
                }
                else if (command is CommandChatMessage)
                {
                    Task.Factory.StartNew(() =>
                    {
                        CommandChatMessage chatMessageCommand = command as CommandChatMessage;
                    }, CancellationToken.None, TaskCreationOptions.None, m_TaskScheduler);
                }
                else if (command is CommandNotifyGame)
                {
                    Task.Factory.StartNew(() =>
                    {
                        CommandNotifyGame commandNotifyGame = command as CommandNotifyGame;
                        CommandNotifyGame game = GameLobbyCollection.Where(x => x.GameId == commandNotifyGame.GameId).FirstOrDefault();
                        if (game != null)
                        {
                            GameLobbyCollection.Remove(game);

                        }
                        GameLobbyCollection.Add(commandNotifyGame);
                    }, CancellationToken.None, TaskCreationOptions.None, m_TaskScheduler);
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }

        private void SendMessageToServer(Command command)
        {
            command.ClientId = m_ClientId;
            Task.Factory.StartNew(() => m_BesiegedServer.SendCommand(command.ToXml()));
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lsvGameLobby.SelectedItem != null)
                {
                    CommandNotifyGame commandNotifyGame = lsvGameLobby.SelectedItem as CommandNotifyGame;
                    CommandJoinGame commandJoinGame = new CommandJoinGame(commandNotifyGame.GameId);
                    SendMessageToServer(commandJoinGame);
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtGameName.Text))
                {
                    MessageBox.Show("Specify a game name");
                }
                else
                {
                    CommandCreateGame commandCreateGame = new CommandCreateGame(txtGameName.Text, (int)sldMaxPlayers.Value);
                    SendMessageToServer(commandCreateGame);
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }
    }
}
