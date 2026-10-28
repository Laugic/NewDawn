using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Tools;
using System.Collections.Generic;
using System.Linq;
using Terraria;
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

        public bool GetEssencesFromTile(int i, int j)
        {
            var modTile = ModContent.GetModTile(Main.tile[i, j].TileType);
            if (modTile is not CrystalTile)
                return false;

            Item crystal = modTile.GetItemDrops(i, j)
                .FirstOrDefault(item => ModContent.GetModItem(item.type) is PotionCrystal);

            if (crystal == null || crystal.IsAir || ModContent.GetModItem(crystal.type) is not ModItem modItem)
                return false;

            return CheckEssences(modItem, i, j);
        }


        private bool CheckEssences(ModItem crystal, int x, int y)
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

            return results.Count > 0;
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