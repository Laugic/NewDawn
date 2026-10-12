using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class HermesConjurationHoming : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                //If the npc is hostile
                if (!target.friendly)
                {
                    //Get the shoot trajectory from the projectile and target
                    float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                    float shootToY = target.position.Y - Projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    //If the distance between the live targeted npc and the projectile is less than 480 pixels
                    if (distance < 480f && !target.friendly && target.active && target.damage > 0)
                    {
                        //Divide the factor, 3f, which is the desired velocity
                        distance = 1f / distance;

                        //Multiply the distance by a multiplier if you wish the projectile to have go faster
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;

                        //Set the velocities to the shoot values
                        int mag = 10;
                        if(Projectile.velocity.X < shootToX)
                        {
                            Projectile.velocity.X += (shootToX - Projectile.velocity.X) / mag;
                        }
                        if(Projectile.velocity.Y < shootToY)
                        {
                            Projectile.velocity.Y += (shootToY - Projectile.velocity.Y) / mag;
                        }
                        if (Projectile.velocity.X > shootToX)
                        {
                            Projectile.velocity.X -= (Projectile.velocity.X - shootToX) / mag;
                        }
                        if (Projectile.velocity.Y > shootToY)
                        {
                            Projectile.velocity.Y -= (Projectile.velocity.Y - shootToY) / mag;
                        }
                    }
                }
            }
            /*Player player = Main.player[projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            mystDur = modPlayer.MysticDuration;*/
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<HermesDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                //projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }
    }
}