using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class YuleConjuration1 : PrimaryConjurationProjectile
    {
        public int counter = 0;
        private float rotationGoal;

        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60 * 8;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            counter = 0;
            rotationGoal = 0;
        }

        public override void AI()
        {
            Projectile.velocity *= .98f;

            counter++;
            if(counter == 24)
                rotationGoal += (float)Math.PI / 4;
            if(counter > 30)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Main.myPlayer == Projectile.owner)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, new Vector2((float)Math.Cos(Projectile.rotation + i * Math.PI / 2) * 4, (float)Math.Sin(Projectile.rotation + i * Math.PI / 2) * 4), ModContent.ProjectileType<YuleConjuration2>(), Projectile.damage, 3f, Projectile.owner);
                }
                counter = 0;
            }
            if (Projectile.rotation < rotationGoal)
                Projectile.rotation += .2f;
        }
    }
}