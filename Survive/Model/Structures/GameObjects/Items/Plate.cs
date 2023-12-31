﻿using Survive.Survive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Plate : Item
    {
        public override string getItemName()
        {
            return "Plate";
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 4;
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => "PlateDrop";
        public Plate(SoundsController soundsController) : base(soundsController) 
        {
            this.soundsController = soundsController;
        }
        public override void PickUp(Character character)
        {
            pickUp(character);
        }
        public override void Drop(Character character)
        {
            character.inventory.items.Remove(this);
            character.inventory.currentlyHeldItem = null;
            character.inventory.InventoryUpdate();
            BrokenPlate brokenPlate = new BrokenPlate(soundsController);
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Add(brokenPlate);
            soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(dropSoundFileName)));
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts) { }
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
