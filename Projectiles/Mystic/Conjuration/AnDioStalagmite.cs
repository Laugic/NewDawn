using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class AnDioStalagmite : ConjurationProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Loki Stalagmite");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 60;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 2;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.velocity.Y = Math.Max(-10, Projectile.velocity.Y - .6f);
        }
    }
}