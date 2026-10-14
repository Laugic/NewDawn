using NewDawn.Buffs;
using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{
	public class ShroomProjectile : ModProjectile
	{
        public int dir = 0;
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroom");
		}

		public override void SetDefaults()
		{
            dir = 0;
			Projectile.width = 12;
			Projectile.height = 12;
            Projectile.timeLeft = 360;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            NewDawnVars.ShroomProjectiles.Add(Projectile.type);
        }

        public override void AI()
        {
            if(dir == 0)
            {
                if (Projectile.velocity.X < 0)
                    dir = -1;
                else
                    dir = 1;
            }
            if (Main.rand.Next(4) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<ShroomDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            if (Projectile.velocity.Y < 2)
                Projectile.velocity.Y += .1f;
            if(dir == 1)
            {
                if(Main.rand.Next(145) == 0)
                    dir = -1;
                if (Projectile.velocity.X < 2)
                    Projectile.velocity.X += .1f;
            }
            if (dir == -1)
            {
                if (Main.rand.Next(145) == 0)
                    dir = 1;
                if (Projectile.velocity.X > -2)
                    Projectile.velocity.X -= .1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Spored>(), 90, true);
        }
    }
}