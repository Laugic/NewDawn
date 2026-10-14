using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Projectiles.Special;
using NewDawn.Projectiles.Summon;
using NewDawn.Items.Loot;

namespace NewDawn.Items.Weapons.Summon
{
    public class TheMessenger : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.mana = 10;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = 25000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<RockTwins>();
            Item.buffTime = 3600;
        }
    
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
    	    int i = Main.myPlayer;
		    float num72 = Item.shootSpeed;
		    int num73 = damage;
		    float num74 = knockback;
    	    num74 = player.GetWeaponKnockback(Item, num74);
    	    player.itemTime = Item.useTime;
    	    Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		    Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		    Vector2 vector3 = Main.MouseWorld - vector2;
    	    float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		    float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		    if (player.gravDir == -1f)
		    {
			    num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
		    }
		    float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		    float num81 = num80;
		    if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		    {
			    num78 = (float)player.direction;
			    num79 = 0f;
			    num80 = num72;
		    }
		    else
		    {
			    num80 = num72 / num80;
		    }
    	    num78 = 0f;
		    num79 = 0f;
		    vector2.X = (float)Main.mouseX + Main.screenPosition.X;
		    vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(vector2.X, vector2.Y), new Vector2(num78, num79), ModContent.ProjectileType<AndesiaProbe>(), num73, num74, i, 0f, 0f);
            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(vector2.X, vector2.Y), new Vector2(num78, num79), ModContent.ProjectileType<DioritusProbe>(), num73, num74, i, 0f, 0f);
            
		    return player.altFunctionUse != 2;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(true);
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AndesiaCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DioritusCore>(), 1);
            recipe.AddIngredient(3081, 50);
            recipe.AddIngredient(3086, 50);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}