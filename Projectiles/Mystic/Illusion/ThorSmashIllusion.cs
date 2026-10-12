using Microsoft.Xna.Framework.Graphics;
using NewDawn.Buffs;
using NewDawn.Projectiles.Mystic.Misc;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class ThorSmashIllusion : IllusionProjectile
    {
        public Texture2D Smash;
        public override void SetDefaults()
        {
            Projectile.width = 222;
            Projectile.height = 242;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 7;
            buffID = ModContent.BuffType<ThunderCharged>();
            if (!Main.dedServ)
            {
                Smash = Mod.GetTexture(this.GetType().GetRootPath() + "/ThorSmashIllusion");
            }
        }
        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                if (Projectile.frame == 3 && Main.myPlayer == Projectile.owner)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        double theta = Main.rand.NextDouble() * Math.PI * 2;
                        float mag = 12;
                        int p = Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + (2 * mag * (float)Math.Cos(theta)), Projectile.Center.Y + 60 + (2 * mag * (float)Math.Sin(theta)), (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<Bolt>(), (int)Projectile.damage, 8, Main.myPlayer);
                        Main.projectile[p].ai[0] = 4;
                        Main.projectile[p].ai[1] = 1;
                    }
                }
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 7)
            {
                Projectile.Kill();
            }
        }
    }
}
