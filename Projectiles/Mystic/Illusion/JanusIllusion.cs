using Microsoft.Xna.Framework;
using NewDawn.Buffs;
using NewDawn.Dusts;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class JanusIllusion : IllusionProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Janus Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            buffID = ModContent.BuffType<SandyBuff>();
        }
        public override void AI()
        {
            Vector2 newVel = -Projectile.velocity * 2 / 3f;
            float theta = Main.rand.Next(-30, 30) * (float)Math.PI / 180;
            float mag = Main.rand.Next(3, 7);
            newVel.X += (float)Math.Cos(theta) * mag;
            newVel.Y += (float)Math.Sin(theta) * mag;
            Dust.NewDust(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), newVel.X, newVel.Y);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Burst(player.Center);
            Vector2 dist = Projectile.Center - player.Center;
            if (player.active && dist.Length() < 2000)
            {
                var telepos = Projectile.position;
                telepos.Y -= 32;
                player.Teleport(telepos);
                Burst(player.Center);
                player.immune = true;
                player.immuneTime = 15;
            }
        }

        private void Burst(Vector2 pos)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 20; i++)
                {
                    var theta = (float)(Math.PI * -Main.rand.NextDouble());
                    Vector2 newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 12;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(pos.X, pos.Y), new Vector2(newVel.X, newVel.Y), ModContent.ProjectileType<JanusIllusionSandball>(), Projectile.damage / 2, 3, Main.myPlayer);
                }
            }
        }
    }
}
