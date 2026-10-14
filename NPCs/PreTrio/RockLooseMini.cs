using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    public class RockLooseMini : ModProjectile
    {
        public int spawned = 0;
        public int delay = 0;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("a Loose Rock");
        }

        public override void SetDefaults()
        {
            bitherial = true;
            delay = 0;
            spawned = 0;
            Projectile.width = 32;
            Projectile.height = 32;
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
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, 0f, 0f);
            Projectile.rotation -= 0.02f;
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
                Projectile.velocity *= 24;
                spawned = 1;
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
            //NPCs.Slybertron.Slybertron.coginatorHits += 1;
            int debuff = BuffID.OnFire;
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }      //Add Onfire buff to the NPC for 1 second
        }
    }
}