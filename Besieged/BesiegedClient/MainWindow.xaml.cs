using Framework.Commands;
using Framework.ServiceContracts;
using Framework.Utilities.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using BesiegedClient.Rendering;

namespace BesiegedClient
{
    public class Dimensions
    {
        public int Width { get; set; }

        public int Height { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush _blackBrush = new SolidColorBrush(Colors.Black);
        private SolidColorBrush _redBrush = new SolidColorBrush(Colors.Red);
        private SolidColorBrush _greenBrush = new SolidColorBrush(Colors.Green);
        private SolidColorBrush _blueBrush = new SolidColorBrush(Colors.Blue);
        private List<Rectangle> _rectangles = new List<Rectangle>();

        private app_code.Client m_Client = new app_code.Client();
        private bool m_IsServerConnectionEstablished = false;
        private TaskScheduler m_TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();


        public MainWindow()
        {
            InitializeComponent();
            GlobalResources.GameWindow = cvsGameWindow;
            
            EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:31337/BesiegedServer/BesiegedMessage");
            DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_Client, new NetTcpBinding(SecurityMode.None), endpointAddress);
            GlobalResources.BesiegedServer = duplexChannelFactory.CreateChannel();

            // Subscribe in a separate thread to preserve the UI thread
            Task.Factory.StartNew(() =>
            {
                CommandConnect commandConnect = new CommandConnect(ClientSettings.Default.Alias);
                GlobalResources.BesiegedServer.SendCommand(commandConnect.ToXml());
            });

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Command message = m_Client.MessageQueue.Take();
                    ProcessMessage(message);
                }
            }, TaskCreationOptions.LongRunning);

            cvsGameWindow.Width = ClientSettings.Default.Width;
            cvsGameWindow.Height = ClientSettings.Default.Height;
            if (!ClientSettings.Default.Fullscreen)
            {
                Application.Current.MainWindow.Width = ClientSettings.Default.Width + 15;
                Application.Current.MainWindow.Height = ClientSettings.Default.Height + 38;
            }
            RenderMenu.RenderMainMenu();
        }

        public void ProcessMessage(Command command)
        {
            if (command is CommandConnectionSuccessful)
            {
                CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                GlobalResources.ClientId = commandConnectionSuccessful.ClientId;
                m_IsServerConnectionEstablished = true;
                Task.Factory.StartNew(() =>
                {
                    MessageBox.Show("Connection successful!");
                }, CancellationToken.None, TaskCreationOptions.None, m_TaskScheduler);
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
                    CommandNotifyGame game = GlobalResources.GameLobbyCollection.Where(x => x.GameId == commandNotifyGame.GameId).FirstOrDefault();
                    if (game != null)
                    {
                        GlobalResources.GameLobbyCollection.Remove(game);
                    }
                    GlobalResources.GameLobbyCollection.Add(commandNotifyGame);
                }, CancellationToken.None, TaskCreationOptions.None, m_TaskScheduler);
            }
            else if (command is CommandJoinGameSuccessful)
            {
                Task.Factory.StartNew(() =>
                {
                    RenderPreGame.RenderPreGameLobby();
                }, CancellationToken.None, TaskCreationOptions.None, m_TaskScheduler);
                
            }
        }

        private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            Application.Current.MainWindow.Title = Application.Current.MainWindow.Width.ToString() + " : " + Application.Current.MainWindow.Height.ToString();
        }
    }
}

/*Code Snippets for shane to deal with maximizing window to fullscreen mode

1. Hook WinProc to catch WM_SYSCOMMAND

2. Check wParam == SC_MAXIMIZE and then

3. Set my windiw's attributes

this.ResizeMode = ResizeMode.NoResize;

this.WindowStyle = WindowStyle.None;

this.WindowState = WindowState.Maximized;

*/

/*Code snippit for drawing grid

            ////init rectangles
            //for (int i = 0; i < windowDimensions.Width; i += 50)
            //{
            //    for (int y = 0; y < windowDimensions.Height; y += 50)
            //    {
            //        Rectangle rect = new Rectangle(); //create the rectangle
            //        rect.StrokeThickness = 1;  //border to 1 stroke thick
            //        rect.Stroke = _blackBrush; //border color to black
            //        rect.Width = 50;
            //        rect.Height = 50;
            //        rect.Name = "box" + i.ToString();
            //        Canvas.SetLeft(rect, i);
            //        Canvas.SetTop(rect, y);
            //        _rectangles.Add(rect);
            //    }
            //}
            //foreach (var rect in _rectangles)
            //{
            //    cvsGameWindow.Children.Add(rect);
            //}

*/