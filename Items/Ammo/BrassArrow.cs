using NewDawn.Items.Loot;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class BrassArrow : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(copper: 16);
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<BrassArrowProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(33);
            recipe.AddIngredient(Mod, nameof(SteamBar), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
