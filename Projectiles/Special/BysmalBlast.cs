using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Special
{
    public class BysmalBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bysmal Blast");
        }
        public bool stopped = false;
        public override void SetDefaults()
        {
            stopped = false;
            Projectile.width = 18;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }


        public override void AI()
        {
            if(Main.rand.Next(8) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0f, Projectile.velocity.Y * 0f);
            if (!stopped)
            {
                Projectile.velocity.X *= .9f;
                Projectile.velocity.Y *= .9f;
            }

            if (Math.Abs(Projectile.velocity.X) <= .2 && Math.Abs(Projectile.velocity.Y) <= .2)
            {
                if (!stopped)
                {
                    stopped = true;
                    Vector2 targetPos;
                    targetPos.X = Main.MouseWorld.X;
                    targetPos.Y = Main.MouseWorld.Y;
                    Projectile.velocity = Projectile.DirectionTo(targetPos) * 22f;
                }
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
        }
    }
}
