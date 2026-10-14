using NewDawn.Control;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AndesiaLeggings : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+100% Mystic Duration\nIncreased Max Minions\n+15% movement speed");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDuration += 1f;
            player.moveSpeed += 0.15f;
            player.maxMinions += 3;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(3086, 32);
            recipe.AddRecipeGroup(RecipeManager.TitaniumBars, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}