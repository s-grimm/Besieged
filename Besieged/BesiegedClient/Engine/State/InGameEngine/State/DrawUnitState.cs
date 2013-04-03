using System.Linq;
using Framework.Map;
using Framework.Sprite;
using Framework.Unit;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public class DrawUnitState : IInGameState
    {
        private static DrawUnitState m_Instance = null;

        private DrawUnitState()
        {
        }

        private const double TileWidth = 50;
        private const double TileHeight = 50;
        private List<Image> _units = new List<Image>();

        public void Initialize()
        {
            //throw new NotImplementedException();
            GameMap map = InGameEngine.Get().Board.GameBoard;
            List<BaseUnit> units = InGameEngine.Get().Board.Units;

            foreach (IUnit unit in units)
            {
                var sprite = unit.GetSprite();
                Image rect = Utilities.Rendering.GetImageForUnit(sprite.ToString());
                rect.Width = TileWidth;
                rect.Height = TileHeight;



                Canvas.SetLeft(rect, unit.X_Position * TileWidth);
                Canvas.SetTop(rect, unit.Y_Position * TileHeight);
                Canvas.SetZIndex(rect, 15);
                _units.Add(rect);
            }

        }

        public void Render()
        {
            Canvas target = InGameEngine.Get().GameCanvas;
            foreach (Image r in _units)
            {
                r.MouseDown += InGameEngine.Get().unit_MouseLeftButtonDown;
                r.MouseMove += InGameEngine.Get().unit_MouseMove;
                r.MouseUp   += InGameEngine.Get().unit_MouseLeftButtonUp;
                target.Children.Add(r);
            }
        }

        public void Dispose()
        {
            //Canvas target = InGameEngine.Get().GameCanvas;
            //foreach (Image r in _units.Where(r => target.Children.Contains(r)))
            //{
            //    target.Children.Remove(r);
            //}
        }

        public static IInGameState Get()
        {
            try
            {
                if (m_Instance == null)
                {
                    m_Instance = new DrawUnitState();
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