using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.SoulStone
{
    public class ShadowDoubleProj : ModProjectile
    {
        float theta = 0;
        bool collided = false;

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10 * 60;
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
                Color color = Color.White * 0.15f;
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void AI()
        {
            for(int i = 0; i < 2; i++)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Black>(), 0, -Main.rand.NextFloat() * 2 - 2);

            float dist = Vector2.Distance(Main.player[Projectile.owner].Center + Main.player[Projectile.owner].velocity * 20, Projectile.Center);
            if(dist != 0)
            {
                float tVel = dist / 5;
                Projectile.velocity = Projectile.DirectionTo(Main.player[Projectile.owner].Center + Main.player[Projectile.owner].velocity * 20) * tVel;
            }
            if (Projectile.velocity.X > 0)
                Projectile.spriteDirection = -1;
            else
                Projectile.spriteDirection = 1;
        }
    }
}