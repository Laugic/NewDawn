using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FrigidHelmet : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FrigidChestplate>() && legs.type == ModContent.ItemType<FrigidLeggings>();
        }


        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.Ranged) += 0.1f;
        }

        public override void UpdateArmorSet(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.setBonus = "+4 Defense\n+25% Snowball Damage\nUnleash Ice Shards when struck";
            modPlayer.Frigid = true;
            modPlayer.SnowDamage += .25f;
            player.statDefense += 4;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 12);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}