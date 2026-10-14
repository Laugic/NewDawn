using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Tools
{
    public class Drillinator : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 82;
            Item.height = 34;
            Item.useTime = 1;
            Item.useAnimation = 11;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.pick = 2500;
            Item.tileBoost += 8;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Tools.DrillinatorProjectile>();
            Item.shootSpeed = 40f;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            float mag = 28f;
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
            //player.pickSpeed = 0.00001f;
            for (int i = -8; i < 9; i++)
            {
                for (int j = -8; j < 9; j++)
                {
                    int k = Main.SmartCursorX + i;
                    int l = Main.SmartCursorY + j;
                    Terraria.WorldGen.KillTile(k, l);
                }
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            player.pickSpeed -= 0.25f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            float mag = 28f;
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DrillOTron10000>(), 1);
            recipe.AddIngredient(3467, 12);
            recipe.AddIngredient(ModContent.ItemType<GalacticFragment>(), 8);
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}