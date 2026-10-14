using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{
    public class SandySharkProjectile : ModProjectile
    {
    	public float dust = 0f;
    	
        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 38;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 66;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 388;
            Main.projFrames[Projectile.type] = 2;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);// + 1.57f;
            
            if (Projectile.velocity.X < 0) Projectile.frame = 1;
            else Projectile.frame = 0;

            if (Main.rand.Next(6) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Sandy>(), 0, Math.Abs(Projectile.velocity.Y) * -0.1f);

            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.SandSharkSummon = false;
            }
            if (modPlayer.SandSharkSummon)
            {
                Projectile.timeLeft = 2;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}