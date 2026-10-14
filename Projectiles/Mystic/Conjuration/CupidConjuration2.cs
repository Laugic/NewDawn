namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class CupidConjuration2 : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
    }
}