﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterChasingInformations
    {
        public bool chasing;
        public Coordinates playerPosition;
        public Coordinates whereThePlayerHasGone;
        public void EndingOfChasing()
        {
            this.chasing = false;
        }
    }
}
