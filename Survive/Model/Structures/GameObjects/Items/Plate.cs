using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Plate : Item
    {
        public override int noiseScale => 3;
        public override void PickUp(Character character)
        {
            character.inventory.currentlyHeldItem = this;
        }
        public override void Drop(Character character)
        {
            //Dropped Plate becomes BrokenPlate, not implemented yet
        }
        public override void Use(Character character)
        {

        }
        public override char symbol => 'p';
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
