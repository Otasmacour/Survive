﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BackPack : Item
    {
        public override string getItemName()
        {
            return "Backpack";
        }
        public override bool takesUpSpaceInTheInventory => false;
        public override int noiseLevel => 3;
        public override int floorNumberWhereItemSpawns => 1;
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => throw new NotImplementedException();
        public BackPack(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
        }
        public override void PickUp(Character character)
        {
            pickUp(character);
            character.inventory.inventorySize += 3;
        }
        public override void Drop(Character character)
        {
            drop(character);
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            
        }
        public override char GetSymbol(Map map)
        {
            return 'B';
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}
