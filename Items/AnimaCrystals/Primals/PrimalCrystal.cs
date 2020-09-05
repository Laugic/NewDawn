using NewDawn.Essences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.Localization;

namespace NewDawn.Items.AnimaCrystals.Primals
{
    public class PrimalCrystal : AnimaCrystal
    {
        public const int
            WIDTH = 24,
            HEIGHT = 26;


        public PrimalCrystal() : base(
            new[]
            {
                (GameCulture.English, "Primal Crystal", "Primal"),
                (GameCulture.French, "Cristal Primitif", "Primitif") // TODO Check if this is the correct translation.
            },
            WIDTH, HEIGHT)
        {
        }

        public PrimalCrystal((GameCulture culture, string displayName, string tooltip)[] strings, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White, int maxStack = 1) : base(strings, width, height, value, defense, rarity, maxStack)
        {
        }


        public override int EssenceCapacity => 1;
        public override Essence.Levels MaxEssenceLevel => Essence.Levels.PRIMAL;

        public new List<Essence> Essences = new List<Essence>();
    }
}
