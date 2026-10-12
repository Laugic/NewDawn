using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
	public class BrassArrowProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brass Arrow");     
		}
        
        public override void SetDefaults()
        {
            Projectile.width = 18;               
			Projectile.height = 18;              
			Projectile.aiStyle = 1;             
			Projectile.friendly = true;         
			Projectile.hostile = false;         
			Projectile.DamageType = DamageClass.Ranged;           
			Projectile.penetrate = 3;           
			Projectile.timeLeft = 600;            
			Projectile.ignoreWater = true;         
			Projectile.tileCollide = true;          
			//aiType = 1;           
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            Projectile.penetrate--;
            Vector2 targetPos;
            targetPos.X = Main.MouseWorld.X;
            targetPos.Y = Main.MouseWorld.Y;
            Projectile.velocity = Projectile.DirectionTo(targetPos) * 22f;

            return false;
		}
        

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 targetPos;
            targetPos.X = Main.MouseWorld.X;
            targetPos.Y = Main.MouseWorld.Y;
            Projectile.velocity = Projectile.DirectionTo(targetPos) * 22f;
        }
    }
}
