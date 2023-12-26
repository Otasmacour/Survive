using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class PadlockCode : Item
    {
        public override string getItemName()
        {
            return "Padlock code";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 7;
        public override string useSoundFileName => "PadlockOpening";
        public override string dropSoundFileName => "PadlockCodeDrop";
        public PadlockCode(SoundsController soundsController) : base(soundsController)
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
            if (mainDoor != null)
            {
                if (mainDoor.codePadlock) { mainDoor.codePadlock = false; soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName))); }
            }
        }
        public override char GetSymbol(Map map)
        {
            return 'P';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
