using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class PoseidonConjuration2 : ConjurationProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            delay = 0;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            Projectile.frame = (int)(Projectile.ai[1]);
            Projectile.velocity *= .985f;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 16, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 0, Color.Aquamarine, 0.75f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
            }
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
        }
    }
}