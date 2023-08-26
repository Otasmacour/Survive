﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Closet : Furniture
    {
        public Closet()
        {
            this.canHideThere = true;
        }
        public override char GetSymbol(Map map)
        {
            return 'c';
        }
        public override int GetPriorityNumber()
        {
            return 20;
        }
    }
}