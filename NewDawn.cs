using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn
{
	public class NewDawn : Mod
    {
        public NewDawn()
        {
            Instance = this;
        }

        public override void Load()
        {
            //MysticaUserInterface = new UserInterface();
        }

        public override void Unload()
        {
            Instance = null;
            //MysticaUserInterface = null;
        }


        //public static UserInterface MysticaUserInterface { get; private set; }
        public static NewDawn Instance { get; private set; }
    }
}