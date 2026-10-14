using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Melee
{
    public class BysmalBrand : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.scale *= 1.25f;
            Item.damage = 110;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 144;
            Item.height = 144;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Borealis>();
            Item.shootSpeed = 18f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Aurora>(), 1);
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 12);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 5);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
        /*
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if(!NewDawnVars.ENPCs.Contains(target.type) && !NewDawnVars.etherial.Contains(target.type) && target.damage > 0 && target.boss == false)
            {
                target.GetGlobalNPC<EtherialGlobalNPC>().etherial = true;
            }
            target.AddBuff(ModContent.BuffType<Frostbite>(), 2 * 60);
        }*/
    }
}
