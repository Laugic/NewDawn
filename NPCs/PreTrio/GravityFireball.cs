using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    public class GravityFireball : ModProjectile
    {
        public int delay = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("a Fireball");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.alpha = 0;
            Projectile.timeLeft = 360;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.scale = 1f + 2 * Main.rand.NextFloat();
        }

        public override void AI()
        {
            Vector2 newVel = -Projectile.velocity * 2 / 3f;
            float theta = Main.rand.Next(-30, 30) * (float)Math.PI / 180;
            float mag = Main.rand.Next(2, 4);
            newVel.X += (float)Math.Cos(theta) * mag;
            newVel.Y += (float)Math.Sin(theta) * mag;
            if (Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SuperMagma>(), newVel.X, newVel.Y);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) - 1.57f;
            delay++;
            if (delay > 30)
                Projectile.velocity.Y += .1f;

            /*
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }*/
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 4 * 60, true);
        }
    }
}