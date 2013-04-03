using Framework.BesiegedMessages;
using Framework.Utilities.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class PregameLobbyState : IGameState
    {
        private static PregameLobbyState m_Instance;
        private double m_MenuYOffset;
        private double m_MenuXOffset;
        private object m_MouseDownSender;

        private PregameLobbyState() { }

        private Button m_SendButton;
        private Image m_LeaveEnabledImage;
        private Image m_LeaveDisabledImage;
        private Image m_ReadyImage;
        private Image m_NotReadyImage;
        private Image m_StartEnabledImage;
        private Image m_StartDisabledImage;
        private TextBox m_ChatMessageBox;
        private ListView m_PlayerListView;
        private GridView m_PlayerGridView;
        private GridViewColumn m_NameColumn;
        private GridViewColumn m_ReadyColumn;
        private ListBox m_ChatMessagesListBox;

        private ImageBrush m_BackgroundBrush;
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new PregameLobbyState();
                    Action action = () => m_Instance.Initialize();
                    ClientGameEngine.Get().ExecuteOnUIThread(action);
                }
                return m_Instance;
            }
            catch (Exception ex)
            {
                // error handling
                throw ex;
            }
        }

        #region Handlers

        public void MenuOptionHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, m_MenuXOffset + 50);
            }
            catch (Exception)
            {
            }
        }

        public void MenuOptionHoverLost(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, m_MenuXOffset);
            }
            catch (Exception)
            {
            }
        }

        public void MenuOptionMouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                m_MouseDownSender = sender;
            }
            catch (Exception)
            {
            }
        }

        public void MenuOptionMouseUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (m_MouseDownSender != sender) return;
                Image img = sender as Image;
                string selected = img.Name;

                if (selected == "NotReady")
                {
                    GenericGameMessage notReady = new GenericGameMessage() { MessageEnum = GameMessage.GameMessageEnum.PlayerNotReady };
                    ClientGameEngine.Get().SendMessageToServer(notReady);

                    m_ReadyImage.Visibility = Visibility.Visible;
                    m_ReadyImage.IsEnabled = true;
                    m_NotReadyImage.Visibility = Visibility.Hidden;
                    m_NotReadyImage.IsEnabled = false;
                }

                else if (selected == "Ready")
                {
                    GenericGameMessage ready = new GenericGameMessage() { MessageEnum = GameMessage.GameMessageEnum.PlayerReady };
                    ClientGameEngine.Get().SendMessageToServer(ready);

                    m_ReadyImage.Visibility = Visibility.Hidden;
                    m_ReadyImage.IsEnabled = false;
                    m_NotReadyImage.Visibility = Visibility.Visible;
                    m_NotReadyImage.IsEnabled = true;
                }

                else if (selected == "StartEnabled")
                {
                    GenericGameMessage start = new GenericGameMessage() { MessageEnum = GameMessage.GameMessageEnum.Start };
                    ClientGameEngine.Get().SendMessageToServer(start);
                }

                //else if (selected == "JoinGame")
                //{
                //    if (m_SelectedGame == null)
                //    {
                //        RenderMessageDialog.RenderMessage("You need to select a game to join!");
                //    }
                //    else
                //    {
                //        if (m_SelectedGame.HasPassword)
                //        {
                //            RenderMessageDialog.RenderInput("Please enter the password: ", (se, ev) =>
                //            {
                //                if (se != null)
                //                {
                //                    CommandJoinGame commandJoinGame = new CommandJoinGame(m_SelectedGame.GameId, se as string);
                //                    ClientGameEngine.Get().SendMessageToServer(commandJoinGame);
                //                }
                //            });
                //        }
                //        else
                //        {
                //            CommandJoinGame commandJoinGame = new CommandJoinGame(m_SelectedGame.GameId, string.Empty);
                //            ClientGameEngine.Get().SendMessageToServer(commandJoinGame);
                //        }
                //    }
                //}
                //else if (selected == "MainMenu")
                //{
                //    ClientGameEngine.Get().ChangeState(MainMenuState.Get());
                //}
            }
            catch (Exception)
            {
            }
        }

        #endregion Handlers

        public void Initialize()
        {
            try
            {
                double aspectRatio = Math.Round((double)ClientGameEngine.Get().ClientDimensions.Width / (double)ClientGameEngine.Get().ClientDimensions.Height, 2, MidpointRounding.AwayFromZero);

                string UIComponentPath = "resources\\UI\\PreGameLobby\\";
                string ratioPath = string.Empty;

                if (aspectRatio == 1.33)
                    ratioPath = "4x3\\";
                else
                    ratioPath = "16x9\\";

                //background
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + ratioPath + "Background.png", UriKind.RelativeOrAbsolute));
                m_BackgroundBrush = new ImageBrush(bitmapImage);

                ClientGameEngine.Get().AllPlayersReady.ValueChanged += (from, to) =>
                {
                    if (to)
                    {
                        Action action = () =>
                        {
                            m_StartEnabledImage.Visibility = Visibility.Visible;
                            m_StartEnabledImage.IsEnabled = true;
                            m_StartDisabledImage.Visibility = Visibility.Hidden;
                        };
                        ClientGameEngine.Get().ExecuteOnUIThread(action);
                    }
                    else
                    {
                        Action action = () =>
                        {
                            m_StartDisabledImage.Visibility = Visibility.Visible;
                            m_StartEnabledImage.IsEnabled = false;
                            m_StartEnabledImage.Visibility = Visibility.Hidden;
                        };
                        ClientGameEngine.Get().ExecuteOnUIThread(action);
                    }
                };

                //Chat Text Box
                m_ChatMessageBox = new TextBox();
                m_ChatMessageBox.Opacity = 0.75;
                m_ChatMessageBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessageBox.FontSize = 18;
                m_ChatMessageBox.KeyDown += (s, ev) =>
                {
                    if (ev.Key == Key.Enter)
                    {
                        if (m_ChatMessageBox.Text.Trim() != string.Empty)
                        {
                            GameChatMessage chat = new GameChatMessage() { Contents = m_ChatMessageBox.Text.Trim() };
                            m_ChatMessageBox.Text = "";
                            ClientGameEngine.Get().SendMessageToServer(chat);
                        }
                    }
                };

                //Chat Message OK
                m_SendButton = new Button();
                m_SendButton.FontFamily = new FontFamily("Papyrus");
                m_SendButton.FontSize = 18;
                m_SendButton.Content = "Send";
                m_SendButton.Click += (s, ev) =>
                {
                    if (m_ChatMessageBox.Text.Trim() != string.Empty)
                    {
                        GameChatMessage chat = new GameChatMessage() { Contents = m_ChatMessageBox.Text.Trim() };
                        m_ChatMessageBox.Text = "";
                        ClientGameEngine.Get().SendMessageToServer(chat);
                    }
                };
                
                //Chat messages
                m_ChatMessagesListBox = new ListBox();
                m_ChatMessagesListBox.ItemsSource = ClientGameEngine.Get().ChatMessageCollection;
                m_ChatMessagesListBox.Opacity = 0.75;
                m_ChatMessagesListBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessagesListBox.FontSize = 14;
                ClientGameEngine.Get().ChatMessageCollection.CollectionChanged += (s, ev) =>
                {
                    m_ChatMessagesListBox.SelectedIndex = m_ChatMessagesListBox.Items.Count - 1;
                };

                //StartEnabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Start.png", UriKind.RelativeOrAbsolute));
                m_StartEnabledImage = new Image();
                m_StartEnabledImage.Source = bitmapImage;
                m_StartEnabledImage.Width = bitmapImage.PixelWidth;
                m_StartEnabledImage.Height = bitmapImage.PixelHeight;
                m_StartEnabledImage.Visibility = Visibility.Hidden;
                m_StartEnabledImage.IsEnabled = false;
                m_StartEnabledImage.Name = "StartEnabled";
                m_StartEnabledImage.MouseEnter += MenuOptionHover;
                m_StartEnabledImage.MouseLeave += MenuOptionHoverLost;
                m_StartEnabledImage.MouseDown += MenuOptionMouseDown;
                m_StartEnabledImage.MouseUp += MenuOptionMouseUp;

                //StartDisabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "StartFade.png", UriKind.RelativeOrAbsolute));
                m_StartDisabledImage = new Image();
                m_StartDisabledImage.Source = bitmapImage;
                m_StartDisabledImage.Width = bitmapImage.PixelWidth;
                m_StartDisabledImage.Height = bitmapImage.PixelHeight;
                m_StartDisabledImage.Visibility = Visibility.Visible;
                m_StartDisabledImage.Name = "StartDisabled";
                //m_StartDisabledImage.MouseEnter += MenuOptionHover;
                //m_StartDisabledImage.MouseLeave += MenuOptionHoverLost;
                //m_StartDisabledImage.MouseDown += MenuOptionMouseDown;
                //m_StartDisabledImage.MouseUp += MenuOptionMouseUp;

                //LeaveEnabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Leave.png", UriKind.RelativeOrAbsolute));
                m_LeaveEnabledImage = new Image();
                m_LeaveEnabledImage.Source = bitmapImage;
                m_LeaveEnabledImage.Width = bitmapImage.PixelWidth;
                m_LeaveEnabledImage.Height = bitmapImage.PixelHeight;
                m_LeaveEnabledImage.Visibility = System.Windows.Visibility.Visible;
                m_LeaveEnabledImage.Name = "LeaveEnabled";
                //m_LeaveEnabledImage.MouseEnter += MenuOptionHover;
                //m_LeaveEnabledImage.MouseLeave += MenuOptionHoverLost;
                //m_LeaveEnabledImage.MouseDown += MenuOptionMouseDown;
                //m_LeaveEnabledImage.MouseUp += MenuOptionMouseUp;

                //LeaveDisabled
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "LeaveFade.png", UriKind.RelativeOrAbsolute));
                m_LeaveDisabledImage = new Image();
                m_LeaveDisabledImage.Source = bitmapImage;
                m_LeaveDisabledImage.Width = bitmapImage.PixelWidth;
                m_LeaveDisabledImage.Height = bitmapImage.PixelHeight;
                m_LeaveDisabledImage.Visibility = System.Windows.Visibility.Hidden;
                m_LeaveDisabledImage.Name = "LeaveDisabled";
                //m_LeaveDisabledImage.MouseEnter += MenuOptionHover;
                //m_LeaveDisabledImage.MouseLeave += MenuOptionHoverLost;
                //m_LeaveDisabledImage.MouseDown += MenuOptionMouseDown;
                //m_LeaveDisabledImage.MouseUp += MenuOptionMouseUp;

                //Ready
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "Ready.png", UriKind.RelativeOrAbsolute));
                m_ReadyImage = new Image();
                m_ReadyImage.Source = bitmapImage;
                m_ReadyImage.Width = bitmapImage.PixelWidth;
                m_ReadyImage.Height = bitmapImage.PixelHeight;
                m_ReadyImage.Visibility = System.Windows.Visibility.Visible;
                m_ReadyImage.Name = "Ready";
                m_ReadyImage.MouseEnter += MenuOptionHover;
                m_ReadyImage.MouseLeave += MenuOptionHoverLost;
                m_ReadyImage.MouseDown += MenuOptionMouseDown;
                m_ReadyImage.MouseUp += MenuOptionMouseUp;

                //NotReady
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "NotReady.png", UriKind.RelativeOrAbsolute));
                m_NotReadyImage = new Image();
                m_NotReadyImage.Source = bitmapImage;
                m_NotReadyImage.Width = bitmapImage.PixelWidth;
                m_NotReadyImage.Height = bitmapImage.PixelHeight;
                m_NotReadyImage.Visibility = System.Windows.Visibility.Hidden;
                m_NotReadyImage.Name = "NotReady";
                m_NotReadyImage.MouseEnter += MenuOptionHover;
                m_NotReadyImage.MouseLeave += MenuOptionHoverLost;
                m_NotReadyImage.MouseDown += MenuOptionMouseDown;
                m_NotReadyImage.MouseUp += MenuOptionMouseUp;

                //Player list
                m_PlayerListView = new ListView();
                m_PlayerListView.Opacity = 0.75;

                m_PlayerGridView = new GridView();
                m_PlayerGridView.AllowsColumnReorder = false;

                m_ReadyColumn = new GridViewColumn();
                m_ReadyColumn.DisplayMemberBinding = new Binding("IsReady");
                m_ReadyColumn.Header = "Ready";
                m_PlayerGridView.Columns.Add(m_ReadyColumn);

                m_NameColumn = new GridViewColumn();
                m_NameColumn.DisplayMemberBinding = new Binding("Name");
                m_NameColumn.Header = "Name";
                m_PlayerGridView.Columns.Add(m_NameColumn);

                m_PlayerListView.View = m_PlayerGridView;
                m_PlayerListView.ItemsSource = ClientGameEngine.Get().PlayerCollection;


                // Chat messages
                m_ChatMessagesListBox.Opacity = 0.75;
                m_ChatMessagesListBox.FontFamily = new FontFamily("Papyrus");
                m_ChatMessagesListBox.FontSize = 14;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Render()
        {
            //dimensions and offsets
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            m_MenuYOffset = dimensions.Height * 0.85;
            m_MenuXOffset = dimensions.Width * 0.15;

            //chat text box
            Canvas.SetLeft(m_ChatMessageBox, dimensions.Width * 0.075);
            Canvas.SetBottom(m_ChatMessageBox, dimensions.Height * 0.025);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessageBox);
            m_ChatMessageBox.Width = dimensions.Width * 0.74;
            m_ChatMessageBox.Height = dimensions.Height * 0.05;

            //chat send button
            Canvas.SetBottom(m_SendButton, dimensions.Height * 0.025);
            Canvas.SetLeft(m_SendButton, dimensions.Width * 0.8225);
            m_SendButton.Height = dimensions.Height * 0.05;
            m_SendButton.Width = dimensions.Width * 0.10;
            ClientGameEngine.Get().Canvas.Children.Add(m_SendButton);   

            //chat list box
            Canvas.SetLeft(m_ChatMessagesListBox, dimensions.Width * 0.075);
            Canvas.SetBottom(m_ChatMessagesListBox, dimensions.Height * 0.10);
            ClientGameEngine.Get().Canvas.Children.Add(m_ChatMessagesListBox);
            m_ChatMessagesListBox.Height = dimensions.Height * 0.25;
            m_ChatMessagesListBox.Width = dimensions.Width * 0.85;

            if (ClientGameEngine.Get().IsGameCreator)
            {
                //start game enabled
                Canvas.SetLeft(m_StartEnabledImage, m_MenuXOffset);
                Canvas.SetBottom(m_StartEnabledImage, m_MenuYOffset);
                Canvas.SetZIndex(m_StartEnabledImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_StartEnabledImage);

                //start game disabled
                Canvas.SetLeft(m_StartDisabledImage, m_MenuXOffset);
                Canvas.SetBottom(m_StartDisabledImage, m_MenuYOffset);
                Canvas.SetZIndex(m_StartDisabledImage, 100);
                ClientGameEngine.Get().Canvas.Children.Add(m_StartDisabledImage);
                m_MenuYOffset -= m_StartDisabledImage.Height * 1.5;
            }

            //leave game enabled
            Canvas.SetLeft(m_LeaveEnabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_LeaveEnabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_LeaveEnabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_LeaveEnabledImage);

            //leave game disabled
            Canvas.SetLeft(m_LeaveDisabledImage, m_MenuXOffset);
            Canvas.SetBottom(m_LeaveDisabledImage, m_MenuYOffset);
            Canvas.SetZIndex(m_LeaveDisabledImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_LeaveDisabledImage);
            m_MenuYOffset -= m_LeaveDisabledImage.Height * 1.5;

            //ready
            Canvas.SetLeft(m_ReadyImage, m_MenuXOffset);
            Canvas.SetBottom(m_ReadyImage, m_MenuYOffset);
            Canvas.SetZIndex(m_ReadyImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_ReadyImage);

            //not ready
            Canvas.SetLeft(m_NotReadyImage, m_MenuXOffset);
            Canvas.SetBottom(m_NotReadyImage, m_MenuYOffset);
            Canvas.SetZIndex(m_NotReadyImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_NotReadyImage);

            //player list view
            Canvas.SetLeft(m_PlayerListView, dimensions.Width * 0.60);
            Canvas.SetBottom(m_PlayerListView, dimensions.Height * 0.45);
            m_PlayerListView.Height = dimensions.Height * 0.50;
            m_PlayerListView.Width = dimensions.Width * 0.3225;
            m_NameColumn.Width = m_PlayerListView.Width * 0.70;
            m_ReadyColumn.Width = m_PlayerListView.Width * 0.28;
            ClientGameEngine.Get().Canvas.Children.Add(m_PlayerListView);
        }
    }
}
