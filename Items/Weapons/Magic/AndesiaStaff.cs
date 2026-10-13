using Microsoft.Xna.Framework;
using NewDawn.Items.Loot;
using NewDawn.Projectiles.Magic;
using NewDawn.Projectiles.Special;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
    public class AndesiaStaff : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Staff of Honor");
            // Tooltip.SetDefault("Right click to place \n'Onto War!'");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.mana = 4;
            Item.width = 88;
            Item.height = 88;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = 100000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 0f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.Magic.AndesiaStaffProjectile>();
                Item.noUseGraphic = true;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<Nothing>();
                Item.noUseGraphic = false;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<AndesiaStaffProjectile>()] < 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.player[Main.myPlayer] == player)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)Main.MouseWorld.X - 3 + Main.rand.Next(0, 6), (int)Main.MouseWorld.Y - 360 - 3 + Main.rand.Next(0, 6)), new Vector2(0, 0), ModContent.ProjectileType<Dioritite>(), (int)Item.damage, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)Main.MouseWorld.X - 3 + Main.rand.Next(0, 6), (int)Main.MouseWorld.Y + 360 - 3 + Main.rand.Next(0, 6)), new Vector2(0, 0), ModContent.ProjectileType<Andesimite>(), (int)Item.damage, 3, Main.myPlayer);
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(3086, 32);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}