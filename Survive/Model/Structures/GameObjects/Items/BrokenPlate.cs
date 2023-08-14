using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    namespace Survive
    {
        class BrokenPlate : Item
        {
            public override string itemName => "Broken plate";
            public override bool takesUpSpaceInTheInventory => true;
            public override int noiseLevel => 3;
            public override void PickUp(Character character)
            {
                pickUp(character);
            }
            public override void Drop(Character character)
            {
                drop(character);
            }
            public override void Use(Character character)
            {

            }
            public override char symbol => 'b';
            public override int GetPriorityNumber()
            {
                return 80;
            }
        }
    }
}
