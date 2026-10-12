using NewDawn.Items.Loot;
using NewDawn.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    public class AnDioYo : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AnDio-Yo");
            // Tooltip.SetDefault("A marball of fun");

            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.width = 32;
            Item.height = 26;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.knockBack = 2.5f;
            Item.damage = 32;
            Item.rare = ItemRarityID.Green;

            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 1);
            Item.shoot = ModContent.ProjectileType<AnDioYoProjectile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(3081, 15);
            recipe.AddIngredient(3086, 25);
            recipe.Register();
        }
    }
}
