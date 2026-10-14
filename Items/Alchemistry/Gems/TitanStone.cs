using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class TitanStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+10% Damage reduction \n+10% Damage and critical strike chance \n Attackers take damage \nSet your enemies ablaze");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.10f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.SoulStoneVisuals)
            {
                if (modPlayer.inf)
                    player.AddBuff(116, 2);
            }
            if (player.thorns < 1f)
            {
                player.thorns = 0.333333343f;
            }
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PanzerCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MachtCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<MineralEnchanterTile>());
            recipe.Register();
        }
    }
}