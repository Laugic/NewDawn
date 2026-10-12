using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class Snowbomb : ModProjectile
    {
        float rot;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 3 * 60;
            rot = (.02f + Main.rand.NextFloat()) * (Main.rand.NextBool() ? -1 : 1);
        }

        public override void AI()
        {
            Movement();

            Projectile.rotation += rot / 30f;
        }

        private void Movement()
        {
            Projectile.velocity.Y += .08f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            for (int k = 0; k < (Main.expertMode ? 12 : 8); k++)
            {
                var theta = Main.rand.NextDouble() * Math.PI;
                float mag = -5 - (Main.rand.NextFloat() * 3);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), Projectile.damage, 7);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!Main.expertMode)
                return;
            target.AddBuff(BuffID.Frostburn, (2 * 60) + Main.rand.Next(60));
            target.AddBuff(BuffID.Chilled, (2 * 60) + Main.rand.Next(60));
        }
    }
}
