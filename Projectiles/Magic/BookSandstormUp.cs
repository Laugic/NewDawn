using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Magic
{
    public class BookSandstormUp : ModProjectile
    {
        int delay = 0;
        float theta = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandnado");
        }

        public override void SetDefaults()
        {
            delay = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 80;
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
            MovementAnimation();
            FrameAnimation();
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.noGravity == false)
                target.velocity.Y = -12f;
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}