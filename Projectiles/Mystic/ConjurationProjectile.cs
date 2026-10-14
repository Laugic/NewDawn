using Terraria;

namespace NewDawn.Projectiles.Mystic
{
    public abstract class ConjurationProjectile : MysticProjectile
    {
    }

    public abstract class PrimaryConjurationProjectile : ConjurationProjectile
    {
        public override void Durationed(float dur)
        {
            Projectile.timeLeft = (int)(Projectile.timeLeft * dur);
        }
    }
}
