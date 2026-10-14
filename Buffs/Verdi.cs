using Terraria;

namespace NewDawn.Buffs
{
	public class Verdi : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Verdi");
			// Description.SetDefault("+10% Max Run Speed");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            NewDawnPlayer.Get(player).Verdi = 2;
        }
        
	}
}
