using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
	public class Sandnado : ModProjectile
	{
        int delay = 0;
        float theta = 0;
        int num = 1;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandnado");
		}

		public override void SetDefaults()
        {
            num = 1;
            delay = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 160;
			Projectile.height = 42;
            Projectile.timeLeft = 7 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
        }

		public override void AI()
        {
            if(Projectile.ai[1] == 1)
                Movement();
            else
                MovementAnimation();
            FrameAnimation();
            SpawnSandnadoLayers();
        }

        private void Movement()
        {
            if (Projectile.Center.X < Main.player[Projectile.owner].Center.X - 4)
                Projectile.velocity.X = 4;
            else if (Projectile.Center.X > Main.player[Projectile.owner].Center.X + 4)
                Projectile.velocity.X = -4;
            else
                Projectile.velocity.X = 0;
            if(Projectile.Center.Y < Main.player[Projectile.owner].Center.Y + 12)
                Projectile.velocity.Y = 2;
            else if (Projectile.Center.Y > Main.player[Projectile.owner].Center.Y - 20)
                Projectile.velocity.Y = -1;
            else
                Projectile.velocity.Y = 0;
        }

        private void MovementAnimation()
        {
            Projectile.scale = Projectile.ai[1] / 4f + .5f;
            theta += (float)Math.PI / 60;
            Projectile.position.Y = Main.projectile[(int)Projectile.ai[0]].position.Y - Projectile.height * (Projectile.ai[1] - 1) + 1;
            Projectile.position.X = Main.projectile[(int)Projectile.ai[0]].position.X + (float)Math.Cos(theta) * 12 * (Projectile.ai[1] - 1);
            if (!Main.projectile[(int)Projectile.ai[0]].active)
                Projectile.Kill();
        }
        
        private void FrameAnimation()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 5)
            {
                Projectile.frame = 0;
            }
        }

        private void SpawnSandnadoLayers()
        {
            if(num < 5)
            {
                delay++;
                if (delay > 15)
                {
                    delay = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Projectile.Center.X, Projectile.Center.Y - Projectile.height * Projectile.scale), new Vector2(0, 0), ModContent.ProjectileType<SandnadoUp>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI, num + 1);
                    num++;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.Y = -26f;
        }
    }
}