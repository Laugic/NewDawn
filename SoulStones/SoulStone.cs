using System.Collections.Generic;
using NewDawn.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Items.Materials;
using NewDawn.Tiles;

namespace NewDawn.SoulStones
{
    public class SoulStone : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            // Tooltip.SetDefault("Absorbs the souls of powerful fallen creatures\nWorks from your Inventory");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.width = 30;
            Item.height = 28;
            Item.value = Item.buyPrice(silver: 20);
            Item.rare = ItemRarityID.Expert;

            //item.accessory = true;
            Cursed = true;
        }

        //public override bool CanEquipAccessory(Player player, int slot) => base.CanEquipAccessory(player, slot) && player.GetModPlayer<NewDawnPlayer>().Focus != null;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            Cursed = !NewDawnWorld.downedNecrodon;
            if (Cursed)
                tooltips.Add(new TooltipLine(Mod, "SoulStoneNoPlayerFocus", "--An otherwordly entity seems to have sealed some of its power--"));

            if (NewDawnPlayer.Focus == null)
            {
                tooltips.Add(new TooltipLine(Mod, "SoulStoneNoPlayerFocus", "You must select a Focus to use the Soul Stone!")
                {
                    OverrideColor = Color.Red
                });
                return;
            }

            tooltips.Add(new TooltipLine(NewDawn.Instance, "SoulStoneDisplayFocusName", "Your focus is " + NewDawnPlayer.Focus.DisplayName + '.')
            {
                OverrideColor = NewDawnPlayer.Focus.AssociatedColor
            });

            for (int i = 0; i < NewDawnPlayer.Focus.EffectsCount; i++)
            {
                if (NewDawnPlayer.Focus.GetEffect(i).Condition(NewDawnPlayer))
                    tooltips.Add(NewDawnPlayer.Focus.GetEffect(i).Tooltip);
            }

            if(NewDawnWorld.GetCurseCount() > 0)
            {
                tooltips.Add(new TooltipLine(NewDawn.Instance, "CurseLine", "----Curses----")
                {
                    OverrideColor = Color.Red
                });

                GetCurseTooltips(tooltips);
            }
            if(!Cursed)
                tooltips.Add(new TooltipLine(Mod, "SoulStoneNoPlayerFocus", "--The Curse has been lifted--")
                {
                    OverrideColor = Color.Gold
                });
        }

        private static void GetCurseTooltips(List<TooltipLine> tooltips)
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            for (int i = 0; i < NewDawnWorld.GetCurseCount() && i < NewDawnPlayer.Focus.CursesCount; i++)
            {
                for (int j = 0; j < NewDawnPlayer.Focus.NemesesCount; j++)
                {
                    if (NewDawnPlayer.Focus.Nemeses[j].GetCurse(i).Condition(NewDawnPlayer))
                        tooltips.Add(NewDawnPlayer.Focus.Nemeses[j].GetCurse(i).Tooltip);
                }
            }

            for (int i = 0; i < Math.Floor((float)NewDawnWorld.GetCurseCount() / 2f) && i < NewDawnPlayer.Focus.CursesCount; i++)
            {
                for (int j = 0; j < NewDawnPlayer.Focus.EnemiesCount; j++)
                {
                    if (NewDawnPlayer.Focus.Enemies[j].GetCurse(i).Condition(NewDawnPlayer))
                        tooltips.Add(NewDawnPlayer.Focus.Enemies[j].GetCurse(i).Tooltip);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (Main.LocalPlayer != player)
                return;

            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get(Main.LocalPlayer);

            if (NewDawnPlayer.Focus == null)
                return;

            for (int i = 0; i < NewDawnPlayer.Focus.EffectsCount; i++)
            {
                if (NewDawnPlayer.Focus.GetEffect(i).Condition(NewDawnPlayer))
                    NewDawnPlayer.Focus.GetEffect(i).Effect(NewDawnPlayer, Item.favorited);
            }

            if (NewDawnWorld.GetCurseCount() > 0)
                GetCurseEffects(player, Item.favorited);
            base.UpdateInventory(player);
        }

        /*
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.LocalPlayer != player)
                return;

            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get(Main.LocalPlayer);

            if (NewDawnPlayer.Focus == null)
                return;

            for (int i = 0; i < NewDawnPlayer.Focus.EffectsCount; i++)
            {
                if (NewDawnPlayer.Focus.GetEffect(i).Condition(NewDawnPlayer))
                    NewDawnPlayer.Focus.GetEffect(i).Effect(NewDawnPlayer, hideVisual);
            }

            if(NewDawnWorld.GetCurseCount() > 0)
                GetCurseEffects(player, hideVisual);
            base.UpdateAccessory(player, hideVisual);
        }*/

        private static void GetCurseEffects(Player player, bool hideVisual)
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get(player);

            for (int i = 0; i < NewDawnWorld.GetCurseCount() && i < NewDawnPlayer.Focus.CursesCount; i++)
            {
                for (int j = 0; j < NewDawnPlayer.Focus.NemesesCount; j++)
                {
                    if (NewDawnPlayer.Focus.Nemeses[j].GetCurse(i).Condition(NewDawnPlayer))
                        NewDawnPlayer.Focus.Nemeses[j].GetCurse(i).Effect(NewDawnPlayer, hideVisual);
                }
            }

            for (int i = 0; i < Math.Floor((float)NewDawnWorld.GetCurseCount() / 2f) && i < NewDawnPlayer.Focus.CursesCount; i++)
            {
                for (int j = 0; j < NewDawnPlayer.Focus.EnemiesCount; j++)
                {
                    if (NewDawnPlayer.Focus.Enemies[j].GetCurse(i).Condition(NewDawnPlayer))
                        NewDawnPlayer.Focus.Enemies[j].GetCurse(i).Effect(NewDawnPlayer, hideVisual);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 10);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }

        public bool Cursed { get; set; }
    }
}