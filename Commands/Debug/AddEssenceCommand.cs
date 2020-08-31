using NewDawn.Essences.Primals;
using NewDawn.Items.AnimaCrystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Commands
{
    public sealed class AddEssenceCommand : NDDebugCommand
    {
        public AddEssenceCommand() : base("nd_addess", CommandType.Chat)
        {
        }


        protected override void ActionLocal(CommandCaller caller, Player player, string input, string[] args)
        {
            if (player.HeldItem.IsAir || player.HeldItem.modItem == default || !(player.HeldItem.modItem is AnimaCrystal ac))
                return;

            ac.AddEssence(new AquaEssence());
        }
    }
}