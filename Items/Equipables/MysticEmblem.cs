using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Equipables
{
    public class MysticEmblem : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+15% mystic damage");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDamage += .15f;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(2349, 4);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}