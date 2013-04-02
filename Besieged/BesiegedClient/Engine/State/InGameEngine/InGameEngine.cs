using BesiegedClient.Engine.State.InGameEngine.State;
using Framework.Controls;
using Framework.Map;
using Framework.Unit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
namespace BesiegedClient.Engine.State.InGameEngine
{
    public class InGameEngine
    {
        private IInGameState m_CurrentGameState;
        private IInGameState m_PreviousGameState;

        private static InGameEngine m_Instance = null;

        public Canvas GameCanvas { get; private set; } //what to draw on

        //the following is ALL Virtual Canvas code

        #region "Virtual Canvas"

        public VirtualCanvas VirtualGameCanvas;//the actual control
        private MapZoom zoom;
        private Pan pan;
        private RectangleSelectionGesture rectZoom;
        private AutoScroll autoScroll;

        #endregion "Virtual Canvas"

        public GameMap GameBoard { get; set; }
        public List<IUnit> Units { get; set; }

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

            //Hack to get a map for Shane to use - replace this with the REAL map later
            GameBoard = new GameMap();

            //unit shit
            Units = new List<IUnit>();

            BeastUnitFactory bFac = new BeastUnitFactory();
            //place a couple of units for testing purposes
            for (int i = 4; i <= 12; i += 4)
            {
                IUnit l = bFac.GetBasicInfantry();
                l.X_Position = i;
                l.Y_Position = i;
                Units.Add(l);
            }
        }

        public static InGameEngine Get()
        {
            if (m_Instance == null)
            {
                m_Instance = new InGameEngine();
            }
            return m_Instance;
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
        bool captured = false;
        double x_shape, y_shape;
        int x_original, y_original;
        UIElement source = null;
        IUnit selectedUnit = null;

        public void unit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            source = (UIElement)sender;
            Mouse.Capture(source);
            captured = true;
            x_shape = Canvas.GetLeft(source);
            y_shape = Canvas.GetTop(source);
            y_original = (int)y_shape / 50;
            x_original = (int)x_shape / 50;
            selectedUnit = Units.Where(x => x.X_Position == x_original && x.Y_Position == y_original).FirstOrDefault();

            e.Handled = true;

            ClientGameEngine.Get().m_CurrentWindow.Cursor = Cursors.Hand;
            //this.ChangeState(UnitSelectedState);
        }
        public void unit_MouseMove(object sender, MouseEventArgs e)
        {
            if (captured && selectedUnit != null)
            {
                double x = e.GetPosition(ClientGameEngine.Get().Canvas).X;
                double y = e.GetPosition(ClientGameEngine.Get().Canvas).Y;
                Canvas.SetLeft(source, x + VirtualGameCanvas.HorizontalOffset);
                Canvas.SetTop(source, y + VirtualGameCanvas.VerticalOffset);
                e.Handled = true;
            }
        }

        public void unit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (captured)
            {
                if (selectedUnit != null)
                {
                    //snap to closest tile
                    double x = e.GetPosition(ClientGameEngine.Get().Canvas).X + VirtualGameCanvas.HorizontalOffset;
                    double y = e.GetPosition(ClientGameEngine.Get().Canvas).Y + VirtualGameCanvas.VerticalOffset;
                    int factor = 50;
                    int nearestX = (int)Math.Round((x / (double)factor)) * factor;
                    int nearestY = (int)Math.Round((y / (double)factor)) * factor;

                    //check bounds on unit

                    int movementRange = selectedUnit.Movement;
                    int totalMovedY = nearestY / 50 - y_original;
                    int totalMovedX = nearestX / 50 - x_original;
                    if (totalMovedY < 0) totalMovedY *= -1;
                    if (totalMovedX < 0) totalMovedX *= -1;

                    if (totalMovedX + totalMovedY > movementRange || !GameBoard.Tiles[nearestY / 50][nearestX / 50].IsPassable || Units.Any(unit => unit.X_Position == nearestX / 50 && unit.Y_Position == nearestY / 50)) //there is a unit here already OR the tile is impassable, return it to its original position
                    {
                        Canvas.SetLeft(source, x_original * 50);
                        Canvas.SetTop(source, y_original * 50);
                    }
                    else
                    {
                        // update unit with its new place
                        selectedUnit.Y_Position = nearestY / 50;
                        selectedUnit.X_Position = nearestX / 50;

                        Canvas.SetLeft(source, nearestX);
                        Canvas.SetTop(source, nearestY);
                    }
                }
            }
            Mouse.Capture(null);
            captured = false;
            selectedUnit = null;

            ClientGameEngine.Get().m_CurrentWindow.Cursor = Cursors.Arrow;

        }
    }
}