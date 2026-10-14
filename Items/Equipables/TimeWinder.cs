using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class TimeWinder : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawn.zaWarudo > 0 || NewDawn.zaWarudo > 0)
            {
                player.GetDamage(DamageClass.Generic) += 0.15f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SteamTank>(), 1);
            recipe.AddIngredient(3086, 32);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}