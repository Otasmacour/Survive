﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Bullets : Item
    {
        public int number;
        string reloadingFileName = "ReloadingBullets";
        public override string getItemName()
        {
            if (number > 0)
            {
                return "Bullets: " + number.ToString();
            }
            else { return "Bullet"; }
        }
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 1;
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => "FallingTool";
        public Bullets(SoundsController soundsController) : base(soundsController)
        {
            this.soundsController = soundsController;
            number = 3;
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
            Gun gun = character.inventory.items.OfType<Gun>().FirstOrDefault();
            if (gun == null) { alerts.Add("You need a gun to shoot"); return; }
            gun.numberOfBullets += number;
            soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(reloadingFileName)));
            character.inventory.items.Remove(this);
            character.inventory.currentlyHeldItem = null;
            character.inventory.InventoryUpdate();
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