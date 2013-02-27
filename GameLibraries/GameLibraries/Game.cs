using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraries
{
    public class Game
    {
        public int turnNum { get; set; }
        public bool state { get; set; }
        public string winner { get; set; }
        public bool turnNew { get; set; }
        public int numCastles { get; set; } //placeholder for castle array
        public int store { get; set; } //placeholder for store array
        public int numPlayers { get; set; }
        public int currentPlayer { get; set; }
        public List<int> players;

        public Game newGame(int n)
        {
            //build a new default game object with the specified number of Castles
            Game game = new Game();
            game.numCastles = n;
            game.numPlayers = n;
            game.turnNum = 0;
            game.state = true;
            game.winner = "None";
            game.turnNew = true;
            game.store = 10;

            for (int i = 1; i <= n; ++i)
            {
                game.players.Add(i);
            }

            return game;
        }

        public Game updateGameState(Game g)
        {
            //update the common game object with data from the current user
            this.state = g.state;
            this.numPlayers = g.numPlayers;
            this.numCastles = g.numCastles;
            this.turnNum = g.turnNum;
            this.players = g.players;
            this.store = g.store;

            //check if new turn
            if (currentPlayer == players.Last())
            {
                this.turnNew = true;
                currentPlayer = players.First();
            }

            //placeholder loop for actual WCF service updating
            //foreach (int i in this.numCastles)
            //{
                    //need to write player and upgrade stats before writing the update method
               



            //}
            return this;
        }


    }
}
