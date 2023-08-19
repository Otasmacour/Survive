using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Shovel : Item
    {
        public override string itemName => "Shovel";
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 1;
        public override string soundFileName => "Shovel";
        public Shovel(GameInformations gameInformations) : base(gameInformations)
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
        }
        public override void Use(Character character)
        {

        }
        public override char symbol => 's';
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
