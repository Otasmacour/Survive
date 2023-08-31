using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Shovel : Item
    {
        Random random = new Random();
        public override string getItemName()
        {
            return "Shovel";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 1;
        public override int floorNumberWhereItemSpawns => 1;
        List<string> diggingSounds = new List<string> { "DiggingWithShovel1", "DiggingWithShovel2", "DiggingWithShovel3", "DiggingWithShovel4", "DiggingWithShovel5" };
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => "FallingTool";
        public Shovel(SoundsController soundsController) : base(soundsController)
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
            soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(diggingSounds[random.Next(diggingSounds.Count)])));
            BuriedChest buriedChest = character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].OfType<BuriedChest>().FirstOrDefault();
            if(buriedChest != null)
            {
                buriedChest.visible = true;
            }
        }
        public override char GetSymbol(Map map)
        {
            return 'S';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
