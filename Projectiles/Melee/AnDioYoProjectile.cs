using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
	public class AnDioYoProjectile : ModProjectile
    {
        public int reload = 60;
        public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 9f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 280f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
            reload = 60;
		}

        public Vector2 GetPosition()
        {
            return Projectile.position;
        }

		public override void PostAI()
        {
            reload -= 1;
            if (reload <= 0)
            {
                reload = 30;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 4 - Main.rand.Next(8), 4 - Main.rand.Next(8), ModContent.ProjectileType<AnDioYoShot>(), Projectile.damage, 3f, Main.myPlayer);
            }
        }
	}
}
