using System;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class MechanicalDungeonGuardian : ModNPC
    {
        int counter = 0;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 8f;
        public float vAccel = .2f;
        public float vMag = 0f;
        Vector2 targetPos;
        int shootDelay = 0;

        public override void SetDefaults()
        {
            shootDelay = 0;
            counter = 0;
            vMag = 0f;
            vMax = 8f;
            tVel = 0f;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 130;
            NPC.height = 130;
            NPC.damage = 99999;
            NPC.defense = 9999;
            NPC.lifeMax = 900000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        private bool CheckIfActive()
        {
            return (NPC.CountNPCS(NPCID.SkeletronPrime) > 0);
        }

        public override void AI()
        {
            NPC.active = CheckIfActive();
            Shoot();
            NPC.rotation += .4f;
            targetPos = Main.player[NPC.target].Center;
            MoveToTarget(NPC);
        }

        private void Shoot()
        {
            shootDelay++;
            if(shootDelay >= 8 * 60)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/retro_blast"));
                for (int i = 0; i < 8; i++)
                {
                    if (Main.netMode != 1)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, (float)Math.Cos(Math.PI / 4 * i) * 12, (float)Math.Sin(Math.PI / 4 * i) * 12, ModContent.ProjectileType<EtherialYeet>(), (int)(80), 3, Main.myPlayer);
                }
                shootDelay = 0;
            }
        }

        private void MoveToTarget(NPC npc)
        {
            float dist = Vector2.Distance(targetPos, npc.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
            {
                vMag += vAccel;
                vMag = tVel;
            }
            if (vMag > tVel)
            {
                vMag = tVel;
            }
            if (vMag > vMax)
            {
                vMag = vMax;
            }
            if (dist != 0)
            {
                npc.velocity = npc.DirectionTo(targetPos) * vMag;
            }
        }
    }
}
