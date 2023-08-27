﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Key : Item
    {
        public override bool takesUpSpaceInTheInventory => true;
        public override int noiseLevel => 2;
        public override int floorNumberWhereItemSpawns => 0;
        public override string dropSoundFileName => "FallingTool";
        public Key(SoundsController soundsController) : base(soundsController)
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
            if (mapHelper.boolFunctions.ChestThere(character.mapWhereIsLocated.twoDArray, character.coordinates) == false)
            {
                return;
            }
            Chest chest = mapHelper.returnFunctions.GetChestThere(character.mapWhereIsLocated, character.coordinates);
            if(this.GetType().Name == chest.typeOfAssociatedKey)
            {
                chest.Unlock(this, character.mapWhereIsLocated, character.coordinates, alerts);
            }
            else { alerts.Add("Wrong key"); }
        }
        public override int GetPriorityNumber()
        {
            return 80;
        }
    }
}