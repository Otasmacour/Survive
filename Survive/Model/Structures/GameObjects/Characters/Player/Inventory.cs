using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Inventory
    {
        public List<Item> list = new List<Item>();
        public Item currentlyHeldItem;
        public int inventorySize;
        public Inventory(int inventorySize)
        {
            this.inventorySize = inventorySize;
        }
    }
}