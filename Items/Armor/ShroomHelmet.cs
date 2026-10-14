using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ShroomHelmet : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
}

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ShroomChest>() && legs.type == ModContent.ItemType<ShroomPants>();
        }


        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDamage += .08f;
        }

        public override void UpdateArmorSet(Player player)
        {
        global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.setBonus = "Overflow attacks shoot Spores from your head\nOverflow slowly accrues over time";
            mysticPlayer.ShroomOverflow = 2;

            if (mysticPlayer.MysticHold > 0)
            {
                if (mysticPlayer.Lux >= mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost && mysticPlayer.Lux < (mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow)
                    mysticPlayer.Lux += 1f / 20f;
                if (mysticPlayer.Vis >= mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost && mysticPlayer.Vis < (mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow)
                    mysticPlayer.Vis += 1f / 20f;
                if (mysticPlayer.Mundus >= mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost && mysticPlayer.Mundus < (mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow)
                    mysticPlayer.Mundus += 1f / 20f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(183, 10);
            recipe.AddIngredient(176, 6);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}