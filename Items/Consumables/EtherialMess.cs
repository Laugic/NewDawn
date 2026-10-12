using NewDawn.Items.Loot;
using NewDawn.NPCs.PreTrio;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Consumables
{
	public class EtherialMess : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Summons Ragnar");
        }

        public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			//item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GeneralBossSpawn>(), ModContent.NPCType<Ragnar>(), knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            return (modPlayer.zoneObsidium && NewDawnWorld.downedEtheria && NPC.CountNPCS(ModContent.NPCType<Ragnar>()) < 1);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

		    recipe.AddIngredient(Mod, nameof(EtherialEssence), 3);

		    recipe.AddTile(TileID.DemonAltar);

		    recipe.Register();
        }
	}
}