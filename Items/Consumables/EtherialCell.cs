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
    public class EtherialCell : NewDawnItem
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
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), NPCID.Golem, knockback, player.whoAmI);
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), NPCID.GolemHead, knockback, player.whoAmI);
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), NPCID.GolemFistLeft, knockback, player.whoAmI);
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), ModContent.ProjectileType<GeneralBossSpawn>(), NPCID.GolemFistRight, knockback, player.whoAmI);
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return NewDawnWorld.downedEtheria && NPC.CountNPCS(NPCID.GolemHead) < 1 && NPC.CountNPCS(NPCID.GolemHeadFree) < 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.LihzahrdPowerCell);

            recipe.AddIngredient(Mod, nameof(EtherialEssence), 5);

            recipe.AddTile(TileID.DemonAltar);

            recipe.Register();
        }
    }
}