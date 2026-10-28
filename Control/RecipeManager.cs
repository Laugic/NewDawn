using NewDawn.Items.Equipables;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Control
{
    public class RecipeManager : ModSystem
    {
        // A place to store the recipe group so we can easily use it later
        public static RecipeGroup EvilBar, EvilMaterial, AdamBar, TitaniumBars, Mythril, LargeGems, GoldBars, ColoredBalloon, Emblems, DungeonTables, DungeonBricks, SilverBars;

        public override void Unload()
        {
            EvilBar = null;
            EvilMaterial = null;
            AdamBar = null;
            TitaniumBars = null;
            Mythril = null;
            LargeGems = null;
            GoldBars = null;
            ColoredBalloon = null;
            Emblems = null;
            DungeonTables = null;
            DungeonBricks = null;
            SilverBars = null;
        }

        public override void AddRecipeGroups()
        {
            // Create a recipe group and store it
            // Language.GetTextValue("LegacyMisc.37") is the word "Any" in english, and the corresponding word in other languages
            EvilBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.DemoniteBar)}",
                ItemID.DemoniteBar, ItemID.CrimtaneBar);

            // To avoid name collisions, when a modded items is the iconic or 1st item in a recipe group, name the recipe group: ModName:ItemName
            //RecipeGroup.RegisterGroup("NewDawn:EvilBars", EvilBars);
            RecipeGroup.RegisterGroup($"NewDawn:{nameof(ItemID.DemoniteBar)}", EvilBar);



            EvilMaterial = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.ShadowScale)}",
                ItemID.ShadowScale, ItemID.TissueSample);
            RecipeGroup.RegisterGroup($"NewDawn:{nameof(ItemID.ShadowScale)}", EvilMaterial);



            AdamBar = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.AdamantiteBar)}",
                ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup($"NewDawn:{nameof(ItemID.AdamantiteBar)}", AdamBar);

            TitaniumBars = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.TitaniumBar)}",
                ItemID.TitaniumBar, ItemID.AdamantiteBar);
            RecipeGroup.RegisterGroup("TitaniumBars", TitaniumBars);

            GoldBars = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.GoldBar)}",
                ItemID.GoldBar, ItemID.PlatinumBar);
            RecipeGroup.RegisterGroup($"NewDawn:{nameof(ItemID.GoldBar)}", GoldBars);



            Mythril = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.MythrilBar)}",
                ItemID.MythrilBar, ItemID.OrichalcumBar);
            RecipeGroup.RegisterGroup($"NewDawn:{nameof(ItemID.MythrilBar)}", Mythril);

            LargeGems = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} Large Gemstone",
                ItemID.LargeEmerald, ItemID.LargeAmber, ItemID.LargeAmethyst, ItemID.LargeDiamond, ItemID.LargeRuby, ItemID.LargeSapphire, ItemID.LargeTopaz);
            RecipeGroup.RegisterGroup($"NewDawn:LargeGems", LargeGems);

            ColoredBalloon = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.BlueHorseshoeBalloon)}",
                ItemID.BlueHorseshoeBalloon, ItemID.WhiteHorseshoeBalloon, ItemID.YellowHorseshoeBalloon, ItemID.BalloonPufferfish);
            RecipeGroup.RegisterGroup($"NewDawn:ColoredBalloon", ColoredBalloon);

            Emblems = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.WarriorEmblem)}",
                ItemID.WarriorEmblem, ItemID.RangerEmblem, ItemID.SorcererEmblem, ItemID.SummonerEmblem, ModContent.ItemType<MysticEmblem>());
            RecipeGroup.RegisterGroup($"NewDawn:Emblems", Emblems);

            DungeonTables = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.BlueDungeonTable)}",
                ItemID.BlueDungeonTable, ItemID.GreenDungeonTable, ItemID.PinkDungeonTable);
            RecipeGroup.RegisterGroup($"NewDawn:DungeonTables", DungeonTables);

            DungeonBricks = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.AncientBlueDungeonBrick)}",
                ItemID.BlueBrick, ItemID.GreenBrick, ItemID.PinkBrick);
            RecipeGroup.RegisterGroup($"NewDawn:DungeonBricks", DungeonBricks);

            // Add an item to an existing Terraria recipeGroup
            //RecipeGroup.recipeGroups[RecipeGroupID.Snails].ValidItems.Add(ModContent.ItemType<Items.ExampleCritter>());

            // While an "IronBar" group exists, "SilverBar" does not. tModLoader will merge recipe groups registered with the same name, so if you are registering a recipe group with a vanilla item as the 1st item, you can register it using just the internal item name if you anticipate other mods wanting to use this recipe group for the same concept. By doing this, multiple mods can add to the same group without extra effort. In this case we are adding a SilverBar group. Don't store the RecipeGroup instance, it might not be used, use the same nameof(ItemID.ItemName) or RecipeGroupID returned from RegisterGroup when using Recipe.AddRecipeGroup instead.
            SilverBars = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
            ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup(nameof(ItemID.SilverBar), SilverBars);
        }

        public override void AddRecipes()
        {
            /*Recipe recipe = Recipe.Create(ItemID.SnowballCannon);
            recipe.AddIngredient(ItemID.Musket);
            recipe.AddIngredient(ItemID.SnowBlock, 20);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = Recipe.Create(ItemID.SnowballCannon);
            recipe.AddIngredient(ItemID.TheUndertaker);
            recipe.AddIngredient(ItemID.SnowBlock, 20);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();*/

            Recipe recipe = Recipe.Create(ItemID.IceMachine);
            recipe.AddIngredient(ItemID.Solidifier);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.WaterBucket);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = Recipe.Create(ItemID.Extractinator);
            recipe.AddIngredient(ModContent.ItemType<Wagidget>());
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = Recipe.Create(ItemID.TatteredCloth, 2);
            recipe.AddIngredient(ModContent.ItemType<Vitasilk>(), 2);
            recipe.AddRecipeGroup(EvilBar, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
