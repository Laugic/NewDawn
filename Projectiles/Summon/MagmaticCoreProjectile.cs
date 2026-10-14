using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{
    public class MagmaticCoreProjectile : ModProjectile
    {
        public float dust = 0f;

        public override void SetDefaults()
        {
            Projectile.width = 38;
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
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);

            if (Projectile.velocity.X < 0) Projectile.frame = 1;
            else Projectile.frame = 0;

            if (Main.rand.Next(6) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), 0, Math.Abs(Projectile.velocity.Y) * -0.1f);

            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.MoltenCoreSummon = false;
            }
            if (modPlayer.MoltenCoreSummon)
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