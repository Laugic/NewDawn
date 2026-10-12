using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Items.Equipables
{
    public class RuneOfEruption : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rune of Eruption");
            // Tooltip.SetDefault("+20% Overflow damage and velocity\nIncreased movement and jump speed when on Overflow");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.IsOnOverflow())
            {
                modPlayer.OverflowDamage += .2f;
                modPlayer.OverflowVelocity += .2f;
                player.moveSpeed += .25f;
                player.jumpSpeedBoost += 3;
                player.maxRunSpeed += 3;
                player.accRunSpeed += 3;
            }
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RuneOfTheAncients>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Eruption>(), 1);
            recipe.AddIngredient(Mod, nameof(SoulOfHaught), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}