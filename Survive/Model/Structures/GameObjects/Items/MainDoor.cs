using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MainDoor : Item
    {
        public bool possiblePassage
        {
            get
            {
                if (plankLock || padlock || codeLock) { return false; }
                else { return false; }
            }
        }
        public bool plankLock = true;
        public bool padlock = true;
        public bool codeLock = true;
        public override string getItemName()
        {
            return "Main door";
        }
        public override bool takesUpSpaceInTheInventory => throw new NotImplementedException();
        public override int noiseLevel => throw new NotImplementedException();
        public override int floorNumberWhereItemSpawns => 0;
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => throw new NotImplementedException();
        public MainDoor(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
        }
        public override void PickUp(Character character) { }
        public override void Drop(Character character) { }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts) { }
        public override char GetSymbol(Map map)
        {
            return 'm';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
