using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class DestructionCore : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+10% Destruction Damage\n+50 Lux");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.DestructionDamage += .1f;
            mysticPlayer.LuxMax += 50;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LargeTopaz, 1);
            recipe.AddIngredient(ItemID.LargeRuby, 1);
            recipe.AddIngredient(ModContent.ItemType<Arcanum>());
            recipe.AddTile(ModContent.TileType<AlchemicalInfuser>());
            recipe.Register();
        }
    }
}