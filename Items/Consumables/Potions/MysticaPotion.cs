using NewDawn.Buffs;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables.Potions
{
    public class MysticaPotion : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Potentia Potion");
            // Tooltip.SetDefault("Restores all Potentias to full capacity, but removes Overflow");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 30;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 2);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().Mysticality == 0;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer modPlayer = player.GetModPlayer<NewDawnPlayer>();
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.Lux = mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost;
            mysticPlayer.Vis = mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost;
            mysticPlayer.Mundus = mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost;
            player.AddBuff(ModContent.BuffType<Mysticality>(), 60 * 60, true);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 1);
            recipe.AddIngredient(ItemID.JungleSpores, 1);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}