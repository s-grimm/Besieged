using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace BesiegedClient.Engine.State
{
    public class SplashScreenLogoState : IGameState
    {
        private static SplashScreenLogoState m_Instance = null;
        private SplashScreenLogoState() { }

        public static IGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new SplashScreenLogoState();
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
        
        Storyboard m_FadeStory;
        Image m_Logo;

        public void Initialize()
        {
            BitmapImage logo = new BitmapImage(new Uri("resources/Logo.png", UriKind.RelativeOrAbsolute));
            
            m_Logo = new Image();
            m_Logo.Source = logo;
            m_Logo.Width = logo.PixelWidth;
            m_Logo.Height = logo.PixelHeight;
        }

        public void Render()
        {
            Dimensions dimensions = ClientGameEngine.Get().ClientDimensions;
            ClientGameEngine.Get().Canvas.Background = Utilities.Rendering.BlackBrush;

            Canvas.SetLeft(m_Logo, dimensions.Width / 2 - m_Logo.Width / 2);
            Canvas.SetTop(m_Logo, dimensions.Height / 2 - m_Logo.Height / 2);
            ClientGameEngine.Get().Canvas.Children.Add(m_Logo);

            //setup storyboard
            DoubleAnimation ani = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(0)

            };

            Storyboard.SetTarget(ani, m_Logo);
            Storyboard.SetTargetProperty(ani, new PropertyPath(Image.OpacityProperty));

            DoubleAnimation anii = new DoubleAnimation()
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(3),
                BeginTime = TimeSpan.FromSeconds(2)
            };

            Storyboard.SetTarget(anii, m_Logo);
            Storyboard.SetTargetProperty(anii, new PropertyPath(Image.OpacityProperty));

            DoubleAnimation aniii = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2),
                BeginTime = TimeSpan.FromSeconds(5)
            };

            Storyboard.SetTarget(aniii, m_Logo);
            Storyboard.SetTargetProperty(aniii, new PropertyPath(Image.OpacityProperty));

            m_FadeStory = new Storyboard();
            m_FadeStory.Children.Add(ani);
            m_FadeStory.Children.Add(anii);
            m_FadeStory.Children.Add(aniii);
            //Execute Story
            m_FadeStory.Begin();
        }
    }
}
