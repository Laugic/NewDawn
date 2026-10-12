using Microsoft.Xna.Framework;
using NewDawn.Items.Loot;
using NewDawn.Projectiles.BossSummons;
using NewDawn.Projectiles.Special;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables
{
    public class SuspiciousTrainWhistle : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Suspicious Steam Whistle");
            // Tooltip.SetDefault("Summons Steam Train");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 40;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Nothing>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<NPCs.SteamTrain.SteamTrainOld>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player) => NPC.CountNPCS(ModContent.NPCType<NPCs.SteamTrain.SteamTrainOld>()) < 1;

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.Cog, 60);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}