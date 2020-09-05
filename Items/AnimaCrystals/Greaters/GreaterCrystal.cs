using System;
using System.Collections.Generic;
using NewDawn.Essences;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;

namespace NewDawn.Items.AnimaCrystals.Greaters
{
    public class GreaterCrystal : AnimaCrystal
    {
        public const int
            WIDTH = 20,
            HEIGHT = 28;


        public GreaterCrystal() : base(new [] 
            {
                (GameCulture.English, "Rough Crystal", "Greater"),
                (GameCulture.French, "Cristal Rugueux", "Supérieur"),
            },
            WIDTH, HEIGHT)
        {

        }


        protected GreaterCrystal((GameCulture culture, string displayName, string tooltip)[] strings, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White, int maxStack = 1) : base(strings, width, height, value, defense, rarity, maxStack)
        {

        }


        public override int EssenceCapacity => 2;

        public override Essence.Levels MaxEssenceLevel => Essence.Levels.GREATER;

        public new List<Essence> Essences = new List<Essence>();
    }
}
