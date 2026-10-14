using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Bosses
{
    public class ElectroKnowledge : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electro Knowledge");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 8 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            if(Projectile.ai[1] > 45)
            {
                Projectile.ai[1] = 0;
                if(Main.netMode != 1)
                {
                    float numBalls = 4;
                    double thetaInit = 0;
                    for (int i = 0; i < numBalls; i++)
                    {
                        float mag = 6 + Main.rand.NextFloat() * 4;
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, mag * (float)Math.Cos(thetaInit + (Math.PI * 2) * (i / numBalls)), mag * (float)Math.Sin(thetaInit + (Math.PI * 2) * (i / numBalls)),
                                ModContent.ProjectileType<KnowledgeBolt>(), (int)(Projectile.damage), 3, 0, .4f);
                    }
                }
            }
            if (Main.rand.Next(2) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), -Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 2 * 60 + Main.rand.Next(60), false);
        }
    }
}