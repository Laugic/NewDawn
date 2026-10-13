using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Buffs;
using Terraria.ModLoader;
using System;
using NewDawn.Particles;
using System.Collections.Generic;
using NewDawn.NPCs;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class EtherialRocket : DestructionProjectile
    {
        int Counter = 0;
        double rotSpeed = .1;
        public List<Particle> Particles;
        public override void SetDefaults()
        {
            Counter = 0;
            Projectile.width = 48;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Particles = new List<Particle>();
            Main.projFrames[Projectile.type] = 2;
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.25f) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, new Rectangle(0, 74 * projectile.frame, 74, 74), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }*/
        public override bool PreDraw(ref Color lightColor)
        {
            ParticleManager.DrawParticles(Main.spriteBatch, Color.White, ref Particles);
            return base.PreDraw(ref lightColor);
        }

        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 2)
            {
                Projectile.frame = 0;
                return;
            }
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);

            MoveTo(Target());
            ParticleTrail();
        }

        private void ParticleTrail()
        {
            Counter++;

            ParticleManager.UpdateParticles(ref Particles);
            if (Counter % 2 == 0)
                Particles.Add(new Particle(NewDawn.RequestTexture("Particles/EtherialSmoke"), Projectile.Center - Projectile.velocity / 2, new Vector2(-2 + Main.rand.Next(5), -2 + Main.rand.Next(5)), 6, .95f, Main.rand.NextFloat() * 6.28f, .2f, 1, .9f + Main.rand.NextFloat() * .1f));
        }

        private void MoveTo(Vector2 targetVector)
        {
            var tarRot = targetVector.ToRotation();
            var curRot = Projectile.velocity.ToRotation();
            var rotGoal = tarRot - curRot;
            if (rotGoal > Math.PI)
                rotGoal -= 2 * (float)Math.PI;
            if (rotGoal < -Math.PI)
                rotGoal += 2 * (float)Math.PI;
            if (rotGoal > rotSpeed)
                Projectile.velocity = Projectile.velocity.RotatedBy(rotSpeed) * .9f;
            if (rotGoal < -rotSpeed)
                Projectile.velocity = Projectile.velocity.RotatedBy(-rotSpeed) * .9f;
            if (Projectile.velocity.Length() < 16)
                Projectile.velocity *= 1.1f;
            /*var curVel = projectile.velocity;
            curVel.Normalize();
            var dirToTarget = targetVector;
            dirToTarget.Normalize();
            var newVel = curVel * 95 + dirToTarget * 5;
            newVel /= 100f;
            newVel *= Math.Min(SPEED * 2, Math.Max(SPEED, projectile.velocity.Length() * 1.1f));
            projectile.velocity = newVel;*/
        }

        private Vector2 Target()
        {
            var targetPos = Projectile.Center;
            targetPos.X += 800;
            bool spotted = false;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.life > 0 && !npc.friendly && !npc.townNPC && npc.damage > 0 && !npc.dontTakeDamage)//(npc.GetGlobalNPC<EtherialGlobalNPC>().bitherial || (NewDawnVars.eNPCs.Contains(npc.type) && NewDawnWorld.downedEtheria) || (!NewDawnVars.eNPCs.Contains(npc.type) && !NewDawnWorld.downedEtheria)))
                {
                    Vector2 newMove = npc.Center - Projectile.Center;
                    var curMove = targetPos - Projectile.Center;
                    if (newMove.Length() < curMove.Length())
                    {
                        targetPos = newMove;
                        spotted = true;
                    }
                }
            }
            if (spotted)
                return targetPos;
            return Projectile.velocity;
        }
    }
}
