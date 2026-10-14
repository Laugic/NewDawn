using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Thrown
{
	public class ViciousAssassinShard : ModProjectile
    {
        public int delay = 0;

		public override void SetDefaults()
        {
            delay = 0;
            Projectile.width = 16;               
			Projectile.height = 16;              
			Projectile.aiStyle = -1;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Ranged;           
			Projectile.penetrate = -1;           
			Projectile.timeLeft = 240;            
			Projectile.ignoreWater = true;         
			Projectile.tileCollide = false;          
		}

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if(Main.rand.Next(4) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), 0, 0);
            Vector2 move = Vector2.Zero;
            float distance = 1400f;
            Vector2 newMove = Main.MouseWorld - Projectile.Center;
            float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
            move = newMove;
            distance = distanceTo;
            AdjustMagnitude(ref move);
            Projectile.velocity = (20 * Projectile.velocity + move) / 11f;
            AdjustMagnitude(ref Projectile.velocity);
            
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }
    }
}
