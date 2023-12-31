﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WoodenChest : Chest
    {
        public WoodenChest(SoundsController soundsController, Item content) : base(soundsController, content)
        {
            this.soundsController = soundsController;
        }
        public override string typeOfAssociatedKey => "WoodenKey";
        public override (int floorNumber, int roomNumber) locationMap => (0, 0);
        public override (int y, int x) locationCoordinates => (4, 1);
        public override string getItemName()
        {
            return "Wooden chest";
        }
        public override char GetSymbol(Map map)
        {
            return 'w';
        }
    }
}
