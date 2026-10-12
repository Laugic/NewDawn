using NewDawn.Buffs;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class CupidIllusion : IllusionProjectile
    {
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heartburn Arrow");     
		}

		public override void SetDefaults()
        {
            Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
            buffID = ModContent.BuffType<Lovestruck>();
        }
    }
}
