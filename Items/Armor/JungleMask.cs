using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class JungleMask : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Jungle Hood");
            // Tooltip.SetDefault("Mystic damage increased by 12%\n+100% Mystic Duration");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 90);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == 229 && legs.type == 230;
        }


        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.MysticDamage += .12f;
            modPlayer.MysticDuration += 1f;
        }

        public override void UpdateArmorSet(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.setBonus = "Your Max Mana is added to your Potentias\nIncreased Mana and Potentia regen";
            modPlayer.LuxMax += player.statManaMax2 / 3;
            modPlayer.VisMax += player.statManaMax2 / 3;
            modPlayer.MundusMax += player.statManaMax2 / 3;

            modPlayer.LuxUnuseRegen += .04f;
            modPlayer.VisUnuseRegen += .04f;
            modPlayer.MundusUnuseRegen += .04f;

            player.manaRegen += 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(331, 8);
            recipe.AddIngredient(1124, 4);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}