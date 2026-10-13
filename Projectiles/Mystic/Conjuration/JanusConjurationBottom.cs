using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class JanusConjurationBottom : PrimaryConjurationProjectile
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
            Projectile.timeLeft = 12 * 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            FrameAnimation();
            SpawnSandnadoLayers();
        }

        private void FrameAnimation()
        {
            Projectile.frameCounter++;
            Projectile.scale = .8f;
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
            if (num < 8)
            {
                delay++;
                if (delay > 5)
                {
                    delay = 0;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Projectile.Center.X, Projectile.Center.Y - Projectile.height * Projectile.scale), new Vector2(0, 0), ModContent.ProjectileType<JanusConjurationUp>(), Projectile.damage, 0, Projectile.owner, Projectile.whoAmI, num + 1);
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