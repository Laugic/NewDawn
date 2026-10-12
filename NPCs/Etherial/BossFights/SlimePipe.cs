using Terraria;
using Terraria.ModLoader;
using System;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class SlimePipe : ModProjectile
    {
        private bool spawned = false;

        public override void SetDefaults()
        {
            spawned = false;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 3;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.ai[1] += .1f;
            Projectile.velocity.Y += Projectile.ai[1];
        }
    }
}