using Microsoft.Xna.Framework;
using NewDawn.Abstracts;
using NewDawn.Items.Materials;
using NewDawn.Players;
using NewDawn.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public class FireInfusion : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("8% increased critical strike chance\nGrants immunity to fire and lava\nAttacks set enemies on fire");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.hasVanityEffects = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 8f;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.GetModPlayer<AccessoryPlayer>().SetOnFire = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FireEssence>(), 3);
            recipe.AddCondition(InfusionCrystal.Infusion);
            recipe.Register();
        }
    }
}
