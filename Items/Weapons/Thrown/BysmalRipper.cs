using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class BysmalRipper : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.width = 12;
            Item.height = 12;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.BysmalRipperProjectile>();
            Item.shootSpeed = 26f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 18);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 8);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}