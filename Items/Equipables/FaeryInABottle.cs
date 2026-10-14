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
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();

            switch (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    if (mysticPlayer.CurrentLuxCost > mysticPlayer.Lux)
                        mysticPlayer.MysticSwitch();
                    break;
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    if (mysticPlayer.CurrentVisCost > mysticPlayer.Vis)
                        mysticPlayer.MysticSwitch();
                    break;
                default:
                    if (mysticPlayer.CurrentMundusCost > mysticPlayer.Mundus)
                        mysticPlayer.MysticSwitch();
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