using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class HatSpin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hat");
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 20;
            Projectile.timeLeft = 10 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
        }

        public override void AI()
        {
            DespawnCheck();
            Movement();
            FrameAnimation();
        }

        private void DespawnCheck()
        {
            if (!(Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<Hypothema>()) || !Main.npc[(int)Projectile.ai[1]].active)
                Projectile.Kill();
        }

        private void Movement()
        {
            if (Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<Hypothema>() && Main.npc[(int)Projectile.ai[1]].active)
            {
                int counter = (int)Main.npc[(int)Projectile.ai[1]].ai[0];
                if (counter < 5 * 60)
                {
                    MoveTowardsAtSpeed(Main.player[Projectile.owner].Center, 6);
                    Projectile.velocity.Y *= .9f;
                }
                else
                {
                    if (Main.npc[(int)Projectile.ai[1]].type == ModContent.NPCType<Hypothema>() && Main.npc[(int)Projectile.ai[1]].active)
                    {
                        MoveTowardsAtSpeed(Main.npc[(int)Projectile.ai[1]].Center, 6);
                        if (Vector2.Distance(Projectile.Center, Main.npc[(int)Projectile.ai[1]].Center) < 20)
                            Projectile.Kill();
                    }
                }
            }
        }

        private void MoveTowardsAtSpeed(Vector2 targetPos, float mag)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - Projectile.Center);
            newVel *= Math.Min(Vector2.Distance(Projectile.Center, targetPos) / 4, Math.Min(mag, Projectile.velocity.Length() + .6f));
            Projectile.velocity = newVel;
        }

        private void FrameAnimation()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 5)
            {
                Projectile.frame = 0;
            }
        }
    }
}