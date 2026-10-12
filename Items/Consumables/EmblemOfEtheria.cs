using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Consumables
{
	public class EmblemOfEtheria : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Emblem of Etheria");
            // Tooltip.SetDefault("Calls Etheria\n\'This seems like a terrible idea.\'");
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
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Etheria>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime && !NewDawnWorld.downedEtheria)
                return false;
            else if (NPC.CountNPCS(ModContent.NPCType<Etheria>()) > 0)
                return false;
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpookyWood, 75);
            recipe.AddIngredient(ItemID.Ectoplasm, 15);
            recipe.AddTile(TileID.DemonAltar);
			recipe.Register();

            Recipe arecipe = CreateRecipe();
            arecipe.AddIngredient(Mod, nameof(EtherialEssence), 15);
            arecipe.AddTile(TileID.DemonAltar);
            arecipe.Register();
        }
	}
}