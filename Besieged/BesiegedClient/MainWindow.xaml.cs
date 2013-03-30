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
using System.Collections.ObjectModel;
using BesiegedClient.Engine;
using BesiegedClient.Engine.State;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Threading;

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
        //private app_code.Client m_Client = new app_code.Client();

        public MainWindow()
        {
            InitializeComponent();
            // fire up the xml core
            Task.Factory.StartNew(() => XmlCore.Start());
            // kickstart the engine singleton and pass it the game canvas
            cvsGameWindow.Width = ClientSettings.Default.Width;
            cvsGameWindow.Height = ClientSettings.Default.Height;
            if (!ClientSettings.Default.Fullscreen)
            {
                Application.Current.MainWindow.Width = ClientSettings.Default.Width + 15;
                Application.Current.MainWindow.Height = ClientSettings.Default.Height + 38;
            }


            BitmapImage logo = new BitmapImage(new Uri("resources/Logo.png", UriKind.RelativeOrAbsolute));
            cvsGameWindow.Background = new SolidColorBrush(Colors.Black);
            Image loadingImage = new Image();
            loadingImage.Source = logo;
            loadingImage.Width = logo.PixelWidth;
            loadingImage.Height = logo.PixelHeight;
            Canvas.SetLeft(loadingImage, cvsGameWindow.Width / 2 - logo.PixelWidth / 2);
            Canvas.SetTop(loadingImage, cvsGameWindow.Height / 2 - logo.Height / 2);
            cvsGameWindow.Children.Add(loadingImage);

            ClientGameEngine.Get().SetGameCanvas(cvsGameWindow);

            DispatcherTimer dtimer = new DispatcherTimer();
            dtimer.Tick += (s, e) => {
                dtimer.Stop();
                ClientGameEngine.Get().ChangeState(MainMenuState.Get());
            };

            dtimer.Interval = new TimeSpan(0, 0, 4);
            dtimer.Start();

            //register close handlers
            //    this.Closing += (s, e) =>
            //        {
            //            if (GlobalResources.m_IsServerConnectionEstablished)
            //            {
            //                try
            //                {
            //                    CommandConnectionTerminated cmdConTerm = new CommandConnectionTerminated(GlobalResources.ClientId, GlobalResources.GameId);
            //                    GlobalResources.BesiegedServer.Sendg Command(cmdConTerm.ToXml());
            //                }
            //                catch (Exception) { }
            //            }
            //        };

            //    

            //    cvsGameWindow.Width = ClientSettings.Default.Width;
            //    cvsGameWindow.Height = ClientSettings.Default.Height;
            //    if (!ClientSettings.Default.Fullscreen)
            //    {
            //        Application.Current.MainWindow.Width = ClientSettings.Default.Width + 15;
            //        Application.Current.MainWindow.Height = ClientSettings.Default.Height + 38;
            //    }
            //    RenderMenu.RenderMainMenu();
            //}

            //public void ProcessMessage(Command command)
            //{
            //    if (command is CommandConnectionSuccessful)
            //    {
            //        CommandConnectionSuccessful commandConnectionSuccessful = command as CommandConnectionSuccessful;
            //        GlobalResources.ClientId = commandConnectionSuccessful.ClientId;
            //        GlobalResources.m_IsServerConnectionEstablished = true;
            //        GlobalResources.currentMenuState = GlobalResources.MenuState.Menu;
            //    }

            //    else if (command is CommandChatMessage)
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            CommandChatMessage chatMessageCommand = command as CommandChatMessage;
            //            GlobalResources.GameSpecificChatMessages.Add(chatMessageCommand.Contents);
            //        }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            //    }

            //    else if (command is CommandNotifyGame)
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            CommandNotifyGame commandNotifyGame = command as CommandNotifyGame;
            //            CommandNotifyGame game = GlobalResources.GameLobbyCollection.Where(x => x.GameId == commandNotifyGame.GameId).FirstOrDefault();
            //            if (game != null)
            //            {
            //                GlobalResources.GameLobbyCollection.Remove(game);
            //            }
            //            GlobalResources.GameLobbyCollection.Add(commandNotifyGame);
            //        }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            //    }

            //    else if (command is CommandJoinGameSuccessful)
            //    {
            //        Task.Factory.StartNew(() =>
            //        {
            //            GlobalResources.GameSpecificChatMessages = new ObservableCollection<string>();
            //            GlobalResources.GameId = command.GameId;
            //            RenderPreGame.RenderPreGameLobby();
            //        }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            //    }

            //    else if (command is CommandServerError)
            //    {
            //        CommandServerError commandServerError = command as CommandServerError;
            //        Task.Factory.StartNew(() =>
            //        {
            //            RenderMessageDialog.RenderMessage(commandServerError.ErrorMessage);
            //        }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            //    }
            //}

            //private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
            //{
            //    Application.Current.MainWindow.Title = Application.Current.MainWindow.Width.ToString() + " : " + Application.Current.MainWindow.Height.ToString();
            //}
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