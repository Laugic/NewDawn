using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;
using NewDawn.NPCs.Hypothema;
using NewDawn.Projectiles.BossSummons;
using NewDawn.Projectiles.Special;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables
{
    public class ChilledMesh : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Innocent Child");
            // Tooltip.SetDefault("Summons Hypothema\n'What are you doing to him?'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Nothing>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Hypothema>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneSnow && NPC.CountNPCS(ModContent.NPCType<Hypothema>()) < 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Snowball, 20);
            recipe.AddIngredient(ModContent.ItemType<Vitasilk>());
            recipe.AddTile(TileID.DemonAltar);

            recipe.Register();
        }
    }
}