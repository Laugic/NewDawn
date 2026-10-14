using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Temple.Consumables
{
    public class WarpPearl : ModItem
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 48;
            Item.height = 48;
            Item.useTime = Item.useAnimation = 20;
            Item.maxStack = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Misc.WarpPearl>();
            Item.shootSpeed = 10;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(source, position, velocity,
                    ModContent.ProjectileType<Projectiles.Misc.WarpPearl>(), 0, 0, player.whoAmI);
            }
            return false;
        }
    }
}