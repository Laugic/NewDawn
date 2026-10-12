using Terraria;

namespace NewDawn.Buffs
{
	public class Midnight : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Midnight");
			// Description.SetDefault("'Midnight's Moon glows bright on you.'\nUnleash a Midnight Burst when changing Mysticism");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			NewDawnPlayer.Get(player).Midnight =true;
        }
        
	}
}
