using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
	public class ViciousAssassinMain : ModProjectile
	{
        public bool spawned = false;
        private float _vMag = 16f;
        private float _vAccel = .4f;
        private float _vMax = 22f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vicious Assassin");
		}

		public override void SetDefaults()
		{
            _vMag = 16f;
            spawned = false;
			Projectile.width = 48;
			Projectile.height = 48;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
        }

        public override void AI()
        {
            Projectile.rotation += 1.57f / 20;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), 0, 0);
            Vector2 targetPos = Main.MouseWorld;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            float tVel = dist / 15;
            if (_vMag < _vMax && _vMag < tVel)
            {
                _vMag += _vAccel;
            }

            if (_vMag > tVel)
            {
                _vMag = tVel;
            }

            if (dist != 0)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * _vMag;
            }
        }

        public override void OnKill(int timeLeft)
        {
            int damage = Projectile.damage;
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 7, 0, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -7, 0, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 7, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -7, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, 5, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, -5, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, -5, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, 5, ModContent.ProjectileType<ViciousAssassinShard>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
            }
        }
    }
}