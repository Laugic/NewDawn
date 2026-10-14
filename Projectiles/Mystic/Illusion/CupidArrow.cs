using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class CupidArrow : IllusionProjectile
    {
        bool justSpawned = false;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Heartburn Arrow");     
		}

		public override void SetDefaults()
        {
            justSpawned = false;
            Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 600;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
            buffID = ModContent.BuffType<Lovestruck>();
        }

        public override void AI()
        {
            if (!justSpawned)
            {
                justSpawned = true;
                Vector2 targetPos;
                targetPos.X = Main.MouseWorld.X;
                targetPos.Y = Main.MouseWorld.Y;
                Projectile.velocity = Projectile.DirectionTo(targetPos) * 12f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.Kill();
			return false;
		}
    }
}
