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
        private string m_ClientId;
        private bool m_IsServerConnectionEstablished = false;
        private TaskScheduler m_TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private IBesiegedServer m_BesiegedServer;

        public MainWindow()
        {
            InitializeComponent();
            GlobalVariables.GameWindow = cvsGameWindow;
            DrawGrid(10, 10); //Needs to be switched to state
            
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
                    Command message = m_Client.MessageQueue.Take();
                    ProcessMessage(message);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void ProcessMessage(Command command)
        {
            if (command is CommandConnectionSuccessful)
            {
                CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
                m_ClientId = commandConnectionSuccessful.ClientId;
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
                CommandNotifyGame commandNotifyGame = command as CommandNotifyGame;
                CommandNotifyGame game = GlobalVariables.GameLobbyCollection.Where(x => x.GameId == commandNotifyGame.GameId).FirstOrDefault();
                if (game != null)
                {
                    GlobalVariables.GameLobbyCollection.Remove(game);
                }
                GlobalVariables.GameLobbyCollection.Add(commandNotifyGame);
            }
        }

        public void GoToMultiplayerLobby(object sender, EventArgs e)
        {
            int counter = 0;
            MessageBoxResult hr;
            while (!m_IsServerConnectionEstablished)
            {
                Thread.Sleep(10000); //sleep for 10 seconds before checking again.
                counter++;

                if (counter > 6) //spinning for 60 seconds, prompt user
                {
                    hr = MessageBox.Show("Timeout while attempting to connect to the server. Retry?", "Connection Timeout", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (hr == MessageBoxResult.Yes)
                    {
                        counter = 0;
                        hr = MessageBoxResult.None;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //render the lobby and attach the event handlers
        }

        public void DrawGrid(int rows, int columns)
        {
            cvsGameWindow.Width = ClientWindowOptions.WindowDimensions.Width;
            cvsGameWindow.Height = ClientWindowOptions.WindowDimensions.Height;
            if (!ClientWindowOptions.FullscreenMode)
            {
                Application.Current.MainWindow.Width = ClientWindowOptions.WindowDimensions.Width + 15;
                Application.Current.MainWindow.Height = ClientWindowOptions.WindowDimensions.Height + 38;
            }
            RenderMenu.RenderMainMenu(cvsGameWindow);
            //RenderGameWindow.RenderUI(cvsGameWindow);
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