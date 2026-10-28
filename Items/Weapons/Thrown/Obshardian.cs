using NewDawn.Items.Materials;
using NewDawn.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Thrown
{
    public class Obshardian : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.damage = 34;           
            Item.DamageType = DamageClass.Throwing;             
            Item.noMelee = true;
            Item.width = 14;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 20;   
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;       
            Item.shoot = ModContent.ProjectileType<ObshardianP>();  
            Item.shootSpeed = 16f;     
            Item.useTurn = true;
            Item.maxStack = 9999;       
            Item.consumable = true;  
            Item.noUseGraphic = true;

        }

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Item.useAnimation = Item.useTime = 24 - modPlayer.ObsidiumHeart * 2;
            base.HoldItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), type, damage, knockback, Main.myPlayer);
            Main.projectile[id].ai[1] = modPlayer.ObsidiumHeart;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(111);
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 4);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}