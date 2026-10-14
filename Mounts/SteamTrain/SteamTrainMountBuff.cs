using NewDawn.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Mounts.SteamTrain
{
	public class SteamTrainMountBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Train");
			// Description.SetDefault("Choo Choo!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<SteamTrainMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
