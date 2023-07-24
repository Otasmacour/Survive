﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Door : GameObject
    {
        public Map destinationMap;
        public Coordinates transitionPointCoordinates;
        public Door()
        {
            this.symbol = 'd';
        }
    }
}
