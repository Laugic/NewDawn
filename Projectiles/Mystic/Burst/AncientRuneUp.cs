using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Burst
{
	public class AncientRuneUp : ModProjectile
	{

        public bool bitherial = true;
        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 16;
			Projectile.height = 16;
			//projectile.alpha = 255;
            Projectile.timeLeft = 45;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            bitherial = true;
            if (Main.rand.Next(4) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sandy>(), Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.5f);
            Projectile.rotation += 0.02f;

        }

    }
}