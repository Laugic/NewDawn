using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class YellowSnowball : ModProjectile
    {
        public int target = -1;

        public override void SetDefaults()
        {
            target = -1;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            var multiplier = Math.Min(600 - Projectile.timeLeft, 30) / 30f;
            Projectile.velocity.Y += .25f * multiplier;

            var targetPos = new Vector2(Projectile.Center.X + 400, Projectile.Center.Y);

            if (target >= 0 && (!Main.npc[target].active || Main.npc[target].life <= 0))
                target = -1;
            if(target == -1)
            {
                foreach(NPC npc in Main.npc)
                {
                    if(npc.active && npc.life > 0 && npc.type != NPCID.TargetDummy && !npc.friendly && !npc.townNPC)
                    {
                        if (Vector2.Distance(Projectile.Center, npc.Center) < Vector2.Distance(Projectile.Center, targetPos))
                        {
                            targetPos = npc.Center;
                            target = npc.whoAmI;
                        }
                    }
                }
            }

            if(target != -1)
            {
                Projectile.velocity.X = .9f * Projectile.velocity.X + .1f * (Main.npc[target].Center.X - Projectile.Center.X) / 4f;
            }
        }
        
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}