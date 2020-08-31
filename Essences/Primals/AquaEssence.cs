using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Essences.Primals
{
    public class AquaEssence : Essence
    {
        public AquaEssence()
        {
            Description = "The essence of water.";
            EffectDescription = "Count as wet.";
            EssenceColor = Color.Blue;
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.statDefense += 4;
        }
    }
}
