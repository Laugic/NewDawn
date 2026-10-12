using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.Mounts.SteamTrain
{
	public class SteamTrainMount : ModMount
	{
        public bool boosted = false;

		public override void SetStaticDefaults()
		{
            boosted = false;
			MountData.spawnDust = ModContent.DustType<Steam>();
			MountData.buff = ModContent.BuffType<SteamTrainMountBuff>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0.5f;
			MountData.runSpeed = 25f;
			MountData.dashSpeed = 8f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 4;
			MountData.acceleration = 0.04f;
			MountData.jumpSpeed = 4f;
			MountData.blockExtraJumps = true;
			MountData.totalFrames = 6;
			MountData.constantJump = true;

			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}

			MountData.playerYOffsets = array;
			MountData.xOffset = 75;
			MountData.bodyFrame = 3;
			MountData.yOffset = -8;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 6;
			MountData.standingFrameDelay = 999999;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 6;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 6;
			MountData.idleFrameDelay = 999999;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;

			if (Main.netMode != 2)
			{
				MountData.textureWidth = MountData.backTexture.Width + 20;
				MountData.textureHeight = MountData.backTexture.Height;
			}
		}

		public override void UpdateEffects(Player player)
		{
            if (Math.Abs(player.velocity.X) > 5f)
            {
                Rectangle rect = player.getRect();
                var dist = 86 * player.direction;
                Dust.NewDust(new Vector2(player.Center.X + dist, rect.Y - 26), 0, -6, ModContent.DustType<TrainSteam>());
                //Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, 0, ModContent.DustType<TrainSteam>());

                if (boosted == false)
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/train_whistle"));
                    boosted = true;
                }
            }

            else boosted = false;
		}
	}
}