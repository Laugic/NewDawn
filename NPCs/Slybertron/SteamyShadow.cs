using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Slybertron
{
	public class SteamyShadow : ModProjectile
    {
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steamy Shadow");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 48;
			Projectile.height = 48;
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
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), 0f, 0f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), 0f, 0f);
            Projectile.rotation += 6;
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }
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
                Projectile.velocity = (20 * Projectile.velocity + move) / 6f;
                AdjustMagnitude(ref Projectile.velocity);
            }


        }


        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 8f / magnitude;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            //NPCs.Slybertron.Slybertron.coginatorHits += 1;
            int debuff = ModContent.BuffType<Steamy>();
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }      //Add Onfire buff to the NPC for 1 second
        }
    }
}