using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class Spiritball : ModProjectile
    {
        bool back = true;
        public override void SetDefaults()
        {
            back = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Vector2 targetVel = Projectile.velocity;
            if(!back)
            {
                targetVel = (Main.player[Projectile.owner].Center - Projectile.Center);
                targetVel.Normalize();
                targetVel *= 20;
            }
            Projectile.velocity = .9f * Projectile.velocity + .1f * targetVel;
            if(Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) <= Projectile.velocity.Length())
                back = true;
            if (Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) > 600)
                back = false;
        }
    }
}