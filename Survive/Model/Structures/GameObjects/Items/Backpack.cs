﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BackPack : Item
    {
        public override string itemName => "Backpack";
        public override bool takesUpSpaceInTheInventory => false;
        public override int noiseLevel => 3;
        public override string soundFileName => "Backpack";
        public BackPack(GameInformations gameInformations) : base(gameInformations)
        {
            this.gameInformations = gameInformations;
        }
        public override Sound GetSound()
        {
            return new Sound(soundFileName, noiseLevel);
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
        public override void Use(Character character)
        {
            
        }
        public override char symbol => 'B';
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}