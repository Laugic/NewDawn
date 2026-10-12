using NewDawn.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class SoulInfusion : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+40 maximum life\nIncreases life and mana regeneration\nHearts heal an additional 20 life upon pickup");
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
            player.statLifeMax2 += 40;
            player.lifeRegen += 2;
            player.manaRegen += 100;
            player.manaRegenBonus += 100;
            player.manaRegenDelayBonus += 30;
            player.GetModPlayer<AlchemistryPlayer>().BonusHealFromHearts += 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SoulEssence>(), 3);
            recipe.AddCondition(InfusionCrystal.Infusion);
            recipe.Register();
        }
    }
}
