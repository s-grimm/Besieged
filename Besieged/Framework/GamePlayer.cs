using Framework.Unit;
namespace Framework
{
    public class GamePlayer
    {
        public IUnitFactory Factory { get; set; }
        public string PlayerID { get; private set; }
        public PlayerColor.PlayerColorEnum PlayerColor { get; set; }

        public GamePlayer(string playerID, Army.ArmyTypeEnum armyType)
        {
            PlayerID = playerID;
            switch (armyType)
            {
                case Army.ArmyTypeEnum.Undead:
                    Factory = new UndeadUnitFactory();
                    break;
                case Army.ArmyTypeEnum.Alliance:
                    Factory = new AllianceUnitFactory();
                    break;
                case Army.ArmyTypeEnum.Beast:
                    Factory = new BeastUnitFactory();
                    break;
                default:
                    break;
            }
        }
    }
}