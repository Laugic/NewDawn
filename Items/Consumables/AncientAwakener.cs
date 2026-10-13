using Microsoft.Xna.Framework;
using NewDawn.NPCs.RockTwins;
using NewDawn.Projectiles.BossSummons;
using NewDawn.Projectiles.Special;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables
{
    public class AncientAwakener : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Summons Dioritus\n\'The rulers of the caverns are fearsome foes\'");
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
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Dioritus>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneRockLayerHeight && NPC.CountNPCS(ModContent.NPCType<Andesia>()) < 1 && NPC.CountNPCS(ModContent.NPCType<Dioritus>()) < 1 && NPC.CountNPCS(ModContent.NPCType<AnDio3>()) < 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.MarbleBlock, 40);
            recipe.AddIngredient(ItemID.GoldenKey);

            recipe.AddTile(TileID.DemonAltar);

            recipe.Register();


            // TODO Move this to proper class.
            recipe = Recipe.Create(ItemID.GoldenKey);

            recipe.AddRecipeGroup(NewDawn.GOLD_BARS_GROUP, 4);
            recipe.AddIngredient(ItemID.Bone);

            recipe.AddTile(TileID.Hellforge);

            recipe.Register();
        }
    }
}