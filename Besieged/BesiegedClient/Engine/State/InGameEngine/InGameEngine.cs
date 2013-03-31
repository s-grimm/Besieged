using BesiegedClient.Engine.State.InGameEngine.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Framework.Controls;
using System.Threading;

namespace BesiegedClient.Engine.State.InGameEngine
{
    public class InGameEngine
    {
        IInGameState m_CurrentGameState;
        IInGameState m_PreviousGameState;

        private static InGameEngine m_Instance = null;

        public Canvas GameCanvas { get; private set; } //what to draw on
        public VirtualCanvas VirtualGameCanvas;//the actual control

        private InGameEngine() {
            VirtualGameCanvas = new VirtualCanvas();
            GameCanvas = VirtualGameCanvas.ContentCanvas;
        }

        public static InGameEngine Get()
        {
            if (m_Instance == null)
            {
                m_Instance = new InGameEngine();
            }
            return m_Instance;
        }


        public void ChangeState(IGameState gameState)
        {
            throw new NotImplementedException();
            //Task.Factory.StartNew(() =>
            //{
            //    Canvas.Children.Clear();
            //    m_PreviousGameState = m_CurrentGameState;
            //    m_CurrentGameState = gameState;
            //    m_CurrentGameState.Render();
            //}, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }

        public void ChangeState(IGameState gameState, Action postRender)
        {
            throw new NotImplementedException();
            //Task.Factory.StartNew(() =>
            //{
            //    Canvas.Children.Clear();
            //    m_PreviousGameState = m_CurrentGameState;
            //    m_CurrentGameState = gameState;
            //    m_CurrentGameState.Render();
            //    postRender.Invoke();
            //}, CancellationToken.None, TaskCreationOptions.None, GlobalResources.m_TaskScheduler);
        }
    }
}
