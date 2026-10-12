using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Ranged
{
    public class Hailball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] > 6)
            {
                Projectile.ai[1] = 0;
                if (Projectile.ai[0] > 0 && Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Main.player[Projectile.owner].GetSource_FromAI(), Projectile.position, new Vector2(0, 4), (int)Projectile.ai[0], Projectile.damage, Projectile.knockback, Projectile.owner);
            }
            var multiplier = Math.Min(300 - Projectile.timeLeft, 30) / 30f;
            Projectile.velocity.Y += .5f * multiplier;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            return false;
        }
    }
}
