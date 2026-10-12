using Terraria;

namespace NewDawn.Buffs
{
	public class ConjurationBoost : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Conjuration Power");
			// Description.SetDefault("'Otherworldly energy strengthens you.'\n+50% Overflow");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).GlobalOverflow += .5f;
        }
        
	}
}
