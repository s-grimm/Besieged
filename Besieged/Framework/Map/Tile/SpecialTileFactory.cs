namespace Framework.Map.Tile
{
    public class SpecialTileFactory : ITileFactory
    {
        public BaseTile GetBasicGround()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }

        public BaseTile GetBasicRoad()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }

        public BaseTile GetBasicBridge()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }

        public BaseTile GetHardTerrain()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }

        public BaseTile GetFluidTerrain()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }

        public BaseTile GetSpecialTerrain()
        {
            return new Border()
            {
                CurrentHealthPointsModifier = 0,
                MaxHealthPointsModifier = 0,
                InitiativeModifier = 0,
                SkillModifier = 0,
                RangeModifier = 0,
                MovementModifier = 0,
                IsPassable = false
            };
        }
    }
}