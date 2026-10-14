using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using NewDawn.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    public class ElderanClaymore : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 56;
            Item.crit = 20;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = false;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 16f;
            Item.useTurn = true;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.shoot = ModContent.ProjectileType<ElderanClaymoreProjectile>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.ownedProjectileCounts[ModContent.ProjectileType<ElderanClaymoreProjectile>()] < 3)
            {
                SoundEngine.PlaySound(SoundID.Item101, player.position);
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(ElderPearl), 12);
            recipe.AddIngredient(Mod, nameof(ElderockItem), 8);
            recipe.AddIngredient(Mod, nameof(ObsidiumRock), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
