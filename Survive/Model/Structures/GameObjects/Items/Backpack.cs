using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BackPack : Item
    {
        public override string getItemName()
        {
            return "Backpack";
        }
        public override bool takesUpSpaceInTheInventory => false;
        public override int noiseLevel => 1;
        public override string useSoundFileName => "PuttingOnBackpack";
        public override string dropSoundFileName => throw new NotImplementedException();
        public BackPack(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
        }
        public override void PickUp(Character character)
        {
            pickUp(character);
            character.inventory.inventorySize += 2;
            soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName)));
        }
        public override void Drop(Character character)
        {
            drop(character);
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            
        }
        public override char GetSymbol(Map map)
        {
            return 'B';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
