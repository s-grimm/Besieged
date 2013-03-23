using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class NewGameState : IGameState
    {
        private static NewGameState m_Instance = null;

        private NewGameState()
        {
        }

        private double m_MenuYOffset;
        private double m_MenuXOffset;

        private Image m_LogoImage;
        private Image m_SinglePlayerImage;
        private Image m_MultiPlayerImage;
        private Image m_OptionsImage;
        private Image m_QuitImage;
        private ImageBrush m_BackgroundBrush;

        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new NewGameState();
                    m_Instance.Initialize();
                }
                return m_Instance;
            }
            catch (Exception ex)
            {
                // error handling
                throw ex;
            }
        }

        public void Initialize()
        {
            double aspectRatio = Math.Round((double)ClientGameEngine.Get().ClientDimensions.Width / (double)ClientGameEngine.Get().ClientDimensions.Height, 2, MidpointRounding.AwayFromZero);

            string UIComponentPath = "resources\\UI\\Menu\\MultiplayerMenu\\";
            string ratioPath = string.Empty;

            if (aspectRatio == 1.33)
                ratioPath = "4x3\\";
            else
                ratioPath = "16x9\\";

            BitmapImage bimg;

            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }
    }
}