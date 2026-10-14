using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
    public class ElderanClaymoreProjectile : ModProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;
        Vector2 targetPos;

        public override void SetDefaults()
        {
            reverse = false;
            delay = 20;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 400;
            targetPos = Projectile.position;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 toPlayer = player.Center - Projectile.Center;

            Projectile.rotation = toPlayer.ToRotation() - 1.57f;

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
                if (Projectile.Distance(player.Center) > 1000)
                    reverse = true;
                else
                {
                    Projectile.ai[0] += .015f;
                    Projectile.velocity.Y += Projectile.ai[0];
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(!reverse)
            {
                reverse = true;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            Projectile.tileCollide = false;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(ModContent.BuffType<Steamy>(), 120);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            NewDawn.DrawChain(Main.spriteBatch, ModContent.Request<Texture2D>("Projectiles/Melee/ElderanClaymoreChain").Value, Main.player[Projectile.owner].MountedCenter, Projectile.Center);
            /*Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 center = projectile.Center;
            Vector2 distToProj = playerCenter - projectile.Center;
            float projRotation = distToProj.ToRotation() + 1.57f;
            float distance = distToProj.Length();
            while (distance > 30f && !float.IsNaN(distance))
            {
                distToProj.Normalize();
                distToProj *= 24f;
                center += distToProj;
                distToProj = playerCenter - center;
                distance = distToProj.Length();
                Color drawColor = lightColor;

                //Draw chain
                Texture2D chainTexture = ModContent.Request<Texture2D>("Projectiles/Melee/ElderanClaymoreChain").Value;
                spriteBatch.Draw(chainTexture, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                    new Rectangle(0, 0, chainTexture.Width, chainTexture.Height), drawColor, projRotation,
                    new Vector2(chainTexture.Width * 0.5f, chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            }*/
            return true;
        }
    }
}