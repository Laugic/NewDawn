using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Ranged
{
	public class LuminarrowHead : ModProjectile
	{
		public override void SetDefaults()
		{
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
        }
    }
}
