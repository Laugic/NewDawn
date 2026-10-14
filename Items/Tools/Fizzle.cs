using NewDawn.Items.Placeable;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Tools
{
    public class Fizzle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.useAnimation = Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }
        public override bool AltFunctionUse(Player player)
        {
            // Enables right-click / alternate use.
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
                return false;

            int x = Player.tileTargetX;
            int y = Player.tileTargetY;

            if (!WorldGen.InWorld(x, y))
                return false;

            Tile tile = Main.tile[x, y];

            if (!tile.HasTile)
                return false;

            if (!player.IsTargetTileInItemRange(Item))
                return false;

            return ModContent.GetModTile(tile.TileType) is CrystalTile;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse != 2)
                return false;

            int x = Player.tileTargetX;
            int y = Player.tileTargetY;

            if (!WorldGen.InWorld(x, y))
                return false;

            Tile tile = Main.tile[x, y];

            if (!tile.HasTile)
                return false;

            ushort tileType = tile.TileType;

            var modTile = ModContent.GetModTile(tileType);
            if (modTile is not null and CrystalTile)
                ((CrystalTile)modTile).GetEssencesFromTile(x, y);

            //if (!extracted)
            //    return false;

            // Destroy the block WITHOUT normal tile drops.
            WorldGen.KillTile(
                x,
                y,
                fail: false,
                effectOnly: false,
                noItem: true
            );

            // Multiplayer tile sync.
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(
                    MessageID.TileManipulation,
                    -1,
                    -1,
                    null,
                    0,
                    x,
                    y
                );
            }

            // Consume exactly one extractor.
            Item.stack--;

            if (Item.stack <= 0)
                Item.TurnToAir();

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ModContent.ItemType<Fissionite>(), 20);
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();
        }
    }
}
