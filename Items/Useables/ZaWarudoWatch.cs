using NewDawn.Buffs;
using NewDawn.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.Items.Useables
{
    public class ZaWarudoWatch : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.expert = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(ModContent.BuffType<TimeExhausted>());
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            //TODO: Add sound effect for this item
            SoundStyle switchSound = new SoundStyle("NewDawn/Sounds/zawarudo")
            {
                Volume = 0.9f,
                PitchVariance = 0.0f,
                MaxInstances = 3,
            };
            SoundEngine.PlaySound(switchSound, player.Center);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            //TimeManagement.TryAlterTime(new TimeAlterationRequest(player, modPlayer.zaWarudoDuration, 0));
            if (NewDawn.zaWarudo < modPlayer.zaWarudoDuration)
            {
                NewDawn.zaWarudo = modPlayer.zaWarudoDuration;
                LaugicalGlobalNPCs.zTime = modPlayer.zaWarudoDuration;
            }
            foreach (Player player2 in Main.player)
                player.AddBuff(ModContent.BuffType<TimeExhausted>(), modPlayer.zCoolDown, true);
            return true;
        }
    }
}