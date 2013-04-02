using Framework.Map;
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
        
        private double _tileWidth = 50;
        private double _tileHeight = 50;
        private List<Image> _units = new List<Image>();

        public void Initialize()
        {
            //throw new NotImplementedException();
            GameMap map = InGameEngine.Get().GameBoard;
            List<IUnit> units = InGameEngine.Get().Units;


            foreach (IUnit unit in units)
            {
                var sprite = unit.GetSprite();
                Image rect = Utilities.Rendering.GetImageForUnit(sprite.ToString());
                rect.Width = _tileWidth;
                rect.Height = _tileHeight;



                Canvas.SetLeft(rect, unit.X_Position * _tileWidth);
                Canvas.SetTop(rect, unit.Y_Position * _tileHeight);
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
            Canvas target = InGameEngine.Get().GameCanvas;
            foreach (Image r in _units)
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
                    m_Instance = new DrawUnitState();
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