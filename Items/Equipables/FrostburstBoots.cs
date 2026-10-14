using NewDawn.Control;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class FrostburstBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.RocketBoots.Add(Item.type);
            // Tooltip.SetDefault("Allows flight, super fast running, and extra mobility on ice\nGrants the ability to double jump\nIncreases jump height and negates fall damage\n15% increased movement speed");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 8);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .15f;
            player.accRunSpeed += 4.5f;
            player.rocketBoots = 3;
            player.iceSkate = true;
            player.GetJumpState(ExtraJump.BlizzardInABottle).Enable();
            player.noFallDmg = true;
            player.jumpSpeedBoost += 3;
        }


        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            for (int j = 0; j < player.armor.Length; j++)
            {
                if (j != slot && NewDawnVars.RocketBoots.Contains(player.armor[j].type))
                    return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostsparkBoots);
            recipe.AddRecipeGroup(RecipeManager.ColoredBalloon);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}