using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Chest : Item
    {
        public Item content;
        public void Unlock(Map map, Coordinates coordinates, Alerts alerts, bool violentlyOpening)
        {
            if (violentlyOpening) { soundsController.soundsToPLay.Enqueue((map, GetSound(violentyOpeningChestSoundFileName))); }
            else { soundsController.soundsToPLay.Enqueue((map, GetSound(useSoundFileName))); }
            map.twoDArray[coordinates.y, coordinates.x].Remove(this);
            map.twoDArray[coordinates.y, coordinates.x].Add(content);
        }
        public abstract string typeOfAssociatedKey {  get; }
        public override bool takesUpSpaceInTheInventory => false;
        public override int noiseLevel => 1;
        public abstract (int floorNumber, int roomNumber) locationMap { get; }
        public abstract (int y, int x) locationCoordinates { get; }
        public override string useSoundFileName => "UnlockingChest";
        string violentyOpeningChestSoundFileName => "ViolentlyOpeningChest";
        public override string dropSoundFileName => throw new NotImplementedException();
        public Chest(SoundsController soundsController, Item content) : base(soundsController)
        {
            this.soundsController = soundsController;
            this.content = content;
        }
        public override void PickUp(Character character)
        {
            
        }
        public override void Drop(Character character)
        {
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            throw new NotImplementedException();
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}