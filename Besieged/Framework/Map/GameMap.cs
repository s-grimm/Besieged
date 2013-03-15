using Framework.Map.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Map
{
    public class GameMap
    {
        public BaseTile[][] Tiles { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string MD5Hash { get; set; }

        public int MapLength { get; set; }

        public int MapHeight { get; set; }

        public GameMap()
        {
            MapLength = 50;
            MapHeight = 50;
            Tiles = new BaseTile[MapHeight][];
            for (int i = 0; i < MapHeight; ++i)
            {
                Tiles[i] = new BaseTile[MapLength]; 
            }

            for (int x = 0; x < MapHeight; ++x)
            {
                for (int y = 0; y < MapLength; ++y)
                {
                    Tiles[x][y] = new Grass();
                }
            }
        }
    }
}