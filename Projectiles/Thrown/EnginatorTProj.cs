using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class EnginatorTProj : ModProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;

        public override void SetDefaults()
        {
            reverse = false;
            delay = 20;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 400;
            Main.projFrames[Projectile.type] = 2;
        }
        
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Projectile.velocity.X < 0) Projectile.frame = 1;
            else Projectile.frame = 0;

            delay -= 1;
            if(delay <= 0 && reverse == false)
            {
                Projectile.velocity.X *= .95f;
                Projectile.velocity.Y *= .95f;
                if (Math.Abs(Projectile.velocity.X) < 4f && Math.Abs(Projectile.velocity.Y) < 4f)
                {
                    Projectile.velocity.X = -Projectile.velocity.X;
                    Projectile.velocity.Y = -Projectile.velocity.Y;
                    reverse = true;
                }
            }
            if (reverse)
            {
                if (Projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref Projectile.velocity);
                    Projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 1400f;
                bool target = false;
                
                    Vector2 newMove = Main.player[Main.myPlayer].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                    
                
                if (target)
                {
                    AdjustMagnitude(ref move);
                    Projectile.velocity = (20 * Projectile.velocity + move) / 5f;
                    AdjustMagnitude(ref Projectile.velocity);
                }

                Vector2 delta = Main.player[Main.myPlayer].Center - Projectile.Center;
                if (Math.Abs(delta.X) < 8 && Math.Abs(delta.Y) < 8)
                    Projectile.Kill();
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 10f)
            {
                vector *= 10f / magnitude;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            reverse = true;
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.tileCollide = false;
            return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 120);       //Add Onfire buff to the NPC for 1 second
        }
    }
}