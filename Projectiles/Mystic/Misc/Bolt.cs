using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using NewDawn.Buffs;

namespace NewDawn.Projectiles.Mystic.Misc
{
	public class Bolt : MysticProjectile
    {
        Vector2 projGoal;
        int delay = 0;
        int nextBolt = 0;
        List<Vector2> Trails { get; set; }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
			Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 5;
            Trails = new List<Vector2>();
            projGoal = Projectile.Center;
            delay = 0;
            nextBolt = 0;
            Projectile.tileCollide = false;
            //buffID = ModContent.BuffType<CosmicDisarray>();
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Trails.Count == 0)
                Prime();

            BoltMove();

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 228, 0, 0);
        }

        private void BoltMove()
        {
            delay--;
            if (delay <= 0)
            {
                Trails.Add(Projectile.Center);

                if (Projectile.ai[0] != 0)
                {
                    nextBolt--;
                    if (nextBolt <= 0)
                        SpawnBolt();
                }

                Projectile.velocity = projGoal - Projectile.Center;
                Projectile.velocity.Normalize();
                Projectile.velocity *= 12;
                Projectile.velocity = Projectile.velocity.RotatedByRandom(3);
                var rand = new Random();
                delay = rand.Next(6, 16);
            }
        }

        private void SpawnBolt()
        {
            if(Main.myPlayer == Projectile.owner)
            {
                int p = Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X, Projectile.position.Y, Projectile.velocity.X, Projectile.velocity.Y, ModContent.ProjectileType<Bolt>(), (int)(Projectile.damage), 8, Main.myPlayer);
                Main.projectile[p].timeLeft = Projectile.timeLeft;
            }
            nextBolt = (int)Projectile.ai[0];
        }

        private void Prime()
        {
            Trails.Add(Projectile.Center);
            Projectile.velocity.Normalize();
            Projectile.velocity *= 12;
            projGoal = Projectile.Center + Projectile.velocity * 6000;

            Projectile.velocity = projGoal - Projectile.Center;
            Projectile.velocity.Normalize();
            Projectile.velocity *= 12;
            Projectile.velocity.RotatedByRandom(2);
            var rand = new Random();
            delay = rand.Next(6, 16);

            if (Projectile.ai[0] != 0)
                nextBolt = (int)Projectile.ai[0];

            if(Projectile.ai[1] == 1)
                buffID = ModContent.BuffType<ThunderCharged>();
        }

        public override void OnKill(int timeLeft)
		{

        }

        public override bool PreDraw(ref Color lightColor)
        {
            if(Trails.Count > 0)
            {
                var curPos = Trails[0];
                Vector2 nextPos = Projectile.Center;
                for (int i = 0; i < Trails.Count; i++)
                {
                    curPos = Trails[i];
                    if (i == Trails.Count - 1)
                        nextPos = Projectile.Center;
                    else
                        nextPos = Trails[i + 1];
                    NewDawn.DrawChain(spriteBatch, Mod.GetTexture("Projectiles/Mystic/Misc/BoltChain"), curPos, nextPos, .5f, Color.White);
                }
            }
            return true;
        }
    }
}