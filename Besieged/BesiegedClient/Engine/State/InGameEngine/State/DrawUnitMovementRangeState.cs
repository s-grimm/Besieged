using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class DrawUnitMovementRangeState : IInGameState
    {
        private static DrawUnitMovementRangeState m_Instance = null;

        private List<Rectangle> _path;
        private const double TileWidth = 50;
        private const double TileHeight = 50;
        public static IEnumerable<Tuple<int, int>> OverlayTiles { get; set; }

        public void Cleanup()
        {
        }

        private DrawUnitMovementRangeState()
        {
        }

        public void Initialize()
        {
            _path = new List<Rectangle>();
        }

        public void Render()
        {
            if (OverlayTiles == null) return;

            foreach (Tuple<int, int> tile in OverlayTiles)
            {
                Rectangle overlay = new Rectangle
                    {
                        Width = TileWidth,
                        Height = TileHeight,
                        Fill = Utilities.Rendering.BlueBrush,
                        Opacity = 0.5
                    };

                Canvas.SetTop(overlay, tile.Item1 * TileWidth);
                Canvas.SetLeft(overlay, tile.Item2 * TileHeight);
                _path.Add(overlay);
            }

            foreach (Rectangle r in _path)
            {
                InGameEngine.Get().GameCanvas.Children.Add(r);
            }
        }

        public void Dispose()
        {
            foreach (Rectangle r in _path.Where(r => InGameEngine.Get().GameCanvas.Children.Contains(r)))
            {
                InGameEngine.Get().GameCanvas.Children.Remove(r);
            }

            _path.Clear();
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new DrawUnitMovementRangeState();
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
