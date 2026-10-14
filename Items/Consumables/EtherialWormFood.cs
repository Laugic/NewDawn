using NewDawn.Items.Loot;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Consumables
{
	public class EtherialWormFood : NewDawnItem
    {

        public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), NPCID.EaterofWorldsHead, knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player) => player.ZoneCorrupt && NewDawnWorld.downedEtheria && NPC.CountNPCS(NPCID.EaterofWorldsHead) < 1;

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, nameof(EtherialEssence), 2);

            recipe.AddTile(TileID.DemonAltar);

			recipe.Register();
        }
	}
}