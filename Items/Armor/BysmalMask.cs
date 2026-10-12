using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BysmalMask : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+10% Damage \n+10% Additional Damage when in the Etherial");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 16;
        }

        public override void UpdateEquip(Player player)
        {
            float dmgBoost = .1f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.Etherable > 0 || NewDawnWorld.downedEtheria)
                dmgBoost += .1f;

            player.GetDamage(DamageClass.Generic) += dmgBoost;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BysmalBar>(), 15);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}