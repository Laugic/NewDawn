using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Magic
{
	public class ElectrosparkProjectile : ModProjectile
	{
        private int delay = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electrospark");
		}

		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
            Projectile.timeLeft = 240;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            delay = 0;
        }

        public override void AI()
        {
            delay += 1;
            if(delay == 25 && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X + 3, Projectile.velocity.Y + 3, ModContent.ProjectileType<ElectrosparkP2>(), Projectile.damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X + 3, Projectile.velocity.Y - 3, ModContent.ProjectileType<ElectrosparkP2>(), Projectile.damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X - 3, Projectile.velocity.Y - 3, ModContent.ProjectileType<ElectrosparkP2>(), Projectile.damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X - 3, Projectile.velocity.Y + 3, ModContent.ProjectileType<ElectrosparkP2>(), Projectile.damage, 3f, Main.myPlayer);
            }
        }
    }
}