using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class WaterInfusion : ModItem
    {

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
            player.fishingSkill += 15;
            player.sonarPotion = true;
            player.aggro -= 5;
            player.calmed = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            player.gills = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WaterEssence>(), 3);
            recipe.AddCondition(InfusionCrystal.Infusion);
            recipe.Register();
        }
    }
}
