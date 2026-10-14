using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
	public class RockFalling : ModProjectile
	{
        public int delay = 0;
        public int delMax = 200;
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Falling Rock");
		}

		public override void SetDefaults()
        {
            //NewDawnVars.eProjectiles.Add(projectile.type);
            bitherial = true;
            delMax = 0;
            delay = 0;
            Projectile.width = 32;
			Projectile.height = 32;
			//projectile.alpha = 255;
            Projectile.timeLeft = 500;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //projectile.rotation = (float)(Main.rand.Next(5) * 3.14 / 4);
        }

		public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, 0f, 0f);

            if (delMax == 0)
                delMax = 20 + 20 * Main.rand.Next(0, 3);
            Projectile.rotation += 0.02f;
            Projectile.velocity.Y += .04f;
            if(Main.expertMode)
                delay += 1;
            if(delay >= delMax && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 7, 0, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -7, 0, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 7, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -7, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, 5, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, -5, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, -5, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, 5, ModContent.ProjectileType<MiniRock>(), (int)(Projectile.damage / 1.27f), 3, Main.myPlayer);
                Projectile.Kill();
            }
        }
        
    }
}