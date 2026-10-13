using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class HallowsEveDestruction1 : DestructionProjectile
    {
        bool justSpawned = false;
        int delay = 0;

        public override void SetDefaults()
        {
            delay = 0;
            justSpawned = false;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor * 0.35f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				if (Projectile.velocity.X < 0f)
				{
					Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
				}
				else
				{
					Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.FlipHorizontally, 0f);
				}
			}
			return true;
		}
		
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            
			if (!justSpawned)
			{
                justSpawned = true;
                for (int i = 0; i < 20; i++)
				{
					Vector2 dustPos = Vector2.UnitX * 0f;
					dustPos += -Vector2.UnitY.RotatedBy((double)((float)i * (6.28318548f / 20)), default(Vector2)) * new Vector2(3f, 8f);
					dustPos = dustPos.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int dust = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + dustPos;
					Main.dust[dust].velocity = Projectile.velocity * 0f + dustPos.SafeNormalize(Vector2.UnitY) * 1f;
				}
			}
			
			for (int i = 0; i < 2; i++)
			{
				float speedX = Projectile.velocity.X / 2.5f * (float)i;
				float speedY = Projectile.velocity.Y / 2.5f * (float)i;
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[dust].position.X = Projectile.Center.X - speedX;
				Main.dust[dust].position.Y = Projectile.Center.Y - speedY;
				Main.dust[dust].velocity *= 0f;
				Main.dust[dust].noGravity = true;
			}
			
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = -1;
			}
			else
			{
				Projectile.spriteDirection = 1;
			}
            delay++;
			if (Main.myPlayer == Projectile.owner && delay > 10)
            {
				Vector2 vec;
				vec.X = Main.MouseWorld.X;
				vec.Y = Main.MouseWorld.Y;
				
				float mag = 7.5f;
				
				float diffX = vec.X - Projectile.Center.X;
				float diffY = vec.Y - Projectile.Center.Y;
				float dist = (float)Math.Sqrt((double)(diffX * diffX + diffY * diffY));
				dist = mag / dist;
				diffX *= dist;
				diffY *= dist;
				
				Projectile.velocity.X = (Projectile.velocity.X * 20f + diffX) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + diffY) / 21f;
			}
        }
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 12; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 50, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
		}
    }
}