﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class NullCharacter : Character
    {
        public override bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates) //this will never be used
        {
            return false;
        }
        public override void Die()
        {
            throw new NotImplementedException();
        }
        public override int GetPriorityNumber()
        {
            throw new NotImplementedException();
        }
    }
}