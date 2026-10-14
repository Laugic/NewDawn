using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{
	public class DaggorShard : ModProjectile
	{
        public int delay = 0;
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daggor Shard");
		}

		public override void SetDefaults()
		{
            delay = 0;
			Projectile.width = 16;
			Projectile.height = 16;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.minion = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .1f;
        }
        
    }
}