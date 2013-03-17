using Framework.Commands;
using Framework.Utilities.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Rendering
{
    public static class RenderPreGame
    {
        private static Dimensions dimensions;
        private static double menuYOffset;
        private static double menuXOffset;
        private static object mousedownRef;

        public static void RenderPreGameLobby()
        {
            dimensions = new Dimensions() { Width = (int)GlobalResources.GameWindow.Width, Height = (int)GlobalResources.GameWindow.Height };
            menuYOffset = dimensions.Height / 2;
            menuXOffset = dimensions.Width * 0.65;
            GlobalResources.GameWindow.Children.Clear();

            double aspectRatio = Math.Round((double)dimensions.Width / (double)dimensions.Height, 2, MidpointRounding.AwayFromZero);

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
            Image img = new Image();
            BitmapImage bimg;
            try
            {
                bimg = new BitmapImage(new Uri(UIComponentPath + ratioPath + "Background.png", UriKind.RelativeOrAbsolute));
                img.Source = bimg;
                img.Width = bimg.PixelWidth;
                img.Height = bimg.PixelHeight;
                GlobalResources.GameWindow.Background = new ImageBrush(bimg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading UI Component : Background.png", "UI Load Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ListBox listBoxChatMessages = new ListBox();
            listBoxChatMessages.ItemsSource = GlobalResources.GameSpecificChatMessages;
            listBoxChatMessages.Opacity = 0.75;
            listBoxChatMessages.Height = dimensions.Height * 0.25;
            listBoxChatMessages.Width = dimensions.Width * 0.7;
            listBoxChatMessages.FontFamily = new FontFamily("Papyrus");
            listBoxChatMessages.FontSize = 14;
            Canvas.SetLeft(listBoxChatMessages, dimensions.Width * 0.15);
            Canvas.SetBottom(listBoxChatMessages, dimensions.Height * 0.10);
            GlobalResources.GameWindow.Children.Add(listBoxChatMessages);

            TextBox chatMessage = new TextBox();
            chatMessage.Opacity = 0.75;
            chatMessage.FontFamily = new FontFamily("Papyrus");
            chatMessage.FontSize = 18;
            chatMessage.Width = dimensions.Width * 0.60;
            chatMessage.Height = dimensions.Height * 0.05;
            Canvas.SetLeft(chatMessage, dimensions.Width * 0.15);
            Canvas.SetBottom(chatMessage, dimensions.Height * 0.025);
            GlobalResources.GameWindow.Children.Add(chatMessage);

            Button sendButton = new Button();
            sendButton.FontFamily = new FontFamily("Papyrus");
            sendButton.FontSize = 18;
            sendButton.Content = "Send";
            sendButton.Height = dimensions.Height * 0.05;
            sendButton.Width = dimensions.Width * 0.08;
            Canvas.SetBottom(sendButton, dimensions.Height * 0.025);
            Canvas.SetLeft(sendButton, dimensions.Width * 0.77);
            GlobalResources.GameWindow.Children.Add(sendButton);

            sendButton.Click += (s, ev) =>
            {
                if (chatMessage.Text.Trim() != string.Empty)
                {
                    CommandChatMessage commandChatMessage = new CommandChatMessage(chatMessage.Text.Trim());
                    commandChatMessage.GameId = GlobalResources.GameId;
                    chatMessage.Text = "";
                    GlobalResources.SendMessageToServer(commandChatMessage.ToXml());
                }
            };
        }
    }
}
