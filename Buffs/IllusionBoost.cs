using Terraria;

namespace NewDawn.Buffs
{
	public class IllusionBoost : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Illusion Power");
			// Description.SetDefault("'The mind's energy strengthens you.'\n+100% Mystic Duration");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().MysticDuration += 1f;
        }
        
	}
}
