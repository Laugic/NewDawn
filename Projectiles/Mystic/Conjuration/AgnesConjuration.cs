using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class AgnesConjuration : PrimaryConjurationProjectile
    {
        int counter = 0;
        Vector2 oldVel;
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8 * 60;
            Projectile.tileCollide = true;
            counter = 0;
        }

        public override void AI()
        {
            if (Projectile.velocity.Length() > .1f)
            {
                oldVel = Projectile.velocity;
                oldVel.Normalize();
                oldVel *= 6;
                Projectile.velocity *= .95f;
            }
            else
                Projectile.velocity *= 0;
            Projectile.rotation = (float)Math.Atan2((double)oldVel.Y, (double)oldVel.X) + (1.57f / 2f);
            Hail();
        }

        private void Hail()
        {
            counter++;
            if (counter > 30)
            {
                counter = 0;
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, new Vector2(oldVel.X, oldVel.Y), ModContent.ProjectileType<AgnesConjuration2>(), Projectile.damage, 0, Projectile.owner);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int imageHeight = TextureAssets.Projectile[Projectile.type].Value.Height;
            int y6 = imageHeight * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, imageHeight)), Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)imageHeight / 2f) + new Vector2(16, -16), Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}