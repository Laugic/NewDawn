using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class EarthInfusion : ModItem
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
            player.statDefense += 6;
            player.noKnockback = true;
            player.pickSpeed -= 0.25f;
            player.tileSpeed += .25f;
            player.wallSpeed += .25f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EarthEssence>(), 3);
            recipe.AddCondition(InfusionCrystal.Infusion);
            recipe.Register();
        }
    }
}
