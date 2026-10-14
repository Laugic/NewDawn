using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;

namespace NewDawn.Projectiles.Accessory
{
    public class SteamTrailProj : ModProjectile
    {
        int delay = 0;
        int direction = 0;
        int frame = -1;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steam");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = .5f;
            delay = 20;
            Main.projFrames[Projectile.type] = 3;
        }

        public override void AI()
        {
            if (frame == -1)
                frame = Main.rand.Next(3);
            Projectile.frame = frame;
            if (delay <= 0)
            {
                Projectile.scale *= 1.01f;
                if (Projectile.scale > 1.5f)
                    Projectile.alpha += 2;
                if (Projectile.alpha > 250)
                    Projectile.Kill();
            }
            else
            {
                delay--;
            }
            if (direction == 0)
            {
                if (Main.rand.Next(2) == 0)
                    direction = -1;
                else
                    direction = 1;
            }
            Projectile.velocity.Y -= .02f;
            Projectile.rotation += (float)(Math.PI / 60) * direction;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 5 * 60);
        }
    }
}
