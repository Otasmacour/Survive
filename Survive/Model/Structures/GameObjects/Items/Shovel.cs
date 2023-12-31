﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        string shovelHittingChestSoundFileName = "ShovelHittingChest";
        List<string> diggingSounds = new List<string> { "DiggingWithShovel1", "DiggingWithShovel2", "DiggingWithShovel3", "DiggingWithShovel4", "DiggingWithShovel5" };
        public override string useSoundFileName => throw new NotImplementedException();
        public override string dropSoundFileName => "MetalItemDrop";
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
            if (character.mapWhereIsLocated.mapInformations.mapType == MapType.Garden)
            {
                BuriedChest buriedChest = character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].OfType<BuriedChest>().FirstOrDefault();
                if (buriedChest != null)
                {
                    buriedChest.visible = true;
                    soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(shovelHittingChestSoundFileName)));
                }
                else { soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(diggingSounds[random.Next(diggingSounds.Count)]))); }
            }
            else { alerts.Add("Digging with a shovel outside the garden, like really ?");  }
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
