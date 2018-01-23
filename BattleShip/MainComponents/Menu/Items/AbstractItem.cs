using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.MainComponents.Menu.Items
{
    public abstract class AbstractItem
    {
        public int NumberSelector { get; set; }

        public string AlternateSelector { get; set; }

        public string DisplayName { get; set; }

        public abstract void Execute();

        public string GetAsMenuPosition()
        {
            return string.Format("{0} - {1}", this.NumberSelector, this.DisplayName);
        }
    }
}
