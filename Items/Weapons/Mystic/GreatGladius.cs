using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Special;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Mystic
{
    public class GreatGladius : MysticItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gladius of The Great Moldyrian");
            // Tooltip.SetDefault("Praise the gods\nIlusion inflicts 'Daybroken'\nAttacks differently projectiles based on Mysticism");
        }

        public override void SetMysticDefaults()
        {
            Item.damage = 1500;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.noMelee = false;
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            Item.scale = 1.5f;
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode != 1)
                return true;
            else return false;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 2000;
            Item.useTime = 26;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 8;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.scale = 2.25f;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 1500;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 4;
            Item.shootSpeed = 12f;
            Item.shoot = ProjectileID.Daybreak;
            Item.scale = 1.5f;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 1000;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            Item.scale = 2f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 2)
                target.AddBuff(BuffID.Daybreak, (int)(4 * 60 * modPlayer.MysticDuration));
            if (modPlayer.MysticMode == 3)
            {
                if (Main.player[Main.myPlayer] == player && player.ownedProjectileCounts[ModContent.ProjectileType<GreatGladiusConjuration1>()] < 2)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<GreatGladiusConjuration1>(), damage, knockback, Main.myPlayer);
            }
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3467, 16);
            recipe.AddIngredient(ModContent.ItemType<GalacticFragment>(), 8);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}