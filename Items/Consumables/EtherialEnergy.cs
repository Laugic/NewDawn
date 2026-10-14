using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Consumables
{
	public class EtherialEnergy : NewDawnItem
    {

        public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item4;
			Item.consumable = true;
		}
        
        public override bool CanUseItem(Player player) => !NewDawnPlayer.Get(player).etherialSlot;

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NewDawnPlayer.Get(player).etherialSlot = true;

            return true;
        }
    }
}