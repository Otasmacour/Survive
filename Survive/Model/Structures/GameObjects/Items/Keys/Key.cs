using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Key : Item
    {
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 2;
        public override int floorNumberWhereItemSpawns => 0;
        public override string dropSoundFileName => "FallingTool";
        public Key(SoundsController soundsController) : base(soundsController)
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
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
