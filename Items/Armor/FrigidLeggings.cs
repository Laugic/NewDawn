using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrigidLeggings : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Green;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.buffImmune[44] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 14);
            recipe.AddIngredient(ModContent.ItemType<FrostShard>(), 1);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}