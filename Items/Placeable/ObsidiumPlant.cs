using NewDawn.Items.Materials;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class ObsidiumPlant : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 62;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ObsidiumPlantBulbs>();
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateInventory(Player player)
        {
            if (Item.stack % 4 == 0)
                Item.createTile = ModContent.TileType<ObsidiumPlantBulbs>();
            if (Item.stack % 4 == 1)
                Item.createTile = ModContent.TileType<ObsidiumPlantHeart>();
            if (Item.stack % 4 == 2)
                Item.createTile = ModContent.TileType<ObsidiumPlantLeaves>();
            if (Item.stack % 4 == 3)
                Item.createTile = ModContent.TileType<ObsidiumPlantMine>();
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.LifeCrystal);
            recipe.AddTile(TileID.Hellforge);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 4);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumOre>(), 8);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.Register();

            recipe = Recipe.Create(ItemID.LifeCrystal);
            recipe.AddTile(TileID.Hellforge);
            recipe.AddIngredient(this);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 4);
            recipe.AddIngredient(ItemID.Hellstone, 8);
            recipe.AddIngredient(ItemID.Ruby, 2);
            recipe.Register();
        }
    }
}