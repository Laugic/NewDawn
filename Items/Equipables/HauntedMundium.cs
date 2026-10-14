using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class HauntedMundium : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+25% Overflow Capacity\n+15% Conjuration Damage\n+50Mundus");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.ConjurationDamage += .15f;
            mysticPlayer.MysticDuration += .25f;
            mysticPlayer.GlobalOverflow += .25f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyWood, 60);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}