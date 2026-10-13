using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Magic
{
    public class BookSandstormBottom : ModProjectile
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
            Projectile.height = 21;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Movement();
            FrameAnimation();
            SpawnSandnadoLayers();
        }

        private void Movement()
        {
            if(Projectile.velocity.X < 0)
            {
                if (Projectile.velocity.X > -4)
                    Projectile.velocity.X *= 1.1f;
            }
            else
            {
                if (Projectile.velocity.X < 4)
                    Projectile.velocity.X *= 1.1f;
            }
            Projectile.velocity.Y *= .9f;
        }

        private void MovementAnimation()
        {
            Projectile.scale = (Projectile.ai[1] / 4f + .5f) / 2;
            theta += (float)Math.PI / 60;
            Projectile.position.Y = Main.projectile[(int)Projectile.ai[0]].position.Y - Projectile.height * (Projectile.ai[1] - 1) + 1;
            Projectile.position.X = Main.projectile[(int)Projectile.ai[0]].position.X + (float)Math.Cos(theta) * 12 * (Projectile.ai[1] - 1);
            if (!Main.projectile[(int)Projectile.ai[0]].active)
                Projectile.Kill();
        }

        private void FrameAnimation()
        {
            Projectile.frameCounter++;
            Projectile.scale = .5f;
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
            if (num < 5)
            {
                delay++;
                if (delay > 5)
                {
                    delay = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Projectile.Center.X, Projectile.Center.Y - Projectile.height * Projectile.scale), new Vector2(0, 0), ModContent.ProjectileType<BookSandstormUp>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI, num + 1);
                    num++;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if(target.noGravity == false)
                target.velocity.Y = -12f;
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}