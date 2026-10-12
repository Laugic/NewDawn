using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class UndeathSkull : PrimaryConjurationProjectile
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
            Projectile.timeLeft = 30 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor * 0.35f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if (Projectile.velocity.X < 0f)
                {
                    spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            return true;
        }

        public override void AI()
        {
            CheckActive();
            GetTarget();
            CheckTarget();
            Movement();
            RunCounter();
            MakeDust();
        }

        private void MakeDust()
        {
            for (int i = 0; i < 2; i++)
            {
                float speedX = Projectile.velocity.X / 2.5f * (float)i;
                float speedY = Projectile.velocity.Y / 2.5f * (float)i;
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Black>(), 0f, 0f, 75, default(Color));
                Main.dust[dust].position.X = Projectile.Center.X - speedX;
                Main.dust[dust].position.Y = Projectile.Center.Y - speedY;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
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
