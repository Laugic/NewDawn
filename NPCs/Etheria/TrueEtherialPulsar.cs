using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etheria
{
	public class TrueEtherialPulsar : ModProjectile
	{
        public int delay = 0;
        public int damage = 0;
        public bool bitherial = true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Etherial Pulsar");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            bitherial = true;
            Projectile.width = 44;
			Projectile.height = 44;
			//projectile.alpha = 255;
            Projectile.timeLeft = 240;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            delay = 0;
            damage = 60;
        }

        public override void AI()
        {
            bitherial = true;
            if (Main.rand.Next(0, 14) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), 0f, 0f);
            delay += 1;
                Projectile.velocity *= .95f;
            if(delay >= 100 && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 7, 0, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -7, 0, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -7, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 7, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, 5, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, -5, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, -5, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, 5, ModContent.ProjectileType<TrueEtherialPulse>(), damage, 3f, Main.myPlayer);
                Projectile.Kill();
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 225;
            int b2 = 125;
            int b3 = 155;
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
            target.AddBuff(44, 300, true);
        }
    }
}