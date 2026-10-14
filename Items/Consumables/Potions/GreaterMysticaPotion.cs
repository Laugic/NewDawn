using NewDawn.Buffs;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables.Potions
{
    public class GreaterMysticaPotion : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 10);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().Mysticality == 0;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer modPlayer = player.GetModPlayer<NewDawnPlayer>();
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (mysticPlayer.Lux < mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost)
                mysticPlayer.Lux = mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost;
            if (mysticPlayer.Vis < mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost)
                mysticPlayer.Vis = mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost;
            if (mysticPlayer.Mundus < mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost)
                mysticPlayer.Mundus = mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost;
            player.AddBuff(ModContent.BuffType<Mysticality2>(), 60 * 60, true);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(3);
            recipe.AddIngredient(ModContent.ItemType<MysticaPotion>(), 2);
            recipe.AddIngredient(ModContent.ItemType<MagmaSnapper>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}