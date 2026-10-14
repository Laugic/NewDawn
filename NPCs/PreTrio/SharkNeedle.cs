using System;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
	public class SharkNeedle : ModProjectile
	{
        public int grounded = 0;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shark Needle");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            grounded = 0;
            Projectile.width = 32;
			Projectile.height = 32;
			//projectile.alpha = 255;
            Projectile.timeLeft = 300;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

		public override void AI()
        {
            bitherial = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + .785f;

        }
        
    }
}