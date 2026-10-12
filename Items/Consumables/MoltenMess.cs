using NewDawn.Items.Materials;
using NewDawn.NPCs.PreTrio;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Consumables
{
	public class MoltenMess : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Summons Ragnar\n\'Chilled Lava.\' \nGuardian of the Obsidium.");
        }


        public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Ragnar>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player) => NewDawnPlayer.Get(player).zoneObsidium && NPC.CountNPCS(ModContent.NPCType<Ragnar>()) < 1;

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 5);
            recipe.AddIngredient(ItemID.Obsidian, 8);

            recipe.AddTile(TileID.DemonAltar);

			recipe.Register();
        }
	}
}