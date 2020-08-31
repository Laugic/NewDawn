using Terraria.ModLoader;
using WebmilioCommons.Commands;

namespace NewDawn.Commands
{
    public abstract class NDDebugCommand : StandardCommand
    {
        protected NDDebugCommand(string command, CommandType type) : base(command, type)
        {
        }


        public override bool Autoload(ref string name) => true;
    }
}