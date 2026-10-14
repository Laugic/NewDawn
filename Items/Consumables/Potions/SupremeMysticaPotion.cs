using NewDawn.Buffs;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables.Potions
{
    public class SupremeMysticaPotion : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 25);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().Mysticality == 0;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer modPlayer = player.GetModPlayer<NewDawnPlayer>();
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (mysticPlayer.Lux < (mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * (1 + (mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow - 1) / 2))
                mysticPlayer.Lux = (mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * (1 + (mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow - 1) / 2);
            if (mysticPlayer.Vis < (mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * (1 + (mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow - 1) / 2))
                mysticPlayer.Vis = (mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * (1 + (mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow - 1) / 2);
            if (mysticPlayer.Mundus < (mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * (1 + (mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow - 1) / 2))
                mysticPlayer.Mundus = (mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * (1 + (mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow - 1) / 2);
            player.AddBuff(ModContent.BuffType<Mysticality3>(), 60 * 60, true);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<GreaterMysticaPotion>(), 2);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 2);
            recipe.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}