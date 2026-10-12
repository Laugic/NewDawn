using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    public class Ragnarune : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ragnarune");
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            NPC.aiStyle = -1;
            NPC.width = 40;
            NPC.height = 40;
            NPC.frame.Y = 0;
            NPC.damage = 50;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.ai[2] = 0;
            NPC.noGravity = true;
        }

        public override void AI()
        {
            if (!Main.npc[(int)NPC.ai[0]].active)
                NPC.active = false;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = (int)NPC.ai[2] * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}