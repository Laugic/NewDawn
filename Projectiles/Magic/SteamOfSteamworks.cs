using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Magic
{
    public class SteamOfSteamworks : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steam of Steamworks");
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 4;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            if (Main.rand.Next(8) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 4 * 60, true);
        }
    }
}