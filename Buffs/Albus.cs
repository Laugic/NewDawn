using Terraria;

namespace NewDawn.Buffs
{
	public class Albus : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Albus");
			// Description.SetDefault("+10% Damage Reduction");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.10f;
        }
        
	}
}
