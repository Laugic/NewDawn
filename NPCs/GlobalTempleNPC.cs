using NewDawn.Items.Accessories;
using NewDawn.Items.Weapons.Snowball;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs
{
    public class GlobalTempleNPCs : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            GetWeaponAndAccDrops(npc);
            GetMaterialDrops(npc);
        }

        private void GetMaterialDrops(NPC npc)
        {

        }

        private void GetWeaponAndAccDrops(NPC npc)
        {
            if ((npc.type == NPCID.SnowmanGangsta) && Main.rand.NextBool(40))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Baller>(), 1);

            if ((npc.type == NPCID.IceQueen) && Main.rand.NextBool(Main.expertMode ? 13 : 22))
                Item.NewItem(npc.GetSource_Loot(), (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IceCrown>(), 1);
        }
    }
}