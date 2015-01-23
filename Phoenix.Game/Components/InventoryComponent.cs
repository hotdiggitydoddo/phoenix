using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class InventoryComponent
    {
        public uint Gold { get; set; }
        public List<int> Items { get; private set; }
        public short MaxSlots { get; set; }
        public InventoryComponent()
        {
            Items = new List<int>();
        }

        public InventoryComponent(short slots)
        {
            MaxSlots = slots;
        }

        public bool IsFull { get { return Items.Count == MaxSlots; } }
    }
}
