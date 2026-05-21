using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Abstracts
{
    public abstract class AbstractStorageTile : ModTileEntity, AbstractStorage
    {
        public abstract Item[] Items { get; set; }

        public virtual bool FilterCheck(Item item, int slot)
        {
            return true;
        }

        public override void OnNetPlace()
        {
            // This hook is only ever called on the server; its purpose is to give more freedom in terms of syncing FROM the server to clients, which we take advantage of
            // by making sure to sync whenever this hook is called:
            NetMessage.SendData(MessageID.TileEntitySharing, number: ID, number2: Position.X, number3: Position.Y);
        }
        /*
        public override void NetSend(BinaryWriter writer)
        {
            // We want to make sure that our data is synced properly across clients and server.
            // NetSend is called whenever a TileEntitySharing message is sent, so the game will handle this automatically for us,
            // granted that we send a message when we need to.
            for (int i = 0; i < Items.Length; i++)
            {
                char[] itemType = new char[Items[i].type.ToString().Length];
                for (int j = 0; j < Items[i].type.ToString().Length; j++)
                    itemType[j] = Items[i].type.ToString()[j];
                writer.Write(itemType, Items[i].stack, 0);
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                char[] itemType = reader.ReadChars(4);
                int index = reader.ReadInt64();
            }
        }*/
    }
}
