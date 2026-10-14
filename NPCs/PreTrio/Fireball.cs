using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    public class Fireball : ModProjectile
    {
        public int spawned = 0;
        public int delay = 0;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("a Fireball");
        }

        public override void SetDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            delay = 0;
            spawned = 0;
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.alpha = 0;
            Projectile.timeLeft = 240;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            
        }

        public override void AI()
        {
            bitherial = true;
            Vector2 newVel = -Projectile.velocity / 2f;
            float theta = Main.rand.Next(-30, 30) * (float)Math.PI / 180;
            float mag = (float)Main.rand.NextDouble() * 2;
            newVel.X += (float)Math.Cos(theta) * mag;
            newVel.Y += (float)Math.Sin(theta) * mag;
            Dust.NewDust(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SuperMagma>(), newVel.X, newVel.Y);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 1.57f;
            if (spawned == 0) { 
            Vector2 move = Vector2.Zero;
            float distance = 1400f;
            bool target = false;
            for (int k = 0; k < 8; k++)
            {
                if (Main.player[k].active)
                {
                    Vector2 newMove = Main.player[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                Projectile.velocity = (16 * Projectile.velocity + move) / 11f;
                AdjustMagnitude(ref Projectile.velocity);
            }
                Projectile.velocity *= 28;
                spawned = 1;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 2)
            {
                Projectile.frame = 0;
            }
        }



        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
                target.AddBuff(BuffID.OnFire, 4 * 60, true);
        }
    }
}