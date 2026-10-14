using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class VulcanConjuration : ConjurationProjectile
    {
        public bool stopped = false;

        public override void SetDefaults()
        {
            stopped = false;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .2f;
        }

        public override void OnKill(int TimeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            if (Main.myPlayer == Projectile.owner)
            {
                for(int i = 0; i < 7; i++)
                {
                    float theta = (float)(Main.rand.Next(45)) / 7;
                    int mag = Main.rand.Next(6, 17);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(theta)*mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<VulcanConjuration2>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                }
            }
        }
        
    }
}