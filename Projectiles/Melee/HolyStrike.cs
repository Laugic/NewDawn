using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
	public class HolyStrike : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 64;               
			Projectile.height = 64;              
			Projectile.aiStyle = -1;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Melee;           
			Projectile.penetrate = -1;           
			Projectile.timeLeft = 60;            
			Projectile.ignoreWater = true;         
			Projectile.tileCollide = false;
            Projectile.scale = 3f;      
		}

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 255, 255, 255);
        }
    }
}
