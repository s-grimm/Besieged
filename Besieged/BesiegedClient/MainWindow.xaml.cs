﻿using Framework.Commands;
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
        private app_code.Client m_Client = new app_code.Client();

        public MainWindow()
        {
            InitializeComponent();
            GlobalResources.GameWindow = cvsGameWindow;
            //register close handlers
            this.Closing += (s, e) =>
                {
                    if (GlobalResources.m_IsServerConnectionEstablished)
                    {
                        CommandConnectionTerminated cmdConTerm = new CommandConnectionTerminated(GlobalResources.ClientId, GlobalResources.GameId);
                        GlobalResources.BesiegedServer.SendCommand(cmdConTerm.ToXml());
                    }
                };

            EndpointAddress endpointAddress = new EndpointAddress(String.Format("net.tcp://{0}:{1}/BesiegedServer/BesiegedMessage", ClientSettings.Default.ServerIP, ClientSettings.Default.ServerPort));
            DuplexChannelFactory<IBesiegedServer> duplexChannelFactory = new DuplexChannelFactory<IBesiegedServer>(m_Client, new NetTcpBinding(SecurityMode.None), endpointAddress);
            GlobalResources.BesiegedServer = duplexChannelFactory.CreateChannel();

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
                GlobalResources.m_IsServerConnectionEstablished = true;
                GlobalResources.currentMenuState = GlobalResources.MenuState.Menu;
            }

            else if (command is CommandChatMessage)
            {
                Task.Factory.StartNew(() =>
                {
                    CommandChatMessage chatMessageCommand = command as CommandChatMessage;
                    GlobalResources.GameSpecificChatMessages.Add(chatMessageCommand.Contents);
                }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
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
                }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            }
            
            else if (command is CommandJoinGameSuccessful)
            {
                Task.Factory.StartNew(() =>
                {
                    GlobalResources.GameSpecificChatMessages = new ObservableCollection<string>();
                    GlobalResources.GameId = command.GameId;
                    RenderPreGame.RenderPreGameLobby();
                }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
            }

            else if (command is CommandServerError)
            {
                CommandServerError commandServerError = command as CommandServerError;
                Task.Factory.StartNew(() =>
                {
                    MessageBox.Show(commandServerError.ErrorMessage);
                }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
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