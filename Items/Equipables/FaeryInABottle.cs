using NewDawn.Items.Consumables.Potions;
using NewDawn.Items.Materials;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class FaeryInABottle : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Faery in a Bottle");
            // Tooltip.SetDefault("Automatically switch Mysticism when you run out of the current Potentia");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

            switch (modPlayer.MysticMode)
            {
                case 1:
                    if (modPlayer.CurrentLuxCost > modPlayer.Lux)
                        modPlayer.MysticSwitch();
                    break;
                case 2:
                    if (modPlayer.CurrentVisCost > modPlayer.Vis)
                        modPlayer.MysticSwitch();
                    break;
                default:
                    if (modPlayer.CurrentMundusCost > modPlayer.Mundus)
                        modPlayer.MysticSwitch();
                    break;
            }
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FireflyinaBottle);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 5);
            recipe.AddIngredient(ModContent.ItemType<MysticaPotion>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}