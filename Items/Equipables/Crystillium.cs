using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class Crystillium : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crystillium");
            // Tooltip.SetDefault("+10% Overflow\nOverflow attacks shoot crystal shards");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.GlobalOverflow += .1f;
            modPlayer.Crystillium = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 10);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}