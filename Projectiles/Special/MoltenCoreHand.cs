using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Special
{
    public class MoltenCoreHand : ModProjectile
    {
        Vector2 targetPos;
        int counter = 0;
        double theta = 0;
        int movementType = 0;

        public override void SetDefaults()
        {
            counter = -1;
            movementType = 0;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            CheckActive();
            GetTarget();
            CheckTarget();
            Movement();
            RunCounter();
            GetRotation();
        }

        private void ResetCounter()
        {
            counter = 0;
            movementType++;
        }

        private void GetRotation()
        {
            if (movementType == 3)
                Projectile.rotation += .1f;
            else
                Projectile.rotation = 0;
            Projectile.spriteDirection = 0;
        }

        private void RunCounter()
        {
            if (counter == -1)
                counter = 60 * (int)Projectile.ai[0];

            counter++;
            theta += Math.PI / 60;

            if (movementType == 0 && counter > 10 * 60)
                ResetCounter();
            if (movementType == 1 && counter > 3 * 60)
                ResetCounter();
            if (movementType == 2 && counter > 12 * 60)
                ResetCounter();
            if (movementType == 3 && counter > 8 * 60)
                ResetCounter();
            if (movementType >= 4)
                movementType = 0;
        }

        private void Movement()
        {
            if (Projectile.ai[1] != -1)
                GetMovementType();
            else
                HoverPlayer();
        }

        private void GetMovementType()
        {
            switch (movementType)
            {
                case 0:
                    HoverPlayer();
                    break;
                case 1:
                    HoverNPC();
                    break;
                case 2:
                    CircleNPC();
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
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get(Main.player[Projectile.owner]);
            if (!Main.player[Projectile.owner].active || NewDawnPlayer.MoltenCore <= 0)
                Projectile.Kill();
            if (Main.player[Projectile.owner].statLife <= (Main.player[Projectile.owner].statLifeMax2 / 2) + 1)
                Projectile.timeLeft = 4;
        }

        public void GetTarget()
        {
            float dist = 600;

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
            if (Projectile.Distance(Main.player[Projectile.owner].Center) > 1000)
                Projectile.ai[1] = -1;
        }

        private void HoverPlayer()
        {
            targetPos = Main.player[(int)Projectile.owner].Center;
            targetPos.X += 80 * (float)Math.Cos(Math.PI * Projectile.ai[0]);
            targetPos.Y += 20 * (float)Math.Sin(theta + (Math.PI * Projectile.ai[0]));
            MoveToTargetPos(99);
        }

        private void CircleNPC()
        {
            targetPos = Main.npc[(int)Projectile.ai[1]].Center;
            targetPos.X += (float)Math.Cos(theta + (Math.PI * Projectile.ai[0])) * 80;
            targetPos.Y += (float)Math.Sin(theta + (Math.PI * Projectile.ai[0])) * 80;
            MoveToTargetPos(12);
        }

        private void HoverNPC()
        {
            if (counter < 2 * 60)
            {
                targetPos = Main.npc[(int)Projectile.ai[1]].Center;
                targetPos.Y -= 80;
            }
            else if (counter >= 2 * 60 && counter < (2 * 60) + 2)
            {
                targetPos = Main.npc[(int)Projectile.ai[1]].Center;
                targetPos.Y += 160;
            }
            MoveToTargetPos(12);
        }

        private void GrabNPC()
        {
            targetPos = Main.npc[(int)Projectile.ai[1]].Center;
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
