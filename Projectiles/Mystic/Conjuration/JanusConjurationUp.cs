using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class JanusConjurationUp : ConjurationProjectile
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
            Projectile.height = 32;
            Projectile.timeLeft = 30000;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Shoot();
            MovementAnimation();
            FrameAnimation();
        }

        private void Shoot()
        {
            if (Main.rand.Next(3 * 60) == 0 && Main.myPlayer == Projectile.owner)
            {
                var theta = (float)(Math.PI * -Main.rand.NextDouble());
                Vector2 newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                Vector2 pos = new Vector2(Projectile.Center.X - (Projectile.width / 2 * Projectile.scale) + Main.rand.Next((int)(Projectile.width * Projectile.scale)), Projectile.Center.Y);
                newVel *= 16;
                int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), pos.X, pos.Y, newVel.X, newVel.Y, ModContent.ProjectileType<JanusConjurationSandball>(), Projectile.damage, 3, Main.myPlayer);
            }
        }

        private void MovementAnimation()
        {
            Projectile.scale = (Projectile.ai[1] / 6f) + .5f;
            Projectile.width = (int)(156 * Projectile.scale);
            theta += (float)Math.PI / 60;
            Projectile.position.Y = Main.projectile[(int)Projectile.ai[0]].position.Y - (Projectile.height * (Projectile.ai[1] - 1));
            Projectile.position.X = Main.projectile[(int)Projectile.ai[0]].position.X + ((float)Math.Cos(theta) * 12 * (Projectile.ai[1] - 1));
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