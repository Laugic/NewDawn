using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class SupremeEmblem : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Cog of Knowledge");
            // Tooltip.SetDefault("'The power of all'\nIncreases damage by 20%");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 48;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NewDawnPlayer>().DamageBoost(.2f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SupremeWarriorEmblem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SupremeRangerEmblem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SupremeSorcererEmblem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SupremeSummonerEmblem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SupremeNinjaEmblem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SupremeMysticEmblem>(), 1);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 5);
            recipe.AddTile(ModContent.TileType<Tiles.AncientEnchanter>());
            recipe.Register();
        }
    }
}