using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Pets
{
	public class MagmaHeartProjectile : ModProjectile
    {

        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magma Heart");
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
        {
            vAccel = .1f;
            Projectile.width = 28;
			Projectile.height = 28;
			Projectile.penetrate = -1;
			Projectile.netImportant = true;
			Projectile.timeLeft *= 5;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.scale = 1f;
			Projectile.tileCollide = false;
		}
        

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (Main.rand.Next(30) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            if (!player.active)
			{
				Projectile.active = false;
				return;
			}
			if (player.dead)
			{
				modPlayer.obsHeart = false;
			}
			if (modPlayer.obsHeart)
			{
				Projectile.timeLeft = 2;
			}
            else
            {
                Projectile.active = false;
            }

            float mag = 20;
            Vector2 rot = Projectile.position;
            rot.X = -12 * player.direction;// (float)Math.Cos(modPlayer.theta / 60f) * mag;
            rot.Y = (float)Math.Sin(modPlayer.theta / 4f) * mag - 40;
            Vector2 targetPos = player.Center + rot;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            tVel = dist / 15;
            if (vMag < tVel)
            {
                vMag += vAccel;
            }

            if (vMag > tVel)
            {
                vMag = tVel;
            }

            if (dist != 0)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
            }

            Lighting.AddLight(Projectile.Center, .8f, .8f, .8f);
        }
	}
}