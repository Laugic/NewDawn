using NewDawn.Essences;
using NewDawn.Essences.PotionEssences;
using NewDawn.Essences.Primals;
using NewDawn.Items.AnimaCrystals;
using System;
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
            if(args.Length > 0)
            {
                string essence = args[0];
                if (!essence.EndsWith("Essence"))
                {
                    if(essence.EndsWith("essence"))
                        essence.Replace("essence", "Essence");
                    else
                        essence += "Essence";
                }
                essence = char.ToUpper(essence[0]) + essence.Substring(1);
                try { ac.AddEssence(EssenceLoader.Instance.New(essence)); }
                catch (Exception e) { }
            }

        }
    }
}