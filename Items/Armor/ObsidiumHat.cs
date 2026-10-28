using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ObsidiumHat : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
}

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ObsidiumLongcoat>() && legs.type == ModContent.ItemType<ObsidiumPants>();
        }


        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.GetDamage(DamageClass.Magic) += 0.15f;
        }

        public override void UpdateArmorSet(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.setBonus = this.GetLocalization("SetBonus").Value;
            modPlayer.Obsidium = true;

            player.statManaMax2 += 60;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 4);
            recipe.AddIngredient(ModContent.ItemType<DarkShard>(), 1);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}