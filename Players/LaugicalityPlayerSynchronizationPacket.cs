//using System.IO;
//using Terraria;
//using Terraria.ID;

//namespace NewDawn
//{
//    public class NewDawnPlayerSynchronizationPacket : ModPlayerNetworkPacket<NewDawnPlayer>
//    {
//        protected override bool PostReceive(BinaryReader reader, int fromWho)
//        {
//            if (!IsResponse && Main.netMode == NetmodeID.MultiplayerClient)
//            {
//                IsResponse = true;
//                Send(Main.myPlayer, Player.whoAmI);
//            }

//            return base.PostReceive(reader, fromWho);
//        }


//        public string Focus
//        {
//            get => ModPlayer.Focus == null ? "" : ModPlayer.Focus.UnlocalizedName;
//            set
//            {
//                if (string.IsNullOrWhiteSpace(value))
//                    return;

//                ModPlayer.Focus = FocusManager.Instance[value];
//            }
//        }

//        public bool IsResponse { get; set; }
//    }
//}