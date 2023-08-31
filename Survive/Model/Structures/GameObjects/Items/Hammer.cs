using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Hammer : Item
    {
        public override string getItemName()
        {
            return "Hammer";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 10;
        public override int floorNumberWhereItemSpawns => -1;
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => "FallingTool";
        public Hammer(SoundsController soundsController) : base(soundsController)
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
            Chest chest = character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].OfType<Chest>().FirstOrDefault();
            if (chest != null)
            {
                if(chest is Chain) { chest.Unlock(character.mapWhereIsLocated, character.coordinates, alerts, false); }
                else if(chest is WoodenChest) { chest.Unlock(character.mapWhereIsLocated, character.coordinates, alerts, true); }
                else { alerts.Add("You can't get into this with a hammer"); }
            }
            else { alerts.Add("There is nothing to hit"); }
        }
        public override char GetSymbol(Map map)
        {
            return 'H';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}