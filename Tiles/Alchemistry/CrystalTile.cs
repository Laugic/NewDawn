using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Materials;
using NewDawn.Items.Tools;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NewDawn.Tiles.Alchemistry
{
    public abstract class CrystalTile : NewDawnTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;

            ItemDrop = ModContent.ItemType<Frostpetal>();

            AddMapEntry(new Color(220, 220, 220));
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
            Main.tileLavaDeath[Type] = false;
            TileObjectData.addTile(Type);
            MineResist = 0.01f;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!closer && Main.myPlayer == Main.LocalPlayer.whoAmI)
                AddBuff();
        }

        public abstract void AddBuff();

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!noItem)
            {
                noItem = true;
                GetDrop(i, j);
            }
        }

        public abstract void GetDrop(int i, int j);

        public virtual void GetEssences(int i, int j) { }

        public void GetEssencesFromTile(int i, int j)
        {
            var modTile = ModContent.GetModTile(Main.tile[i, j].TileType);
            if (modTile != null && modTile is CrystalTile c && ModContent.GetModItem(c.ItemDrop) is PotionCrystal)
            {
                CheckEssences(ModContent.GetModItem(c.ItemDrop), i, j);
            }
        }


        private void CheckEssences(ModItem crystal, int x, int y)
        {
            //Collapse Items into list of items with stacks
            List<int> results = new List<int>();

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.Conditions.Contains(PotionCrystal.Demystify))
                {
                    foreach (var item in recipe.requiredItem)
                    {
                        if (item.type == crystal.Type)
                        {
                            for (int j = 0; j < recipe.createItem.stack; j++)
                                results.Add(recipe.createItem.type);
                        }
                    }
                }
            }
            if (results.Count > 0)
            {
                foreach (int item in results)
                {
                    Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, item);
                }
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;

            if (player.HeldItem.type != ModContent.ItemType<Fizzle>())
                return;

            if (!player.IsTargetTileInItemRange(player.HeldItem))
                return;

            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Fizzle>();

            player.cursorItemIconPush = 6;
        }

    }
}