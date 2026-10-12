using Terraria;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class VulcanConjuration2 : ConjurationProjectile
    {
        private int delay = 0;
        private int delMax = 0;

		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vulcan Conjuration");
		}

		public override void SetDefaults()
		{
            delay = 0;
            delMax = 0;
			Projectile.width = 22;
			Projectile.height = 22;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        
        public override void AI()
        {
            float mag = 12f;
            float theta = (float)Main.rand.Next(8, 15) / 7;
            bool stopped = false;
            Projectile.velocity.X *= .9f;
            Projectile.velocity.Y *= .9f;
            if (Math.Abs(Projectile.velocity.X) <= .2 && Math.Abs(Projectile.velocity.Y) <= .2)
            {
                stopped = true;
            }
            if (delMax == 0)
                delMax = Main.rand.Next(60);
            if(stopped)
                delay++;
            if(delay >= delMax)
            {
                SoundEngine.PlaySound(SoundID.Item64, Projectile.position);
                float vX = mag * (float)Math.Cos(theta);
                float vY = -mag * (float)Math.Sin(theta);
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<VulcanConjuration3>(), (int)(Projectile.damage), 3, Main.myPlayer);
                Projectile.Kill();
            }
        }
    }
}