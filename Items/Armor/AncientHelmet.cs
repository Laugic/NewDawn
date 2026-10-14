using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientHelmet : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AncientArmor>() && legs.type == ModContent.ItemType<AncientGreaves>();
        }


        public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 6f;
            player.accRunSpeed += 6f;
            player.jumpSpeedBoost += 4;
        }

        public override void UpdateArmorSet(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            player.setBonus = "Increased Max Minions, Mana, and Potentia";
            mysticPlayer.LuxMax += 40;
            mysticPlayer.VisMax += 40;
            mysticPlayer.MundusMax += 40;
            player.statManaMax2 += 40;
            player.maxMinions += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Crystilla>(), 6);
            recipe.AddIngredient(ItemID.DesertFossil, 12);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}