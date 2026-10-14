using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Burst
{
	public class EruptionBurst : ModProjectile
	{
        public bool bitherial = true;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eruption Burst");
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 16;
			Projectile.height = 16;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

		public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.5f);
            
            Projectile.rotation += 0.02f;
            NewDawnPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<NewDawnPlayer>();
            global::NewDawn.Players.MysticPlayer mysticPlayer = Main.player[Projectile.owner].GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (Main.rand.Next(2) == 0 && Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X + -5+Main.rand.Next(0,11), -Main.rand.Next(5,10),  ModContent.ProjectileType<EruptionBurstUp>(), (int)(30 * mysticPlayer.MysticDamage * mysticPlayer.MysticBurstDamage), 3, Main.myPlayer);
            
        }
        
    }
}