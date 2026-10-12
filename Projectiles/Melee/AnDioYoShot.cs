using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
    public class AnDioYoShot : ModProjectile
    {
        public bool bitherial = true;
        public bool stopped = false;
        public int power = 0;
        public int damage = 0;
        public int delay = 0;
        public bool spawned = false;
        public float theta = 0;
        public float vel = 0;
        public bool zImmune = true;
        public float tVel = 0;
        public float distance = 0;
        public Vector2 origin;
        public Vector2 originV;
        public double mag = 10;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AnDio Yo Shot");
        }
        public override void SetDefaults()
        {
            mag = 10;
            zImmune = true;
            theta = 0;
            vel = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            spawned = false;
            //mystDmg = (float)projectile.damage;
            //mystDur = 1f + projectile.knockBack;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }


        public override void AI()
        {
            if(origin.X == 0)
            {
                origin.X = Projectile.position.X;
                origin.Y = Projectile.position.Y;
            }
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), 0f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            theta -= 3.14f / 30;
            mag += .75;
            double targetX = origin.X + mag * Math.Cos(theta + 3.14) - Projectile.width / 2;
            double targetY = origin.Y + mag * Math.Sin(theta + 3.14);
            //projectile.position.X = (float)targetX;
            //projectile.position.Y = (float)targetY;
            distance = (float)Math.Sqrt((targetX - Projectile.position.X) * (targetX - Projectile.position.X) + (targetY - Projectile.position.Y) * (targetY - Projectile.position.Y));
            tVel = distance / 6;

            if (vel < tVel)
            {
                vel += .2f;
                vel *= 1.1f;
            }
            if (vel > tVel)
            {
                vel -= .1f;
                vel *= .95f;
            }
            Projectile.velocity.X = (float)Math.Abs((Projectile.position.X - targetX) / distance * vel);
            if (targetX < Projectile.position.X)
                Projectile.velocity.X *= -1;
            Projectile.velocity.Y = (float)Math.Abs((Projectile.position.Y - targetY) / distance * vel);
            if (targetY < Projectile.position.Y)
                Projectile.velocity.Y *= -1;
        }
    }
}