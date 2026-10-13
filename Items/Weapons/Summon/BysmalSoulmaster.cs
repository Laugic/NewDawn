using Microsoft.Xna.Framework;
using System;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Summon
{
    public class BysmalSoulmaster : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bysmal Soulmaster");
            // Tooltip.SetDefault("'Unleash them'\nWhen in the Etherial after defeating Etheria, attack twice as fast.");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 250;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 9;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item122;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SoulmasterOrb>();
            Item.shootSpeed = 14f;
        }

        public override void HoldItem(Player player)
        {
            if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(player).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
            {
                Item.useTime = 30;
                Item.useAnimation = 30;
            }
            else
            {
                Item.useTime = 60;
                Item.useAnimation = 60;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float theta = 0;
            float mag = 8;
            for(int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<SoulmasterOrb>(), damage, 3f, player.whoAmI);
                theta += (float)Math.PI / 4;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 15);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 8);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}