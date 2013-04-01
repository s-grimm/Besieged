using Framework.Map;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class InGameSetupState : IInGameState
    {
        private Color CreateRandomColor()
        {
            Random randonGen = new Random();
            Color randomColor = Color.FromRgb((byte)randonGen.Next(0, 255), (byte)randonGen.Next(0, 255), (byte)randonGen.Next(0, 255));
            return randomColor;
        }

        private static InGameSetupState m_Instance = null;

        private InGameSetupState()
        {
        }

        private double _tileWidth = 50;
        private double _tileHeight = 50;
        private List<Rectangle> _rectangles = new List<Rectangle>();

        public void Initialize()
        {
            //throw new NotImplementedException();
            GameMap map = InGameEngine.Get().GameBoard;

            double mapHeight = map.MapHeight * _tileHeight;
            double mapWidth = map.MapLength * _tileWidth;

            InGameEngine.Get().VirtualGameCanvas.Boundry = new System.Windows.Size(mapWidth, mapHeight);

            for (int i = 0; i < map.MapLength; i += 1)
            {
                for (int y = 0; y < map.MapHeight; y += 1)
                {
                    var sprite = map.Tiles[i][y].GetSprite();
                    Rectangle rect = new Rectangle(); //create the rectangle
                    rect.StrokeThickness = 1;  //border to 1 stroke thick
                    rect.Stroke = new SolidColorBrush(Colors.Black); //border color to black
                    rect.Fill = Utilities.Rendering.GetBrushForTile(sprite.ToString());
                    rect.Width = 50;
                    rect.Height = 50;
                    rect.Name = "box" + i.ToString();
                    Canvas.SetLeft(rect, i * _tileWidth);
                    Canvas.SetTop(rect, y * _tileHeight);
                    _rectangles.Add(rect);
                }
            }
        }

        public void Render()
        {
            Canvas target = InGameEngine.Get().GameCanvas;
            foreach (Rectangle r in _rectangles)
            {
                target.Children.Add(r);
            }
        }

        public void Dispose()
        {
            Canvas target = InGameEngine.Get().GameCanvas;
            foreach (Rectangle r in _rectangles)
            {
                if (target.Children.Contains(r))
                {
                    target.Children.Remove(r);
                }
            }
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new InGameSetupState();
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
    }
}