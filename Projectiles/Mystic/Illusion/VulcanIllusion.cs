using NewDawn.Buffs;
using Terraria;
using NewDawn.Dusts;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class VulcanIllusion : IllusionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            buffID = ModContent.BuffType<Steamy>();
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }
        
    }
}