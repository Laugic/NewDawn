using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class AmmoReservationEssence : PotionEssence
    {
        public AmmoReservationEssence()
        {
            Name = "Ammo Reservation";
            EffectDescription = "20% Chance not to consume ammo";
            EssenceColor = new Color(1f, .75f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.ammoPotion = true;
        }
    }
}
