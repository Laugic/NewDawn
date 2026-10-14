using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class AncientJewelry : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.lifeRegen = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegenBonus += 10;
            player.statManaMax2 += 40;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BandOfAncients>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AncientNecklace>(), 1);
            recipe.AddTile(114);
            recipe.Register();
        }
    }
}