using BesiegedClient.Engine.State.InGameEngine.State;
using Framework.Controls;
using Framework.Map;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BesiegedClient.Engine.State.InGameEngine
{
    public class InGameEngine
    {


        public Canvas GameCanvas { get; private set; } //what to draw on

        //the following is ALL Virtual Canvas code

        #region "Virtual Canvas"

        private MapZoom zoom;
        private Pan pan;
        private RectangleSelectionGesture rectZoom;
        private AutoScroll autoScroll;

        #endregion "Virtual Canvas"

        public GameMap GameBoard { get; set; }

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
                if (m_CurrentGameState != null)
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
                if (m_CurrentGameState != null)
                {
                    m_CurrentGameState.Dispose();
                }
                m_PreviousGameState = m_CurrentGameState;
                m_CurrentGameState = gameState;
                m_CurrentGameState.Render();
                postRender.Invoke();
            }, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }
    }
}
