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
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDamage += .12f;
            mysticPlayer.MysticDuration += 1f;
        }

        public override void UpdateArmorSet(Player player)
        {
        global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.setBonus = "Your Max Mana is added to your Potentias\nIncreased Mana and Potentia regen";
            mysticPlayer.LuxMax += player.statManaMax2 / 3;
            mysticPlayer.VisMax += player.statManaMax2 / 3;
            mysticPlayer.MundusMax += player.statManaMax2 / 3;

            mysticPlayer.LuxUnuseRegen += .04f;
            mysticPlayer.VisUnuseRegen += .04f;
            mysticPlayer.MundusUnuseRegen += .04f;

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