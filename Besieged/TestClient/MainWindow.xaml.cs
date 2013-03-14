using Framework.Commands;
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
        private Client _client = new Client();
        private string _clientIdentifier;
        private bool _isServerConnectionEstablished = false;
        private TaskScheduler _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private IBesiegedServer _besiegedServer;

        public ObservableCollection<CommandNotifyGame> GameLobbyCollection { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            EndpointAddress endpointAddress = new EndpointAddress("http://localhost:31337/BesiegedServer/BesiegedMessage");
            DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(_client, new WSDualHttpBinding(), endpointAddress);
            _besiegedServer = duplexChannelFactory.CreateChannel();

            // Subscribe in a separate thread to preserve the UI thread
            Task.Factory.StartNew(() =>
            {
                CommandConnect commandConnect = new CommandConnect();
                _besiegedServer.SendCommand(commandConnect.ToXml());
            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command command = _client.MessageQueue.Take();
                    ProcessMessage(command);
                }
            }, TaskCreationOptions.LongRunning);

            GameLobbyCollection = new ObservableCollection<CommandNotifyGame>();
            DataContext = this;
        }

        public void ProcessMessage(Command command)
        {
            if (command is CommandConnectionSuccessful)
            {
                CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                _clientIdentifier = commandConnectionSuccessful.UniqueIdentifier;
                _isServerConnectionEstablished = true;
                //Task.Factory.StartNew(() =>
                //{
                //    MessageBox.Show("Connection successful!");
                //}, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
            }
            else if (command is CommandChatMessage)
            {
                Task.Factory.StartNew(() =>
                {
                    CommandChatMessage chatMessageCommand = command as CommandChatMessage;
                }, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
            }
            else if (command is CommandNotifyGame)
            {
                Task.Factory.StartNew(() =>
                {
                    CommandNotifyGame commandNotifyGame = command as CommandNotifyGame;
                    CommandNotifyGame game = GameLobbyCollection.Where(x => x.UniqueIdentifier == commandNotifyGame.UniqueIdentifier).FirstOrDefault(); 
                    if (game != null)
                    {
                        GameLobbyCollection.Remove(game);

                    }
                    GameLobbyCollection.Add(commandNotifyGame);
                }, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
            }
        }

        private void SendMessageToServer(string command)
        {
            Task.Factory.StartNew(() => _besiegedServer.SendCommand(command));
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lsvGameLobby.SelectedItem != null)
                {
                    CommandNotifyGame commandNotifyGame = lsvGameLobby.SelectedItem as CommandNotifyGame;
                    CommandJoinGame commandJoinGame = new CommandJoinGame(_clientIdentifier, commandNotifyGame.UniqueIdentifier);
                    SendMessageToServer(commandJoinGame.ToXml());
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }
    }
}
