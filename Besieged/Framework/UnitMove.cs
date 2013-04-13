namespace Framework
{
    public class UnitMove
    {
        public Coordinate StartCoordinate { get; set; }

        public Coordinate EndCoordinate { get; set; }

        public UnitMove()
        {
        }

        public UnitMove(Coordinate start, Coordinate end)
        {
            StartCoordinate = start;
            EndCoordinate = end;
        }
    }

    public class UnitAttack
    {
        public Coordinate Attacker { get; set; }

        public Coordinate Defender { get; set; }

        public UnitAttack()
        {
        }

        public UnitAttack(Coordinate attacker, Coordinate defender)
        {
            Attacker = attacker;
            Defender = defender;
        }
    }

    public class Coordinate
    {
        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }

        public Coordinate()
        {
        }

        public Coordinate(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
    }
}