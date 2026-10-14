using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Buffs;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class PoseidonIllusion : IllusionProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;
        Vector2 targetPos;
        int debugTimer = 0;
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 6 * 60;
            Projectile.ignoreWater = true;
            targetPos = Projectile.position;
            buffID = ModContent.BuffType<DepthBubbles>();
            debugTimer = 0;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!reverse)
            {
                reverse = true;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            Projectile.tileCollide = false;
            return false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + .785f;
            Player player = Main.player[Projectile.owner];
            Vector2 toPlayer = player.Center - Projectile.Center;

            if (reverse)
            {
                targetPos = player.Center;
                if (toPlayer.Length() < 20)
                    Projectile.Kill();
                toPlayer.Normalize();
                toPlayer *= 12;
                Projectile.velocity = toPlayer;
            }
            else
            {
                if (Projectile.Distance(player.Center) > 800)
                    reverse = true;
                else
                {
                    Projectile.ai[0] += .015f;
                    Projectile.velocity.Y += Projectile.ai[0];
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("NewDawn/Projectiles/Mystic/Illusion/PoseidonIllusion").Value;
            var origin = Projectile.Center;
            //origin.X -= texture.Width / 2;
            //origin.Y -= texture.Height / 2;
            NewDawn.DrawChain(Main.spriteBatch, ModContent.Request<Texture2D>("NewDawn/Projectiles/Mystic/Illusion/PoseidonChain").Value, Main.player[Projectile.owner].MountedCenter, Projectile.Center);
            Main.spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, Projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

    }
}