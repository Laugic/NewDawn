using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Essences.Primals
{
    public class IgnisEssence : Essence
    {
        public IgnisEssence()
        {
            Name = "Ignis";
            Description = "The essence of fire";
            EffectDescription = "'Pure heat'";
            EssenceColor = new Color(1f, .4f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.AddBuff(BuffID.OnFire, 2, true);
        }
    }
}
