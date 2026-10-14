using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etheria
{
	public class EtherialWave : ModProjectile
    {
        public bool bitherial = true;
        public float dAccel = 0f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Etherial Wave");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            dAccel = 0f;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 22;
			Projectile.height = 22;
			//projectile.alpha = 255;
            Projectile.timeLeft = 200;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.Next(0, 14) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            dAccel += 0.5f;
            Projectile.direction += (int)dAccel;
            bitherial = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 125;
            int b2 = 225;
            int b3 = 255;
            if (drawColor.R != (byte)b)
            {
                drawColor.R = (byte)b;
            }
            if (drawColor.G < (byte)b2)
            {
                drawColor.G = (byte)b2;
            }
            if (drawColor.B < (byte)b3)
            {
                drawColor.B = (byte)b3;
            }
            return drawColor;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Chilled, 90, true);
        }
    }
}