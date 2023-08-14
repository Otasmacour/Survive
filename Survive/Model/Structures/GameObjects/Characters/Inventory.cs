using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Inventory
    {
        public List<Item> items = new List<Item>();
        public Item currentlyHeldItem;
        public int inventorySize;
        public Inventory(int inventorySize)
        {
            this.inventorySize = inventorySize;
        }
        public void InventoryUpdate()
        {
            if(items.Count == 0)
            {
                currentlyHeldItem = null;
            }
            else
            {
                if(currentlyHeldItem == null)
                {
                    currentlyHeldItem = items[0];
                }
            }
        }
    }
}