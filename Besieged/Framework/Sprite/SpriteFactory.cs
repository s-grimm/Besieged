using Framework.Map.Tile;
using Framework.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public static class SpriteFactory
    {
        public static object GetSprite(DrawableObject drawableObject)
        {
            try
            {
                if (drawableObject is Dwarf)
                {
                    return "Dwarf.png";
                }
                else if (drawableObject is DwarfChampion)
                {
                    return "DwarfChampion.png";
                }
                else if (drawableObject is Archer)
                {
                    return "Archer.png";
                }
                else if (drawableObject is ElvenRanger)
                {
                    return "ElvenRanger.png";
                }
                else if (drawableObject is Centaur)
                {
                    return "Centaur.png";
                }
                else if (drawableObject is CentaurOutrider)
                {
                    return "CentaurOutrider.png";
                }
                else if (drawableObject is Wizard)
                {
                    return "Wizard.png";
                }
                else if (drawableObject is AscendantWizard)
                {
                    return "AscendantWizard.png";
                }
                else if (drawableObject is Gnoll)
                {
                    return "Gnoll.png";
                }
                else if (drawableObject is GnollMarauder)
                {
                    return "GnollMarauder.png";
                }
                else if (drawableObject is Lizardman)
                {
                    return "Lizardman.png";
                }
                else if (drawableObject is LizardmanCaptain)
                {
                    return "LizardmanCaptain.png";
                }
                else if (drawableObject is Taurus)
                {
                    return "Taurus.png";
                }
                else if (drawableObject is TaurusLord)
                {
                    return "TaurusLord.png";
                }
                else if (drawableObject is Genie)
                {
                    return "Genie.png";
                }
                else if (drawableObject is MasterGenie)
                {
                    return "MasterGenie.png";
                }
                else if (drawableObject is Skeleton)
                {
                    return "SkeletonCaptain.png";
                }
                else if (drawableObject is SkeletonCaptain)
                {
                    return "SkeletonCaptain.png";
                }
                else if (drawableObject is Orc)
                {
                    return "Orc.png";
                }
                else if (drawableObject is OrcChief)
                {
                    return "OrcChief.png";
                }
                else if (drawableObject is WolfRider)
                {
                    return "WolfRider.png";
                }
                else if (drawableObject is WolfLord)
                {
                    return "WolfLord.png";
                }
                else if (drawableObject is Vampire)
                {
                    return "Vampire.png";
                }
                else if (drawableObject is VampireCount)
                {
                    return "VampireCount.png";
                }
                else if (drawableObject is Grass)
                {
                    return "Grass.png";
                }
                else if (drawableObject is Border)
                {
                    return "Border.png";
                }
                else if (drawableObject is ArcticBridge)
                {
                    return "ArcticBridge.png";
                }
                else if (drawableObject is ArcticRiver)
                {
                    return "ArcticRiver.png";
                }
                else if (drawableObject is ArcticRoad)
                {
                    return "ArcticRoad.png";
                }
                else if (drawableObject is Ash)
                {
                    return "Ash.png";
                }
                else if (drawableObject is Bush)
                {
                    return "Bush.png";
                }
                else if (drawableObject is DeepSnow)
                {
                    return "DeepSnow.png";
                }
                else if (drawableObject is DesertBridge)
                {
                    return "DesertBridge.png";
                }
                else if (drawableObject is DesertRiver)
                {
                    return "DesertRiver.png";
                }
                else if (drawableObject is DesertRoad)
                {
                    return "DesertRoad.png";
                }
                else if (drawableObject is DesolateBridge)
                {
                    return "DesolateBridge.png";
                }
                else if (drawableObject is DesolateRoad)
                {
                    return "DesolateRoad.png";
                }
                else if (drawableObject is Door)
                {
                    return "Door.png";
                }
                else if (drawableObject is DungeonBridge)
                {
                    return "DungeonBridge.png";
                }
                else if (drawableObject is DungeonRoad)
                {
                    return "DungeonRoad.png";
                }
                else if (drawableObject is DungeonRiver)
                {
                    return "DungeonRiver.png";
                }
                else if (drawableObject is GrasslandBridge)
                {
                    return "GrasslandBridge.png";
                }
                else if (drawableObject is Ice)
                {
                    return "Ice.png";
                }
                else if (drawableObject is Lava)
                {
                    return "Lava.png";
                }
                else if (drawableObject is Oasis)
                {
                    return "Oasis.png";
                }
                else if (drawableObject is Quicksand)
                {
                    return "Quicksand.png";
                }
                else if (drawableObject is Sand)
                {
                    return "Sand.png";
                }
                else if (drawableObject is Snow)
                {
                    return "Snow.png";
                }
                else if (drawableObject is Spikes)
                {
                    return "Spikes.png";
                }
                else if (drawableObject is StoneFloor)
                {
                    return "StoneFloor.png";
                }
                else if (drawableObject is Tree)
                {
                    return "Tree.png";
                }
                else if (drawableObject is WetlandBridge)
                {
                    return "WetlandBridge.png";
                }
                else if (drawableObject is WetlandRoad)
                {
                    return "WetlandRoad.png";
                }
                else if (drawableObject is WetlandRiver)
                {
                    return "WetlandRiver.png";
                }
				else if (drawableObject is AllianceCastle) {
                    return "AllianceCastle.png";
                }
                else if (drawableObject is BeastCastle) {
                    return "BeastCastle.png";
                }
                else if (drawableObject is UndeadCastle) {
                    return "UndeadCastle.png";
                }

                else
                {
                    return string.Empty;
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
