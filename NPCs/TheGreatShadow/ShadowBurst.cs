using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.TheGreatShadow
{
	public class ShadowBurst : ModProjectile
    {
        public bool bitherial = true;
        public int delay = 60;
        public bool zImmune = true;
        public float mag = 0;
        public Vector2 dustPos;
        public override void SetDefaults()
        {
            zImmune = true;
            delay = 60;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
        

        public override void AI()
        {
            bitherial = true;
            delay -= 1;
            if (delay <= 0)
                Projectile.Kill();
            mag += 3f;
            int k = 12;
            Random random = new Random();
            float theta = 6.28f * (float)random.NextDouble();
            while(k > 0)
            { 
                k--;
                theta = 6.28f * (float)random.NextDouble();
                dustPos.X = Projectile.Center.X + mag*(float)Math.Cos(theta);
                dustPos.Y = Projectile.Center.Y + mag*(float)Math.Sin(theta);
                Dust.NewDustPerfect(dustPos, ModContent.DustType<Black>(), null);
            }
        }

    }
}