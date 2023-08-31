using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MasterKey : Item
    {
        public override string getItemName()
        {
            return "Master key";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 3;
        public override int floorNumberWhereItemSpawns => 0;
        public override string useSoundFileName => "UnlockingChest";
        public override string dropSoundFileName => throw new NotImplementedException();
        public MasterKey(SoundsController soundsController) : base(soundsController)
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
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            MainDoor mainDoor = mapHelper.returnFunctions.AdjacentItems(character.mapWhereIsLocated, character.coordinates).OfType<MainDoor>().FirstOrDefault();
            if(mainDoor != null)
            {
                if (mainDoor.padlock) { mainDoor.padlock = false; soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName))); }
            }
        }
        public override char GetSymbol(Map map)
        {
            return 'M';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}