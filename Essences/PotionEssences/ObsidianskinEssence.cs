using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace NewDawn.Essences.PotionEssences
{
    public class ObsidianskinEssence : PotionEssence
    {
        public ObsidianskinEssence()
        {
            Name = "Obsidianskin";
            EffectDescription = "Grants immunity to lava an fire";
            EssenceColor = new Color(.6f, 0f, .8f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[BuffID.OnFire] = true;
        }
    }
}
