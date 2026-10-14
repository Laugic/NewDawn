using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class MiniHypo : ModNPC
    {
        int counter = 0;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.MisterStabby);
            NPC.width = 32;
            NPC.height = 28;
            NPC.damage = 40;
            NPC.defense = 4;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit11;
            NPC.DeathSound = SoundID.NPCDeath15;
            NPC.value = 0;
            NPC.noGravity = false;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, (2 * 60) + Main.rand.Next(60), true);
        }

        public override void OnKill()
        {
            var entitySource = new EntitySource_Parent(this.Entity);
            Item.NewItem(entitySource, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SnowBlock, Main.rand.Next(1, 4));
        }

        public override void FindFrame(int frameHeight)
        {
            counter++;
            if (counter > 15)
                counter = 0;
            NPC.frame.Y = frameHeight * (counter > 7 ? 1 : 0);
        }
    }
}
