using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class AgnesIllusion2 : IllusionProjectile
    {
        public float mag = 0;
        Vector2 origin;
        double theta = 0;
        bool spawned = false;
        public override void SetDefaults()
        {
            theta = 0;
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 4 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            spawned = false;
            buffID = ModContent.BuffType<InfernalBuff>();
        }
        public override void AI()
        {
            if(!spawned)
            {
                spawned = true;
                theta = Main.rand.NextDouble() * Math.PI * 2;
                origin = Projectile.position;
                mag = 20;
            }
            if(Main.rand.Next(8) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            //projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            theta += Math.PI / 60;
            mag += .4f;
            var targetPos = new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag) + origin;
            Projectile.velocity = Projectile.DirectionTo(targetPos) * Projectile.Distance(targetPos) / 4;
        }
    }
}