using NewDawn.Items.Loot;
using NewDawn.NPCs.Bosses;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Consumables
{
	public class MechanicalMonitor : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steam-O-Vision");
            // Tooltip.SetDefault("Summons The Annihilator");
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
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<TheAnnihilator>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player) => NPC.CountNPCS(ModContent.NPCType<TheAnnihilator>()) < 1;

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.Lens, 3);
			recipe.AddIngredient(ItemID.Vertebrae, 3);
            recipe.AddIngredient(Mod, nameof(SoulOfHaught), 3);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 3);
            recipe.AddIngredient(ItemID.Cog, 20);

            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
            

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.Lens, 3);
            recipe.AddIngredient(ItemID.RottenChunk, 3);
            recipe.AddIngredient(Mod, nameof(SoulOfHaught), 3);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 3);
            recipe.AddIngredient(ItemID.Cog, 20);

            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
	}
}