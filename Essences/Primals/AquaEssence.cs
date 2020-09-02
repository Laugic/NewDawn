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
            Name = "Aqua";
            Description = "The essence of water";
            EffectDescription = "'Fluidity'";
            EssenceColor = new Color(0f, .75f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.gills = true;
        }
    }
}
