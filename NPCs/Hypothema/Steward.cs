using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class Steward : ModProjectile
    {
        static float maxXVel = 12f;
        static float maxXAcc = .2f;
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.timeLeft = 14 * 60;
        }

        public override void AI()
        {
            Projectile.netUpdate = true;

            if (Projectile.scale != 1)
                return;
            Movement();


            if (Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SnowCloudDust>(), Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            if (!Main.npc[(int)Projectile.ai[0]].active || Main.npc[(int)Projectile.ai[0]].type != ModContent.NPCType<Hypothema>() || Main.npc[(int)Projectile.ai[0]].life < 1)
                Projectile.active = false;

            Projectile.rotation += Projectile.velocity.X / 40f;
        }

        private void Movement()
        {
            if (Projectile.Center.Y < Main.player[Main.npc[(int)Projectile.ai[0]].target].Center.Y && Projectile.ai[1] < .1f)
                Projectile.ai[1] += .02f;
            if (Projectile.velocity.Y < 12)
                Projectile.velocity.Y += Projectile.ai[1] + .02f;
            if (Projectile.Center.X < Main.player[Main.npc[(int)Projectile.ai[0]].target].Center.X && Projectile.velocity.X < maxXVel)
                Projectile.velocity.X += maxXAcc;
            if (Projectile.Center.X > Main.player[Main.npc[(int)Projectile.ai[0]].target].Center.X && Projectile.velocity.X > -maxXVel)
                Projectile.velocity.X -= maxXAcc;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item51, Projectile.Center);
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.ai[1] = 0;
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath15, Projectile.Center);
            var entitySource = new EntitySource_Parent(this.Entity);
            NPC.NewNPC(entitySource, (int)Projectile.Center.X, (int)Projectile.Center.Y, ModContent.NPCType<MiniHypo>());
            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<SnowCloudDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
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
