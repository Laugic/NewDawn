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
    public class BigSnowballProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {
            Projectile.velocity.Y += .4f;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 33, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            int shards = Main.rand.Next(2, 5);
            if (Projectile.ai[0] > 0 && Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < shards; i++)
                {
                    var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    var newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 12;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position, newVel, (int)Projectile.ai[0], Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}