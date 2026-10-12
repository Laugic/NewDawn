using Terraria;
using Terraria.ID;


namespace NewDawn.Items.Loot
{
    public class EtherialDestructionCore : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Destruction");
            // Tooltip.SetDefault("Your global damage modifier is applied to your defense while in the Etherial");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
            {
                modPlayer.EtherialDestroyer = true;
            }
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DestructionCore>(), 1);
            recipe.AddIngredient(mod, nameof(EtherialEssence), 6);
            recipe.AddTile(ModContent.TileType<Tiles.AncientEnchanter>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}