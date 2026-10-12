using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Necrodon
{
    public class PhylacteryRotate : ModProjectile
    {
        private Vector2 origin;
        private bool Spawned = false;
        private int counter = 0;
        private float mag = 0;
        private float magGoal = 0;
        public float magCel = 0;
        private double theta = 0;
        private double thetaGoal = 0;
        private double thetCel = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("the Groove");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 16 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            origin = Projectile.position;
            magGoal = 80;
            counter = 0;
            magCel = 0;
            theta = thetaGoal = thetCel = 0;
            Spawned = false;
        }

        public override void AI()
        {
            float light = 0.8f;
            Lighting.AddLight(Projectile.Center + Projectile.velocity, light, light, light);
            if (!Spawned)
            {
                origin = Projectile.Center;
                counter = (int)Main.npc[(int)Projectile.ai[0]].ai[2];
                theta = thetaGoal = Math.PI * 2 * Projectile.ai[1] / 16;
                Spawned = true;
            }
            if (!(Main.npc[(int)Projectile.ai[0]].type == ModContent.NPCType<Phylactery>() && Main.npc[(int)Projectile.ai[0]].active))
            {
                Projectile.Kill();
                return;
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.position.Y - origin.Y, (double)Projectile.position.X - origin.X) - 1.57f;

            GetGoals();
            GetMovement();

            if (Projectile.timeLeft < 30)
            {
                Projectile.alpha += 4;
                Projectile.scale *= .98f;
            }
        }

        private void GetMovement()
        {
            if (mag < magGoal)
            {
                magCel += .6f;
                mag = Math.Min(magGoal, mag + magCel);
            }
            else
                magCel = 0;
            if(theta < thetaGoal)
            {
                thetCel += .01;
                theta = Math.Min(thetaGoal, theta + thetCel);
            }
            else if (theta > thetaGoal)
            {
                thetCel -= .01;
                theta = Math.Max(thetaGoal, theta + thetCel);
            }
            else
                thetCel = 0;
            Projectile.position.X = origin.X + (float)Math.Cos(theta) * mag;
            Projectile.position.Y = origin.Y + (float)Math.Sin(theta) * mag;
        }

        private void GetGoals()
        {
            if(counter < (int)Main.npc[(int)Projectile.ai[0]].ai[2])
            {
                counter++;
                if(counter % 2 == 0)
                {
                    magGoal += 200;
                }
                else
                {
                    if (counter % 4 == 1)
                        thetaGoal += Math.PI / 2;
                    else
                        thetaGoal -= Math.PI / 2;
                }
            }
        }
    }
}
