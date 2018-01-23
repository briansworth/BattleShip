using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.MainComponents.Menu.Items
{
    public class ExitItem : AbstractItem
    {
        public ExitItem()
        {
            this.NumberSelector = 2;
            this.DisplayName = "Exit";
            this.AlternateSelector = "exit";
        }

        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
