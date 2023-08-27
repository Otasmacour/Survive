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
        public void Unlock(Key key, Map map, Coordinates coordinates, Alerts alerts)
        {
            map.twoDArray[coordinates.y, coordinates.x].Remove(this);
            map.twoDArray[coordinates.y, coordinates.x].Add(content);
        }
        public abstract string typeOfAssociatedKey {  get; }
        public override bool takesUpSpaceInTheInventory => false;
        public override int noiseLevel => throw new NotImplementedException();
        public override int floorNumberWhereItemSpawns => 0;
        public override string dropSoundFileName => throw new NotImplementedException();
        public Chest(SoundsController soundsController, Item content) : base(soundsController)
        {
            this.soundsController = soundsController;
            this.content = new Plate(soundsController);
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