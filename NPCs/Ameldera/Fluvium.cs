using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Ameldera
{
    public class Fluvium : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.BlueSlime);
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = 20;
            NPC.defense = 6;
            NPC.lifeMax = 120;
            NPC.value = 60f;
            NPC.lavaImmune = true;
            Main.npcFrameCount[NPC.type] = 2;
        }

    }
}
