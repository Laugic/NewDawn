using NewDawn.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class AnDiuriken : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AnDiuriken");
            // Tooltip.SetDefault("Stacks up to 4");
        }
        public override void SetDefaults()
        {
            Item.damage = 36;           
            Item.DamageType = DamageClass.Throwing;            
            Item.noMelee = true;
            Item.width = 30;
            Item.height = 20;
            Item.useTime = 20;      
            Item.useAnimation = 20;   
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 100000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;       
            Item.shoot = ModContent.ProjectileType<AnDiurikenThrownProjectile>();
            Item.shootSpeed = 16f;     
            Item.useTurn = true;
            Item.maxStack = 4;       
            Item.consumable = false;  
            Item.noUseGraphic = true;

        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.AnDiurikenThrownProjectile>()] < Item.stack;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>(), 1);
            recipe.AddIngredient(3081, 10);
            recipe.AddIngredient(3086, 25);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}