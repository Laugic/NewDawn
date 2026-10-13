using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Summon
{
    public class ArcticHydra : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Arctic Hydra Staff");
            // Tooltip.SetDefault("'The beast of many Heads'\nWhen in the Etherial after defeating Etheria, Double Damage and Aggro range.");
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 16;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<ArcticHydraHead>();
            Item.shootSpeed = 0;
            Item.buffType = ModContent.BuffType<Buffs.ArcticHydraBuff>();
            Item.buffTime = 60;
        }

        public override void HoldItem(Player player)
        {
            if ((NewDawnWorld.downedEtheria || NewDawnPlayer.Get(player).Etherable > 0) && NewDawnWorld.downedTrueEtheria)
            {
                Item.damage = 200;
            }
            else
            {
                Item.damage = 100;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 14);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 6);
            recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}