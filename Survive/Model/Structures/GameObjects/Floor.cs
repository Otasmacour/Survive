using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Floor : GameObject
    {
        public override char symbol => '.';
        public override int GetPriorityNumber()
        {
            return 100;
        }
    }
}
