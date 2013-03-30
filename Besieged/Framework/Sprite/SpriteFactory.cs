﻿using Framework.Map.Tile;
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
                    return "Skeleton.png";
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
