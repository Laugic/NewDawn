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
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();

            switch (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    if (mysticPlayer.Lux <= mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost && (mysticPlayer.CurrentLuxCost > mysticPlayer.Lux || (mysticPlayer.Vis >= (mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow) || (mysticPlayer.Mundus >= (mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow)))
                        mysticPlayer.MysticSwitch();
                    break;
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    if (mysticPlayer.Vis <= mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost && (mysticPlayer.CurrentVisCost > mysticPlayer.Vis || (mysticPlayer.Lux >= (mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow) || (mysticPlayer.Mundus >= (mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow)))
                        mysticPlayer.MysticSwitch();
                    break;
                default:
                    if (mysticPlayer.Mundus <= mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost && (mysticPlayer.CurrentMundusCost > mysticPlayer.Mundus || (mysticPlayer.Vis >= (mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow) || (mysticPlayer.Lux >= (mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow)))
                        mysticPlayer.MysticSwitch();
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