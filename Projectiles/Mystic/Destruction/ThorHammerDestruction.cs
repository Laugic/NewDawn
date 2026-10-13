using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Buffs;
using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class ThorHammerDestruction : DestructionProjectile
    {
        int delay = 0;

        public override void SetDefaults()
        {
            delay = 0;
            Projectile.width = 74;
            Projectile.height = 74;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 5;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, 74 * Projectile.frame, 74, 74), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
                return;
            }
        }

        public override void AI()
        {
            Projectile.rotation += .2f;
            delay++;
            if(delay > 10)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.velocity = Main.player[Projectile.owner].Center - Projectile.Center;
                    if (Projectile.velocity.Length() < 40)
                        Projectile.Kill();
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= 36f;
                }
            }
        }
    }
}
