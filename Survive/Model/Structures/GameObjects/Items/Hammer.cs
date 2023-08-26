using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Hammer : Item
    {
        public override string itemName => "Hammer";
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 10;
        public override int floorNumberWhereItemSpawns => -1;
        public override string dropSoundFileName => "FallingTool";
        public Hammer(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
        }
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
        public override char GetSymbol(Map map)
        {
            return 'H';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
