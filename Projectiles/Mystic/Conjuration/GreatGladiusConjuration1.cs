using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class GreatGladiusConjuration1 : ConjurationProjectile
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Great Gladius Conjuration");
		}

		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 2;
            Projectile.timeLeft = 6 * 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        
        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
            {
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - 64, Projectile.Center.Y, -4 + Main.rand.Next(9), -Main.rand.Next(6, 9), ModContent.ProjectileType<GreatGladiusConjuration2>(), (int)(Projectile.damage) / 4, 3, Main.myPlayer);
            }
        }
    }
}