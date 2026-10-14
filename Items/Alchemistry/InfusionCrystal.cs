using NewDawn.Abstracts;
using NewDawn.Items.Alchemistry.OLD_Essences;
using NewDawn.Items.Materials;
using NewDawn.Players;
using NewDawn.Tiles;
using NewDawn.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NewDawn.Items.Alchemistry
{
    public class InfusionCrystal : AbstractStorageItem
    {
        //TODO:
        //PREVENT RIGHT CLICKING TO EQUIP CUZ IT BREAKS STUFF
        public override Item[] Items { get; set; } = new Item[3];

        public bool Sealed { get; set; } = false;

        public static Condition Infusion = new Condition("Infused in Infusion Crystal", () => false);


        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.useAnimation = Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            if (Items[0] == null)
                AbstractStorage.GenerateClearStorage(this);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            if (Sealed)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null && Items[i].stack > 0 && Items[i].ModItem is Essence)
                    {
                        for (int j = 0; j < Items[i].ToolTip.Lines; j++)
                        {
                            string tip = ((Essence)Items[i].ModItem).GetTipAtIndex(this, i);
                            if (tip != "")
                                tooltips.Add(new TooltipLine(Mod, "Stored Item Tooltips", tip));
                        }
                    }
                }
            }
            else
                tooltips.Add(new TooltipLine(Mod, "To Infuse", "Infuse 3 Essences together\nRight click to open slots"));
        }

        public override void RightClick(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
                AbstractStorage.TriggerUI(this);
            Item.stack++;
        }

        public override bool CanRightClick()
        {
            return !Sealed;
        }

        public override bool? UseItem(Player player)
        {
            if (!Sealed)
            {
                bool checkEmpty = false;
                foreach (var item in Items)
                {
                    if (item == null || item.stack == 0)
                        checkEmpty = true;
                }
                if (!checkEmpty)
                {
                    SoundEngine.PlaySound(SoundID.DD2_DefeatScene);
                    var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
                    if (uiplayer != null)
                    {
                        if (uiplayer.LinkedStorage == this)
                        {
                            uiplayer.LinkedStorage = null;
                            SoundEngine.PlaySound(SoundID.MenuClose);
                        }
                        ItemStorageUISystem.ItemStorage.Reload();
                    }
                    Sealed = true;

                    CheckSets();

                    Main.NewText("Infusion complete!");
                }
                else if (player.whoAmI == Main.myPlayer && player.itemAnimation == player.itemAnimationMax)
                    Main.NewText("All slots must be filled to infuse!");
            }

            return null;
        }

        private void CheckSets()
        {
            //Collapse Items into list of items with stacks
            List<Item> ingredients = new List<Item>();
            foreach (var item in Items)
            {
                bool alreadyIn = false;
                foreach (var ing in ingredients)
                {
                    if (ing.type == item.type)
                    {
                        ing.stack++;
                        alreadyIn = true;
                    }
                }
                if (!alreadyIn)
                    ingredients.Add(item);
            }
            bool foundRecipe = false;

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                foundRecipe = true;
                if (recipe.Conditions.Contains(Infusion))
                {
                    foreach (var ing in ingredients)
                    {
                        var foundMatch = false;
                        foreach (var item in recipe.requiredItem)
                        {
                            if (item.type == ing.type && item.stack == ing.stack)
                                foundMatch = true;
                        }
                        if (!foundMatch)
                        {
                            foundRecipe = false;
                            break;
                        }
                    }
                    foreach (var item in recipe.requiredItem)
                    {
                        var foundMatch = false;
                        foreach (var ing in ingredients)
                        {
                            if (item.type == ing.type && item.stack == ing.stack)
                                foundMatch = true;
                        }
                        if (!foundMatch)
                        {
                            foundRecipe = false;
                            break;
                        }
                    }
                    if (foundRecipe)
                    {
                        Main.LocalPlayer.QuickSpawnItem(new EntitySource_ItemUse(Main.LocalPlayer, Item), new Item(recipe.createItem.type));
                        Item.consumable = true;
                        Item.stack--;
                        break;
                    }
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            Item.defense = 0;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null && Items[i].stack > 0 && Items[i].ModItem is Essence)
                    ((Essence)Items[i].ModItem).GetEffectAtIndex(player, this, i);
            }
        }

        public override bool FilterCheck(Item item, int slot)
        {
            return item.ModItem is Essence;
        }

        public override bool MaxStackOne()
        {
            return true;
        }

        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
            tag["Sealed"] = Sealed;
        }

        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
            bool seal = tag.Get<bool>("Sealed");
            Sealed = seal;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.LavaGem>());
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Vitasilk>(), 4);
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();
        }
    }
}
