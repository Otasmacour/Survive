﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Player : Character
    {
        public Player(Character character)
        {
            RoleConstructor(character);
        }
    }
}
