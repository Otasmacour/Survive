using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class ItemManipulation
    {
        public void DropItem(Character character, Item item)
        {
            Console.WriteLine("Dropping items coming soon, btw ReadKey is waiting for a reply");
            Console.ReadKey();
        }
        public void PickUpItem(Character character)
        {
            Console.WriteLine("Picking up items coming soon, btw ReadKey is waiting for a reply");
            Console.ReadKey();
        }
        public void UseItem(Character character, Item item)
        {
            Console.WriteLine("Using items coming soon, btw ReadKey is waiting for a reply");
            Console.ReadKey();
        }
    }
}
