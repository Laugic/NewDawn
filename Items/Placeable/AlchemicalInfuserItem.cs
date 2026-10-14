using NewDawn.Control;
using NewDawn.Items.Equipables;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class AlchemicalInfuserItem : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 27;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 50) * 5;
            Item.createTile = ModContent.TileType<Tiles.AlchemicalInfuser>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(9, 20);
            recipe.AddIngredient(170, 8);
            recipe.AddIngredient(31, 8);
            recipe.AddIngredient(8, 4);
            recipe.Register();

            Recipe warrecipe = Recipe.Create(ItemID.WarriorEmblem);
            warrecipe.AddRecipeGroup(RecipeManager.Emblems);
            warrecipe.AddIngredient(ItemID.SoulofNight, 4);
            warrecipe.AddIngredient(Mod, nameof(SoulOfHaught), 4);
            warrecipe.AddTile(TileID.MythrilAnvil);
            warrecipe.Register();

            Recipe ranrecipe = Recipe.Create(ItemID.RangerEmblem);
            ranrecipe.AddRecipeGroup(RecipeManager.Emblems);
            ranrecipe.AddIngredient(ItemID.SoulofLight, 4);
            ranrecipe.AddIngredient(Mod, nameof(SoulOfHaught), 4);
            ranrecipe.AddTile(TileID.MythrilAnvil);
            ranrecipe.Register();

            Recipe sorrecipe = Recipe.Create(ItemID.SorcererEmblem);
            sorrecipe.AddRecipeGroup(RecipeManager.Emblems);
            sorrecipe.AddIngredient(ItemID.SoulofNight, 4);
            sorrecipe.AddIngredient(ItemID.SoulofLight, 4);
            sorrecipe.AddTile(TileID.MythrilAnvil);
            sorrecipe.Register();

            Recipe sumrecipe = Recipe.Create(ItemID.SummonerEmblem);
            sumrecipe.AddRecipeGroup(RecipeManager.Emblems);
            sumrecipe.AddIngredient(ItemID.SoulofLight, 4);
            sumrecipe.AddIngredient(Mod, nameof(SoulOfSought), 4);
            sumrecipe.AddTile(TileID.MythrilAnvil);
            sumrecipe.Register();

            //Recipe ninrecipe = Recipe.Create(null, "NinjaEmblem");
            //ninrecipe.AddRecipeGroup(RecipeManager.Emblems);
            //ninrecipe.AddIngredient(ItemID.SoulofNight, 4);
            //ninrecipe.AddIngredient(Mod, nameof(SoulOfSought), 4);
            //ninrecipe.AddTile(TileID.MythrilAnvil);
            //ninrecipe.Register();

            Recipe mysrecipe = Recipe.Create(ModContent.ItemType<MysticEmblem>());
            mysrecipe.AddRecipeGroup(RecipeManager.Emblems);
            mysrecipe.AddIngredient(Mod, nameof(SoulOfHaught), 4);
            mysrecipe.AddIngredient(Mod, nameof(SoulOfSought), 4);
            mysrecipe.AddTile(TileID.MythrilAnvil);
            mysrecipe.Register();

            Recipe enchrecipe1 = Recipe.Create(ItemID.EnchantedSword);
            enchrecipe1.AddIngredient(ItemID.SilverBroadsword);
            enchrecipe1.AddIngredient(ModContent.ItemType<ArcaneShard>(), 4);
            enchrecipe1.AddIngredient(ItemID.Sapphire, 2);
            enchrecipe1.AddIngredient(ItemID.Ruby, 2);
            enchrecipe1.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            enchrecipe1.Register();

            Recipe enchrecipe2 = Recipe.Create(ItemID.EnchantedSword);
            enchrecipe2.AddIngredient(ItemID.TungstenBroadsword);
            enchrecipe2.AddIngredient(ModContent.ItemType<ArcaneShard>(), 4);
            enchrecipe2.AddIngredient(ItemID.Sapphire, 2);
            enchrecipe2.AddIngredient(ItemID.Ruby, 2);
            enchrecipe2.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            enchrecipe2.Register();

            Recipe starfuryrecipe = Recipe.Create(ItemID.Starfury);
            starfuryrecipe.AddRecipeGroup(RecipeManager.GoldBars, 12);
            starfuryrecipe.AddIngredient(ItemID.SunplateBlock, 24);
            starfuryrecipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 4);
            starfuryrecipe.AddIngredient(ItemID.Amethyst, 2);
            starfuryrecipe.AddIngredient(ItemID.Topaz, 2);
            starfuryrecipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            starfuryrecipe.Register();

            Recipe golemcellrecipe = Recipe.Create(ItemID.LihzahrdPowerCell);
            golemcellrecipe.AddIngredient(2766, 4); //Solar Tablet Fragment
            golemcellrecipe.AddIngredient(ItemID.HallowedBar, 1);
            golemcellrecipe.AddTile(ModContent.TileType<Tiles.AncientEnchanter>());
            golemcellrecipe.Register();
        }
    }
}