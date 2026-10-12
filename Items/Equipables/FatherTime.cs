using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class FatherTime : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Father Time");
            // Tooltip.SetDefault("+15% Increased Damage while time is stopped\nReduces cooldown between Time Stops\nIncreases Duration of Time Stop\n'Mastery of Time'");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 64;
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
            modPlayer.zaWarudoDuration += (int)(1.75 * 60);
            modPlayer.zCoolDown -= 10 * 60;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TimeWinder>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Clockface>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HandsOfTime>(), 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(Mod, nameof(Gear), 20);
            recipe.AddIngredient(ItemID.Cog, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}