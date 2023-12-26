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
        public override int noiseLevel => 7;
        public override string useSoundFileName => "ViolentlyOpeningChest";
        public override string dropSoundFileName => "MetalItemDrop";
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
            MainDoor mainDoor = mapHelper.returnFunctions.AdjacentItems(character.mapWhereIsLocated, character.coordinates).OfType<MainDoor>().FirstOrDefault();
            Chest chest = character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].OfType<Chest>().FirstOrDefault();
            if (chest != null)
            {
                if(chest is Chain) { chest.Unlock(character.mapWhereIsLocated, character.coordinates, alerts, false); }
                else if(chest is WoodenChest) { chest.Unlock(character.mapWhereIsLocated, character.coordinates, alerts, true); }
                else { alerts.Add("You can't get into this with a hammer"); }
            }
            else if(mainDoor != null && mainDoor.plankLock)
            {
                mainDoor.plankLock = false;
                soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(useSoundFileName)));
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