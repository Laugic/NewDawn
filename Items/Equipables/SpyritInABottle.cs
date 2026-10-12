using NewDawn.Items.Consumables.Potions;
using NewDawn.Items.Loot;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class SpyritInABottle : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spyrit in a Bottle");
            // Tooltip.SetDefault("Automatically switch Mysticism when you run out of the current Potentia,\nor one of your Potentias is at Max Overflow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 1000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

            switch (modPlayer.MysticMode)
            {
                case 1:
                    if (modPlayer.Lux <= modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost && (modPlayer.CurrentLuxCost > modPlayer.Lux || (modPlayer.Vis >= (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow) || (modPlayer.Mundus >= (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow)))
                        modPlayer.MysticSwitch();
                    break;
                case 2:
                    if (modPlayer.Vis <= modPlayer.VisMax + modPlayer.VisMaxPermaBoost && (modPlayer.CurrentVisCost > modPlayer.Vis || (modPlayer.Lux >= (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow) || (modPlayer.Mundus >= (modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost) * modPlayer.MundusOverflow * modPlayer.GlobalOverflow)))
                        modPlayer.MysticSwitch();
                    break;
                default:
                    if (modPlayer.Mundus <= modPlayer.MundusMax + modPlayer.MundusMaxPermaBoost && (modPlayer.CurrentMundusCost > modPlayer.Mundus || (modPlayer.Vis >= (modPlayer.VisMax + modPlayer.VisMaxPermaBoost) * modPlayer.VisOverflow * modPlayer.GlobalOverflow) || (modPlayer.Lux >= (modPlayer.LuxMax + modPlayer.LuxMaxPermaBoost) * modPlayer.LuxOverflow * modPlayer.GlobalOverflow)))
                        modPlayer.MysticSwitch();
                    break;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FaeryInABottle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GreaterMysticaPotion>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 5);
            recipe.AddTile(ModContent.TileType<MineralEnchanterTile>());
            recipe.Register();
        }
    }
}