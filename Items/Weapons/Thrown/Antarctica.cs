using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Thrown;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class Antarctica : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Antarctica");
            // Tooltip.SetDefault("'A blizzard hails from above'\nWhen in the Etherial after defeating Etheria, rain down more Hail,\nand stay stuck in enemies for twice as long.");
        }
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.width = 12;
            Item.height = 12;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AntarcticaProjectile>();
            Item.shootSpeed = 26f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.NorthPole);
            recipe.AddIngredient(null, nameof(BysmalBar), 15);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 6);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}