using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class PoseidonConjuration1 : PrimaryConjurationProjectile
    {
        Vector2 targetPos;
        int counter = 0;
        double theta = 0;
        int movementType = 0;
        int bubbleCounter = 0;

        public override void SetDefaults()
        {
            counter = -1;
            movementType = 0;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 12 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            Bubbles();
            CheckActive();
            GetTarget();
            CheckTarget();
            Movement();
            RunCounter();
            MakeDust();
        }

        private void Bubbles()
        {
            bubbleCounter++;
            if (bubbleCounter > 30)
            {
                bubbleCounter = 0;
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y,
                           Projectile.velocity.X - (float)Main.rand.NextDouble() + (2 * (float)Main.rand.NextDouble()), Projectile.velocity.Y - (float)Main.rand.NextDouble() + (2 * (float)Main.rand.NextDouble()),
                           ModContent.ProjectileType<PoseidonConjuration2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Main.rand.Next(4));
                    SoundEngine.PlaySound(SoundID.Item86, Projectile.position);
                }
            }
        }

        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
                return;
            }
        }

        private void MakeDust()
        {

        }

        private void ResetCounter()
        {
            counter = 0;
            movementType++;
            targetPos = Projectile.position;
        }

        private void RunCounter()
        {
            if (counter == -1)
                counter = 60 * (int)Projectile.ai[0];

            counter++;
            theta += Math.PI / 60;

            if (movementType == 0 && counter > 3 * 60)
                ResetCounter();
            if (movementType == 1 && counter > 4 * 60)
                ResetCounter();
            if (movementType >= 2)
                movementType = 0;
        }

        private void Movement()
        {
            if (Projectile.ai[1] != -1)
                GetMovementType();
            else
                CirclePlayer();
        }

        private void GetMovementType()
        {
            switch (movementType)
            {
                case 0:
                    CirclePlayer();
                    break;
                default:
                    GrabNPC();
                    break;
            }
        }
        private void CheckTarget()
        {
            if (Projectile.ai[1] != -1)
            {
                if (Main.npc[(int)Projectile.ai[1]].life < 1 || !Main.npc[(int)Projectile.ai[1]].active)
                    Projectile.ai[1] = -1;
            }
        }

        public void CheckActive()
        {
            if (!Main.player[Projectile.owner].active || Main.player[Projectile.owner].statLife == 0)
                Projectile.Kill();
        }

        public void GetTarget()
        {
            float dist = 1400;

            foreach (NPC npc in Main.npc)
            {
                if (npc.damage > 0 && npc.type != NPCID.TargetDummy && !npc.townNPC && !npc.dontTakeDamage && !npc.friendly)
                {
                    if (npc.Distance(Projectile.Center) < dist)
                    {
                        dist = npc.Distance(Projectile.Center);
                        Projectile.ai[1] = npc.whoAmI;
                    }
                    if (npc.Distance(Main.player[Projectile.owner].Center) < dist * .67)
                    {
                        dist = npc.Distance(Main.player[Projectile.owner].Center);
                        Projectile.ai[1] = npc.whoAmI;
                    }
                }
            }
            if (Projectile.Distance(Main.player[Projectile.owner].Center) > 2000)
                Projectile.ai[1] = -1;
        }

        private void CirclePlayer()
        {
            targetPos = Main.player[(int)Projectile.owner].Center;
            targetPos.X += 80 * (float)Math.Cos(theta + (Math.PI * Projectile.ai[0]));
            targetPos.Y += 80 * (float)Math.Sin(theta + (Math.PI * Projectile.ai[0]));
            MoveToTargetPos(99);
        }

        private void GrabNPC()
        {
            if (Projectile.Distance(targetPos) < 10)
            {
                float dashDist = 80;
                targetPos = Main.npc[(int)Projectile.ai[1]].Center;
                var shift = targetPos - Projectile.position;
                shift.Normalize();
                shift *= dashDist;
                shift.RotatedByRandom(MathHelper.ToRadians(45));
                targetPos += shift;
            }
            MoveToTargetPos(6);
        }

        private void MoveToTargetPos(float max)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - Projectile.Center);
            newVel *= Math.Min(Vector2.Distance(Projectile.Center, targetPos) / 4, Math.Min(Projectile.velocity.Length() + .6f, max));
            Projectile.velocity = newVel;
        }
    }
}
