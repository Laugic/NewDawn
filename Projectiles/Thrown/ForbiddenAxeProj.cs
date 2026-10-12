using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class ForbiddenAxeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.velocity.X *= .99f;
            Projectile.velocity.Y += .2f;
            if (Projectile.velocity.X < 0)
                Projectile.spriteDirection = -1;
            Projectile.rotation += Projectile.velocity.X / 32;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            int numProjectiles = Main.rand.Next(3, 7);
            for (int i = 0; i < numProjectiles; i++)
            {
                float theta = Main.rand.NextFloat() * 2 * (float)Math.PI;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position, new Vector2((float)Math.Cos(theta) * 14f, (float)Math.Sin(theta) * 14f), ProjectileID.CrystalBullet, Projectile.damage, 3, Projectile.owner);
            }
            base.OnKill(timeLeft);
        }
    }
}