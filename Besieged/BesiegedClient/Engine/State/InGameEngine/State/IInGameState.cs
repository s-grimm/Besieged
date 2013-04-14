namespace BesiegedClient.Engine.State.InGameEngine.State
{
    public interface IInGameState
    {
        void Initialize();

        void Render();

        void Dispose();

        void Cleanup();
    }
}