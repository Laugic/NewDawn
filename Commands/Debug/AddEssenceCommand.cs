using NewDawn.Essences.PotionEssences;
using NewDawn.Essences.Primals;
using NewDawn.Items.AnimaCrystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Commands
{
    public sealed class AddAquaEssenceCommand : NDDebugCommand
    {
        public AddAquaEssenceCommand() : base("nd_addess_aq", CommandType.Chat)
        {
        }


        protected override void ActionLocal(CommandCaller caller, Player player, string input, string[] args)
        {
            if (player.HeldItem.IsAir || player.HeldItem.modItem == default || !(player.HeldItem.modItem is AnimaCrystal ac))
                return;

            ac.AddEssence(new AquaEssence());
        }
    }
    public sealed class AddIgnisEssenceCommand : NDDebugCommand
    {
        public AddIgnisEssenceCommand() : base("nd_addess_ig", CommandType.Chat)
        {
        }


        protected override void ActionLocal(CommandCaller caller, Player player, string input, string[] args)
        {
            if (player.HeldItem.IsAir || player.HeldItem.modItem == default || !(player.HeldItem.modItem is AnimaCrystal ac))
                return;

            ac.AddEssence(new IgnisEssence());
        }
    }

    public sealed class AddPotionEssenceCommand : NDDebugCommand
    {
        public AddPotionEssenceCommand() : base("nd_addess_pot", CommandType.Chat)
        {
        }


        protected override void ActionLocal(CommandCaller caller, Player player, string input, string[] args)
        {
            if (player.HeldItem.IsAir || player.HeldItem.modItem == default || !(player.HeldItem.modItem is AnimaCrystal ac))
                return;

            ac.AddEssence(new AquaEssence());
        }
    }

    public sealed class AddIronskinEssenceCommand : NDDebugCommand
    {
        public AddIronskinEssenceCommand() : base("nd_addess_is", CommandType.Chat)
        {
        }


        protected override void ActionLocal(CommandCaller caller, Player player, string input, string[] args)
        {
            if (player.HeldItem.IsAir || player.HeldItem.modItem == default || !(player.HeldItem.modItem is AnimaCrystal ac))
                return;

            ac.AddEssence(new IronskinEssence());
        }
    }
}