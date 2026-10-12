using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class CharonConjuration : PrimaryConjurationProjectile
    {
        public float mag = 0;
        Vector2 origin;
        double theta = 0;
        bool spawned = false;
        public override void SetDefaults()
        {
            theta = 0;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            spawned = false;
        }
        public override void AI()
        {
            if(!spawned)
            {
                spawned = true;
                theta = Main.rand.NextDouble() * Math.PI * 2;
                origin = Projectile.position;
                mag = 120;
            }

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Black>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            theta += Math.PI / 60;
            var targetPos = new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag) + origin;
            Projectile.velocity = Projectile.DirectionTo(targetPos) * Projectile.Distance(targetPos) / 4;
        }
    }
}