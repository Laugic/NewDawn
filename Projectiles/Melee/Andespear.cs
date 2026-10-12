using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
    public class Andespear : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Andespear");
        }
        public override void SetDefaults()
        {
            Projectile.height = 74;
            Projectile.width = 74;  
			Projectile.aiStyle = 19;
			Projectile.DamageType = DamageClass.Melee;  
			Projectile.timeLeft = 90;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
        }

        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        public override void AI()
        {
            //Thanks ExampleMod :thumbsup:
            
            Player projOwner = Main.player[Projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
            Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f) 
                {
                    MovementFactor = 3f; 
                    Projectile.netUpdate = true; 
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) 
                {
                    MovementFactor -= 2.4f;
                }
                else 
                {
                    MovementFactor += 2.1f;
                }
            }
            
            Projectile.position += Projectile.velocity * MovementFactor;
            
            if (projOwner.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            /*
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= MathHelper.ToRadians(90f);
            }
            */
            
        
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f - (float)Math.PI / 2;
        	if(Projectile.spriteDirection == -1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
        }
    }
}