using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class EnginatorT : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.width = 106;
            Item.height = 74;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.reuseDelay = 20;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EnginatorTProj>();
            Item.shootSpeed = 16f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFraught>(), 8);
            recipe.AddIngredient(Mod, nameof(Gear), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}