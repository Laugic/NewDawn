using Terraria.ModLoader;

namespace NewDawn.Projectiles.Magic
{
	public class ElectrosparkP2 : ModProjectile
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electrospark");
		}

		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
            Projectile.timeLeft = 80;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 4;
        }
    }
}