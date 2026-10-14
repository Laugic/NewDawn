using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class JetDagger : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.width = 12;
            Item.height = 12;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.JetDaggerProjectile>();
            Item.shootSpeed = 16f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.noUseGraphic = true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(SteamBar), 18);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFraught>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}