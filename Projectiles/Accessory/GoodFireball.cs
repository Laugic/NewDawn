using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Accessory
{
    public class GoodFireball : ModProjectile
    {
        public bool stopped = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
        }

        public override void SetDefaults()
        {
            stopped = false;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Main.rand.Next((int)(8 / Projectile.scale)) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), -2 + Main.rand.NextFloat() * 4, -Main.rand.NextFloat() * 4);
            if (stopped)
            {
                Projectile.velocity.X *= .8f;
                Projectile.velocity.Y = 0;
                Projectile.scale *= .98f;
                if (Projectile.scale <= .05f)
                    Projectile.Kill();
                Projectile.rotation = 0;
            }
            else
            {
                Projectile.velocity.Y += Projectile.ai[0];
                Projectile.ai[0] += .01f;
                Projectile.velocity.X *= .99f;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - (float)Math.PI / 2;
            }

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            stopped = true;
            return false;
        }
    }
}
