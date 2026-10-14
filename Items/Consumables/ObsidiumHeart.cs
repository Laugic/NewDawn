using NewDawn.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables
{
	public class ObsidiumHeart : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases the strength of Obsidium weapons\nIncreases your Max Potentia by 10\nCan be used 5 times\nGrown on Magma Shards");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
			Item.height = 28;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
            Item.consumable = true;
		}
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            if (player.itemAnimation == player.itemAnimationMax - 1 && NewDawnPlayer.Get(player).ObsidiumHeart < 5)
            {
                Item.stack--;
                modPlayer.ObsidiumHeart++;
                mysticPlayer.LuxMaxPermaBoost += 10;
                mysticPlayer.VisMaxPermaBoost += 10;
                mysticPlayer.MundusMaxPermaBoost += 10;
            }
            return base.UseItem(player);
        }
    }
}