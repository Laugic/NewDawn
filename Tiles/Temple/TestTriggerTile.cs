using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using NewDawn.Temple;
using NewDawn.Items.Temple.Consumables;
using NewDawn.Temple.RoomGeneration;

namespace NewDawn.Tiles.Temple
{
    public class TestTriggerTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Test Temple Entrance Pillar");
            AddMapEntry(new Color(150, 150, 150), name);
        }

        public override bool RightClick(int i, int j)
        {
            int numKeys = 0;
            foreach(Player player in Main.player)
            {
                foreach(Item item in player.inventory)
                {
                    if (item.type == ModContent.ItemType<Keystone>())
                        numKeys += item.stack;
                    if(item.type == ModContent.ItemType<GoldenTempleKey>())
                        numKeys += 99;
                }
            }
            if(numKeys >= 3 || SubworldSystem.Current is Sanctuary)
                TempleMain.NextFloor();
            else
                Main.NewText("You need more Keystones to proceed!");
            /*if(SubworldSystem.Current is TempleWorld)
            {
                bool unlocked = true;
                foreach(NPC npc in Main.npc)
                {
                    if(npc.damage > 0)// && !npc.townNPC && npc.life > 0 && npc.active)
                    {
                        unlocked = false;
                        Main.NewText("Defeat all threats before proceeding!");
                    }
                }
                if(unlocked)
                    TempleMain.EnterFloor();
            }*/
            //else
            //    TempleMain.EnterFloor();
            return true;
        }
    }
}
