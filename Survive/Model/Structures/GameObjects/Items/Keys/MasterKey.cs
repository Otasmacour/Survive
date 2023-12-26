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
        public override int noiseLevel => 7;
        public override string useSoundFileName => "UnlockingChest";
        public override string dropSoundFileName => "KeyDrop";
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
            Chest chest = character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].OfType<Chest>().FirstOrDefault();
            if (mainDoor != null)
            {
                if (mainDoor.padlock) { mainDoor.padlock = false; soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName))); }
            }
            else if (chest != null) { alerts.Add("Master key is not ment to unlock a regular chest"); }
            else { alerts.Add("You have to find something to unlock before you unlock anything"); }
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