using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
	public class HolyOrigin : ModProjectile
	{
        public int delay = 0;
		public override void SetDefaults()
		{
            delay = 0;
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 0;
			Projectile.scale = 1f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.scale = 2;
		}
		
		public override void AI()
		{
            delay--;
            if(delay <= 0)
            {
                delay = 4;
                if(Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X, Projectile.position.Y, 0, 48, ModContent.ProjectileType<HolyStrike>(), (int)(Projectile.damage), 3, Main.myPlayer);
            }
        }
	}
}