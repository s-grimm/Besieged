using System.ServiceModel;

namespace Framework
{
    public interface IGameState
    {
        Server.Client Client { get; set; }

        [OperationContract(IsOneWay = true)]
        void UpdateClient(string alias, int id);
    }
    public class GameState : IGameState
    {
        public Server.Client Client { get; set; }

        public GameState()
        {
            Client = new Server.Client();
        }

        public void UpdateClient(string alias, int id) //replace this with a client object eventually
        {
            Client.Alias = alias;
            Client.ClientID = id;
        }
    }
}