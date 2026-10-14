using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class SupremeSorcererEmblem : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += .2f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(489, 1);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 5);
            recipe.AddTile(ModContent.TileType<Tiles.AncientEnchanter>());
            recipe.Register();
        }
    }
}