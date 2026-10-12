using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Ranged
{
    public class BigHail : ModProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            delay = 0;
        }


        public override void AI()
        {
            delay++;
            if(delay > 15 || Main.rand.Next(30) == 0)
            {
                delay = 0;
                if(Projectile.ai[0] > 0 && Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position, new Vector2(0, 2), (int)Projectile.ai[0], Projectile.damage, Projectile.knockback, Projectile.owner);
            }
            Projectile.velocity.Y += .5f;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            return false;
        }
    }
}