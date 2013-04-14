using Framework.Map;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class InGameSetupState : IInGameState
    {
        private static InGameSetupState m_Instance = null;

        private InGameSetupState()
        {
        }

        public void Cleanup()
        {
        }

        private const double TileWidth = 50;
        private const double TileHeight = 50;
        private List<Rectangle> _rectangles = new List<Rectangle>();

        public void Initialize()
        {
            //throw new NotImplementedException();
            GameMap map = InGameEngine.Get().Board.GameBoard;

            double mapHeight = map.MapHeight * TileHeight;
            double mapWidth = map.MapLength * TileWidth;

            InGameEngine.Get().VirtualGameCanvas.Boundry = new System.Windows.Size(mapWidth, mapHeight + InGameEngine.Get().UI_ScaleAdjustment - 25);

            for (int i = 0; i < map.MapLength; i += 1)
            {
                for (int y = 0; y < map.MapHeight; y += 1)
                {
                    var sprite = map.Tiles[i][y].GetSprite();
                    Rectangle rect = new Rectangle
                        {
                            StrokeThickness = 1,
                            Stroke = new SolidColorBrush(Colors.Black),
                            Fill = Utilities.Rendering.GetBrushForTile(sprite.ToString()),
                            Width = 50,
                            Height = 50,
                            Name = "box" + i
                        }; //create the rectangle
                    Canvas.SetLeft(rect, i * TileWidth);
                    Canvas.SetTop(rect, y * TileHeight);
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

            InGameEngine.Get().ChangeState(DrawUnitState.Get());
        }

        public void Dispose()
        {
            Canvas target = InGameEngine.Get().GameCanvas;
            foreach (Rectangle r in _rectangles.Where(r => target.Children.Contains(r)))
            {
                target.Children.Remove(r);
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
                throw;
            }
        }
    }
}