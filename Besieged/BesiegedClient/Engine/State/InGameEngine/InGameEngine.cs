using BesiegedClient.Engine.State.InGameEngine.State;
using Framework;
using Framework.Controls;
using Framework.Map;
using Framework.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BesiegedClient.Pathing;

namespace BesiegedClient.Engine.State.InGameEngine
{
    public class InGameEngine
    {
        private IInGameState m_CurrentGameState;
        private IInGameState m_PreviousGameState;
        private PathFinder _pathFinder;
        private static InGameEngine m_Instance = null;

        public Canvas GameCanvas { get; private set; } //what to draw on
        public IUnit SelectedUnit { get; set; }
        //the following is ALL Virtual Canvas code

        #region "Virtual Canvas"

        public VirtualCanvas VirtualGameCanvas;//the actual control
        private MapZoom zoom;
        private Pan pan;
        private RectangleSelectionGesture rectZoom;
        private AutoScroll autoScroll;

        #endregion "Virtual Canvas"

        public GameState Board { get; set; }

        private InGameEngine()
        {
            #region "DrawCanvas"

            VirtualGameCanvas = new VirtualCanvas();
            GameCanvas = VirtualGameCanvas.ContentCanvas;
            VirtualGameCanvas.Width = 800;
            VirtualGameCanvas.Height = 600;

            VirtualGameCanvas.SmallScrollIncrement = new Size(50 / 2, 50 / 2); //smallest scroll increment //change this later

            zoom = new MapZoom(GameCanvas); //set the zoom to the canvas we are drawing on.
            pan = new Pan(GameCanvas, zoom); //panning
            rectZoom = new RectangleSelectionGesture(GameCanvas, zoom, ModifierKeys.Control);
            rectZoom.ZoomSelection = true;
            autoScroll = new AutoScroll(GameCanvas, zoom);

            #endregion "DrawCanvas"
            _pathFinder = new PathFinder();
            //kickstart
            DrawUnitMovementRangeState.Get();
        }

        public static InGameEngine Get()
        {
            return m_Instance ?? (m_Instance = new InGameEngine());
        }

        public void ChangeState(IInGameState gameState)
        {
            Task.Factory.StartNew(() =>
            {
                if (m_CurrentGameState != null && m_CurrentGameState != InGameSetupState.Get())
                {
                    m_CurrentGameState.Dispose();
                }
                m_PreviousGameState = m_CurrentGameState;
                m_CurrentGameState = gameState;
                m_CurrentGameState.Render();
            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        public void ChangeState(IInGameState gameState, Action postRender)
        {
            Task.Factory.StartNew(() =>
            {
                if (m_CurrentGameState != null && m_CurrentGameState != InGameSetupState.Get())
                {
                    m_CurrentGameState.Dispose();
                }
                m_PreviousGameState = m_CurrentGameState;
                m_CurrentGameState = gameState;
                m_CurrentGameState.Render();
                postRender.Invoke();
            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        //unit movement shit
        private bool captured = false;
        private bool _preventAction = false;
        private double _xShape, _yShape;
        private int _xOriginal, _yOriginal;
        private UIElement _source = null;
        private IUnit _selectedUnit = null;

        public void unit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            _source = (UIElement)sender;
            Mouse.Capture(_source);
            _xShape = Canvas.GetLeft(_source);
            _yShape = Canvas.GetTop(_source);
            _yOriginal = (int)_yShape;
            _xOriginal = (int) _xShape;
            _selectedUnit = Board.Units.FirstOrDefault(x => x.X_Position == _xOriginal / 50 && x.Y_Position == _yOriginal / 50);
            SelectedUnit = _selectedUnit;
            if (_preventAction)
            {
                _selectedUnit = null;
                return;
            }
            if (_selectedUnit == null) return;
            //overlay the units movement ability
            DrawUnitMovementRangeState.OverlayTiles = PathFinder.FindRange(_xOriginal / 50, _yOriginal / 50, _selectedUnit.Movement);
            InGameEngine.Get().ChangeState(DrawUnitMovementRangeState.Get());
            _preventAction = false;
            captured = true;
            _preventAction = false;
        }

        public void unit_MouseMove(object sender, MouseEventArgs e)
        {
            if (_preventAction) return;
            _preventAction = true;
            if (!captured || _selectedUnit == null)
            {
                _preventAction = false;
                return;
            }
            double x = e.GetPosition(ClientGameEngine.Get().Canvas).X;
            double y = e.GetPosition(ClientGameEngine.Get().Canvas).Y;
            Canvas.SetLeft(_source, x + VirtualGameCanvas.HorizontalOffset);
            Canvas.SetTop(_source, y + VirtualGameCanvas.VerticalOffset);
            e.Handled = true;
            _preventAction = false;
        }

        public void unit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (captured )
            {
                if (_preventAction) return;
                _preventAction = true;
                if (_selectedUnit != null)
                {
                    //snap to closest tile
                    double x = e.GetPosition(ClientGameEngine.Get().Canvas).X + VirtualGameCanvas.HorizontalOffset;
                    double y = e.GetPosition(ClientGameEngine.Get().Canvas).Y + VirtualGameCanvas.VerticalOffset;
                    const int factor = 50;
                    int nearestX = (int)Math.Round((x / (double)factor)) * factor;
                    int nearestY = (int)Math.Round((y / (double)factor)) * factor;

                    //check bounds on unit

                    int movementRange = _selectedUnit.Movement;

                    if (!Board.GameBoard.Tiles[nearestY/50][nearestX/50].IsPassable //the tile is impassable
                        || Board.Units.Any(unit => unit.X_Position == (nearestX/50) && unit.Y_Position == (nearestY/50)))
                        //there is a unit here already
                    {
                        Canvas.SetLeft(_source, _xOriginal);
                        Canvas.SetTop(_source, _yOriginal);
                    }
                    else
                    {

                        var path = PathFinder.FindPath(_xOriginal/50, _yOriginal/50, nearestX/50, nearestY/50);
                        //tuples in path are y,x not x,y
                        if (path == null //no path 
                            || path.TotalCost > movementRange) //it costs more than your movement range to move here 
                        {
                            Canvas.SetLeft(_source, _xOriginal);
                            Canvas.SetTop(_source, _yOriginal);
                        }
                        else
                        {
                            DrawUnitMovedPathState.OverlayTiles = path;
                            // update unit with its new place
                            _selectedUnit.Y_Position = nearestY/50;
                            _selectedUnit.X_Position = nearestX/50;

                            Canvas.SetLeft(_source, nearestX);
                            Canvas.SetTop(_source, nearestY);
                            InGameEngine.Get().ChangeState(DrawUnitMovedPathState.Get());
                        }
                    }
                    _selectedUnit = null;
                    //if(m_CurrentGameState != WaitingState.Get())
                    //    InGameEngine.Get().ChangeState(WaitingState.Get());
                }
                _preventAction = false;
            }
            Mouse.Capture(null);
            captured = false;
        }

        public void ActivateTurn()
        {
            _preventAction = false;
        }
        public void DeActivateTurn()
        {
            _preventAction = true;
        }
    }
}