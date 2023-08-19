using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Plate : Item
    {
        public override string itemName => "Plate";
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 3;
        public override string soundFileName => "Plate";
        public Plate(GameInformations gameInformations) : base(gameInformations) 
        {
            this.gameInformations = gameInformations;
        }
        public override Sound GetSound()
        {
            return new Sound(soundFileName, noiseLevel);
        }
        public override void PickUp(Character character)
        {
            pickUp(character);
        }
        public override void Drop(Character character)
        {
            drop(character);
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
