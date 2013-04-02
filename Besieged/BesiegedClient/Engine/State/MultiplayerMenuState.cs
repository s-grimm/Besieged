using BesiegedClient.Engine.Dialog;
using Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class MultiplayerMenuState : IGameState
    {
        private static MultiplayerMenuState m_Instance = null;
        private double m_MenuYOffset;
        private double m_MenuXOffset;
        private object m_MouseDownSender;
        private CommandNotifyGame m_SelectedGame;

        private ListView m_CurrentGameListView;
        private GridView m_GameGridView;
        private GridViewColumn m_NameColumn;
        private GridViewColumn m_CapacityColumn;
        private ImageBrush m_BackgroundBrush;
        private Image m_JoinGameImage;
        private Image m_CreateGameImage;
        private Image m_MainMenuImage;

        private MultiplayerMenuState() { }
        
        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new MultiplayerMenuState();
                    m_Instance.Initialize();
                }
                return m_Instance;
            }
            catch (Exception ex)
            {
                // we need client side error handling!
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
                if (selected == "CreateGame")
                {
                    ClientGameEngine.Get().ChangeState(NewGameState.Get());
                }
                else if (selected == "JoinGame")
                {
                    if (m_SelectedGame == null)
                    {
                        RenderMessageDialog.RenderMessage("You need to select a game to join!");
                    }
                    else
                    {
                        if (m_SelectedGame.HasPassword)
                        {
                            RenderMessageDialog.RenderInput("Please enter the password: ", (se, ev) => 
                            {
                                if (se != null)
                                {
                                    ClientGameEngine.Get().ChangeState(LoadingState.Get());
                                    CommandJoinGame commandJoinGame = new CommandJoinGame(m_SelectedGame.GameId, se as string);
                                    ClientGameEngine.Get().SendMessageToServer(commandJoinGame);
                                }
                            });
                        }
                        else
                        {
                            CommandJoinGame commandJoinGame = new CommandJoinGame(m_SelectedGame.GameId, string.Empty);
                            ClientGameEngine.Get().SendMessageToServer(commandJoinGame);
                        }
                    }
                }
                else if (selected == "MainMenu")
                {
                    ClientGameEngine.Get().ChangeState(MainMenuState.Get());
                }
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
                m_CurrentGameListView = new ListView();
                m_CurrentGameListView.Opacity = 0.75;

                m_GameGridView = new GridView();
                m_GameGridView.AllowsColumnReorder = false;

                m_NameColumn = new GridViewColumn();
                m_NameColumn.DisplayMemberBinding = new Binding("Name");
                m_NameColumn.Header = "Game Name";
                m_GameGridView.Columns.Add(m_NameColumn);

                m_CapacityColumn = new GridViewColumn();
                m_CapacityColumn.DisplayMemberBinding = new Binding("Capacity");
                m_CapacityColumn.Header = "Capacity";
                m_GameGridView.Columns.Add(m_CapacityColumn);

                m_CurrentGameListView.View = m_GameGridView;
                m_CurrentGameListView.ItemsSource = ClientGameEngine.Get().GamesCollection;

                m_CurrentGameListView.SelectionChanged += (s, e) =>
                {
                    m_SelectedGame = ((ListView)s).SelectedItem as CommandNotifyGame;
                };

                double aspectRatio = Math.Round((double)ClientGameEngine.Get().ClientDimensions.Width / (double)ClientGameEngine.Get().ClientDimensions.Height, 2, MidpointRounding.AwayFromZero);
                string UIComponentPath = "resources\\UI\\Menu\\MultiplayerMenu\\";
                string ratioPath = string.Empty;

                if (aspectRatio == 1.33)
                {
                    //4:3
                    ratioPath = "4x3\\";
                }
                else
                {
                    //16:9
                    ratioPath = "16x9\\";
                }

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage = new BitmapImage(new Uri(UIComponentPath + ratioPath + "Background.png", UriKind.RelativeOrAbsolute));
                m_BackgroundBrush = new ImageBrush(bitmapImage);

                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "JoinGame.png", UriKind.RelativeOrAbsolute));
                m_JoinGameImage = new Image();
                m_JoinGameImage.Source = bitmapImage;
                m_JoinGameImage.Width = bitmapImage.PixelWidth;
                m_JoinGameImage.Height = bitmapImage.PixelHeight;
                m_JoinGameImage.Name = "JoinGame";
                m_JoinGameImage.MouseEnter += MenuOptionHover;
                m_JoinGameImage.MouseLeave += MenuOptionHoverLost;
                m_JoinGameImage.MouseDown += MenuOptionMouseDown;
                m_JoinGameImage.MouseUp += MenuOptionMouseUp;

                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "CreateGame.png", UriKind.RelativeOrAbsolute));
                m_CreateGameImage = new Image();
                m_CreateGameImage.Source = bitmapImage;
                m_CreateGameImage.Width = bitmapImage.PixelWidth;
                m_CreateGameImage.Height = bitmapImage.PixelHeight;
                m_CreateGameImage.Name = "CreateGame";
                m_CreateGameImage.MouseEnter += MenuOptionHover;
                m_CreateGameImage.MouseLeave += MenuOptionHoverLost;
                m_CreateGameImage.MouseDown += MenuOptionMouseDown;
                m_CreateGameImage.MouseUp += MenuOptionMouseUp;

                bitmapImage = new BitmapImage(new Uri(UIComponentPath + "MainMenu.png", UriKind.RelativeOrAbsolute));
                m_MainMenuImage = new Image();
                m_MainMenuImage.Source = bitmapImage;
                m_MainMenuImage.Width = bitmapImage.PixelWidth;
                m_MainMenuImage.Height = bitmapImage.PixelHeight;
                m_MainMenuImage.Name = "MainMenu";
                m_MainMenuImage.MouseEnter += MenuOptionHover;
                m_MainMenuImage.MouseLeave += MenuOptionHoverLost;
                m_MainMenuImage.MouseDown += MenuOptionMouseDown;
                m_MainMenuImage.MouseUp += MenuOptionMouseUp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Render()
        {
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            m_MenuYOffset = dimensions.Height / 2;
            m_MenuXOffset = dimensions.Width * 0.65;

            ClientGameEngine.Get().Canvas.Background = m_BackgroundBrush;   // set the background brush

            m_CurrentGameListView.Width = dimensions.Width / 2;             // resize and add the game listbox to the canvas
            m_CurrentGameListView.Height = dimensions.Height / 2;
            Canvas.SetLeft(m_CurrentGameListView, dimensions.Width / 8);
            Canvas.SetBottom(m_CurrentGameListView, dimensions.Height / 4);
            m_NameColumn.Width = m_CurrentGameListView.Width * 0.65;
            m_CapacityColumn.Width = m_CurrentGameListView.Width * 0.33;
            ClientGameEngine.Get().Canvas.Children.Add(m_CurrentGameListView);

            Canvas.SetLeft(m_JoinGameImage, m_MenuXOffset);                 // resize and add the join game menu item to the canvas
            Canvas.SetBottom(m_JoinGameImage, m_MenuYOffset);
            Canvas.SetZIndex(m_JoinGameImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_JoinGameImage);
            m_MenuYOffset -= m_JoinGameImage.Height * 1.5;


            Canvas.SetLeft(m_CreateGameImage, m_MenuXOffset);               // resize and add the create game menu item to the canvas
            Canvas.SetBottom(m_CreateGameImage, m_MenuYOffset);
            Canvas.SetZIndex(m_CreateGameImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_CreateGameImage);
            m_MenuYOffset -= m_CreateGameImage.Height * 1.5;

            Canvas.SetLeft(m_MainMenuImage, m_MenuXOffset);                 // resize and add the main-menu menu item to the canvas
            Canvas.SetBottom(m_MainMenuImage, m_MenuYOffset);
            Canvas.SetZIndex(m_MainMenuImage, 100);
            ClientGameEngine.Get().Canvas.Children.Add(m_MainMenuImage);
            m_MenuYOffset -= m_MainMenuImage.Height * 1.5;
        }
    }
}
