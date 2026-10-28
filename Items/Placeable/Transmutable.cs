using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class Transmutable : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.createTile = ModContent.TileType<Tiles.Transmutable>();
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(16); //Anvil
            recipe.AddIngredient(3066, 12); //SmoothMarble
            recipe.AddIngredient(ItemID.Book);
            recipe.Register();

            //Ores
            //Copper to Tin
            Recipe CTrecipe = Recipe.Create(ItemID.TinOre, 3);
            CTrecipe.AddIngredient(ItemID.CopperOre, 3);
            CTrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            CTrecipe.Register();

            //Tin to Copper
            Recipe TCrecipe = Recipe.Create(ItemID.CopperOre, 3);
            TCrecipe.AddIngredient(ItemID.TinOre, 3);
            TCrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            TCrecipe.Register();

            //Iron to Lead
            Recipe ILrecipe = Recipe.Create(ItemID.LeadOre, 3);
            ILrecipe.AddIngredient(ItemID.IronOre, 3);
            ILrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            ILrecipe.Register();

            //Lead to Iron
            Recipe LIrecipe = Recipe.Create(ItemID.IronOre, 3);
            LIrecipe.AddIngredient(ItemID.LeadOre, 3);
            LIrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            LIrecipe.Register();

            //Silver to Tungsten
            Recipe STrecipe = Recipe.Create(ItemID.TungstenOre, 4);
            STrecipe.AddIngredient(ItemID.SilverOre, 4);
            STrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            STrecipe.Register();

            //Tungsten to Silver
            Recipe TSrecipe = Recipe.Create(ItemID.SilverOre, 4);
            TSrecipe.AddIngredient(ItemID.TungstenOre, 4);
            TSrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            TSrecipe.Register();

            //Gold to Platinum
            Recipe GPrecipe = Recipe.Create(ItemID.PlatinumOre, 4);
            GPrecipe.AddIngredient(ItemID.GoldOre, 4);
            GPrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            GPrecipe.Register();

            //Platinum to Gold
            Recipe PGrecipe = Recipe.Create(ItemID.GoldOre, 4);
            PGrecipe.AddIngredient(ItemID.PlatinumOre, 4);
            PGrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            PGrecipe.Register();

            //Bars
            //Copper to Tin
            Recipe CTBrecipe = Recipe.Create(ItemID.TinBar, 3);
            CTBrecipe.AddIngredient(ItemID.CopperBar, 3);
            CTBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            CTBrecipe.Register();

            //Tin to Copper
            Recipe TCBrecipe = Recipe.Create(ItemID.CopperBar, 3);
            TCBrecipe.AddIngredient(ItemID.TinBar, 3);
            TCBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            TCBrecipe.Register();

            //Iron to Lead
            Recipe ILBrecipe = Recipe.Create(ItemID.LeadBar, 3);
            ILBrecipe.AddIngredient(ItemID.IronBar, 3);
            ILBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            ILBrecipe.Register();

            //Lead to Iron
            Recipe LIBrecipe = Recipe.Create(ItemID.IronBar, 3);
            LIBrecipe.AddIngredient(ItemID.LeadBar, 3);
            LIBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            LIBrecipe.Register();

            //Silver to Tungsten
            Recipe STBrecipe = Recipe.Create(ItemID.TungstenBar, 4);
            STBrecipe.AddIngredient(ItemID.SilverBar, 4);
            STBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            STBrecipe.Register();

            //Tungsten to Silver
            Recipe TSBrecipe = Recipe.Create(ItemID.SilverBar, 4);
            TSBrecipe.AddIngredient(ItemID.TungstenBar, 4);
            TSBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            TSBrecipe.Register();

            //Gold to Platinum
            Recipe GPBrecipe = Recipe.Create(ItemID.PlatinumBar, 4);
            GPBrecipe.AddIngredient(ItemID.GoldBar, 4);
            GPBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            GPBrecipe.Register();

            //Platinum to Gold
            Recipe PGBrecipe = Recipe.Create(ItemID.GoldBar, 4);
            PGBrecipe.AddIngredient(ItemID.PlatinumBar, 4);
            PGBrecipe.AddTile(Mod, nameof(Tiles.TransmutationTable));
            PGBrecipe.Register();
        }
    }
}