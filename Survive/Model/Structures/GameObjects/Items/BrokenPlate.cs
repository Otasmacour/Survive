﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    namespace Survive
    {
        class BrokenPlate : Item
        {
            public override string getItemName()
            {
                return "Broken plate";
            }
            public override bool takesUpSpaceInTheInventory => true;
            public override int noiseLevel => 3;
            public override string useSoundFileName => throw new NotImplementedException();
            public override string dropSoundFileName => "PlateDrop";
            public BrokenPlate(SoundsController soundsController) : base(soundsController)
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
}
