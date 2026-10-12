using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class YellowIceBall : ModProjectile
    {
        public int delay = 0;

        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            delay = 0;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
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
            if(delay > 1 * 60)
                Projectile.ai[0] += .01f;
        }

        public override void PostAI()
        {
            Projectile.velocity.Y += Projectile.ai[0];
        }
        
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}