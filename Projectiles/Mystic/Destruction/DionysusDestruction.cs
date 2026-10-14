using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class DionysusDestruction : DestructionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.scale = 2f;
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = 20;
			height = 20;
			return true;
		}

        public override void AI()
        {
            Projectile.velocity.Y += .4f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + .785f;
        }

		public override void OnKill(int timeLeft)
		{
			if (Main.myPlayer == Projectile.owner)
			{
               Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DionysusExplosion>(), (int)Projectile.damage, 3f, Main.myPlayer);
			}
		}
    }
}