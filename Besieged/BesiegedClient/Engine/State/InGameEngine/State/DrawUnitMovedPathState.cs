using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class DrawUnitMovedPathState : IInGameState
    {
        private static DrawUnitMovedPathState m_Instance = null;

        private List<Rectangle> _overlay;
        private const double TileWidth = 50;
        private const double TileHeight = 50;
        public static IEnumerable<Tuple<int, int>> OverlayTiles { get; set; }

        public void Cleanup()
        {
        }

        private DrawUnitMovedPathState()
        {
        }

        public void Initialize()
        {
            _overlay = new List<Rectangle>();
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
                        Fill = Utilities.Rendering.GreenBrush,
                        Opacity = 0.5
                    };

                Canvas.SetTop(overlay, tile.Item1 * TileWidth);
                Canvas.SetLeft(overlay, tile.Item2 * TileHeight);
                _overlay.Add(overlay);
            }

            foreach (Rectangle r in _overlay)
            {
                InGameEngine.Get().GameCanvas.Children.Add(r);
            }
        }

        public void Dispose()
        {
            foreach (Rectangle r in _overlay.Where(r => InGameEngine.Get().GameCanvas.Children.Contains(r)))
            {
                InGameEngine.Get().GameCanvas.Children.Remove(r);
            }

            _overlay.Clear();
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new DrawUnitMovedPathState();
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
