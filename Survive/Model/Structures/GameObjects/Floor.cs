using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Floor : GameObject
    {
        public override char GetSymbol(Map map)
        {
            return '.';
        }
        public override int GetPriorityNumber()
        {
            return 100;
        }
    }
}
