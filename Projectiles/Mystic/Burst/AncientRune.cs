using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Burst
{
	public class AncientRune : ModProjectile
	{
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandnado");
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 16;
			Projectile.height = 16;
			//projectile.alpha = 255;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            //projectile.rotation = (float)(Main.rand.Next(5) * 3.14 / 4);
            Projectile.penetrate = -1;
        }

		public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sandy>(), Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.5f);
            
            Projectile.rotation += 0.02f;
            NewDawnPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<NewDawnPlayer>();
            if (Main.rand.Next(4) == 0 && Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -4+Main.rand.Next(0,9), -Main.rand.Next(3,7),  ModContent.ProjectileType<AncientRuneUp>(), (int)(12 * modPlayer.MysticDamage * modPlayer.MysticBurstDamage), 3, Main.myPlayer);
            
        }
        
    }
}