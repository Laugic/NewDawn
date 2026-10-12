using Terraria;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class ConjurationProjectile : MysticProjectile
    {
    }

    public class PrimaryConjurationProjectile : ConjurationProjectile
    {
        public override void Durationed(float dur)
        {
            Projectile.timeLeft = (int)(Projectile.timeLeft * dur);
        }
    }
}
