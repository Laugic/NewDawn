using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Magic
{
    public class BysmalWandProjectile : ModProjectile
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
        public double mag = 24;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bysmal Blast");
        }

        public override void SetDefaults()
        {
            mag = 24;
            zImmune = true;
            theta = 0;
            vel = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            spawned = false;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (origin.X == 0)
            {
                origin.X = Projectile.position.X;
                origin.Y = Projectile.position.Y;
                originV.X = Projectile.velocity.X;
                originV.Y = Projectile.velocity.Y;
            }
            bitherial = true;
            if(Main.rand.Next(8) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            theta += 3.14f / 30 * Projectile.ai[0];
            mag += .025;
            origin += originV / 5;
            double targetX = origin.X + mag * Math.Cos(theta + 3.14) - Projectile.width / 2;
            double targetY = origin.Y + mag * Math.Sin(theta + 3.14);
            distance = (float)Math.Sqrt((targetX - Projectile.position.X) * (targetX - Projectile.position.X) + (targetY - Projectile.position.Y) * (targetY - Projectile.position.Y));
            tVel = distance / 6;
            Projectile.netUpdate = true;
            if (vel < tVel)
            {
                vel += .2f;
                vel *= 1.1f;
            }
            if (vel > tVel)
            {
                vel = tVel;
            }
            Projectile.velocity.X = (float)Math.Abs((Projectile.position.X - targetX) / distance * vel);
            if (targetX < Projectile.position.X)
                Projectile.velocity.X *= -1;
            Projectile.velocity.Y = (float)Math.Abs((Projectile.position.Y - targetY) / distance * vel);
            if (targetY < Projectile.position.Y)
                Projectile.velocity.Y *= -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int imageHeight = TextureAssets.Projectile[Projectile.type].Value.Height;
            int y6 = imageHeight * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, imageHeight)), Projectile.GetAlpha(Color.White), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)imageHeight / 2f) + new Vector2(6, -6), Projectile.scale, Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}