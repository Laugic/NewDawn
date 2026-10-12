using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class RuneOfTheAncients : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rune of the Ancients");
            // Tooltip.SetDefault("+15% Overflow damage and velocity\nIncreased movement speed when on Overflow");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.IsOnOverflow())
            {
                modPlayer.OverflowDamage += .15f;
                modPlayer.OverflowVelocity += .15f;
                player.moveSpeed += .15f;
                player.jumpSpeedBoost += 3;
                player.maxRunSpeed += 2;
                player.accRunSpeed += 2;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(169, 24);
            recipe.AddIngredient(607, 16);
            recipe.AddIngredient(ModContent.ItemType<AncientShard>(), 1);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}