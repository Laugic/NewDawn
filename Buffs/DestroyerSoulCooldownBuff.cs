//using Terraria;
//using Terraria.ID;

//namespace NewDawn.Buffs
//{
//    public class DestroyerSoulCooldownBuff : NewDawnBuff
//    {
//        public override void SetStaticDefaults()
//        {
//            // DisplayName.SetDefault("Destroyer Soul Cooldown");
//            // Description.SetDefault("Your Destroyer effect is on Cooldown.");
//            Main.debuff[Type] = false;
//            Main.buffNoSave[Type] = true;
//            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
//        }

//        public override void Update(Player player, ref int buffIndex)
//        {
//            player.GetModPlayer<NewDawnPlayer>().DestroyerCooldown = true;
//        }
//    }
//}
