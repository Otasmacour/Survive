using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Wall : GameObject
    {
        public override char GetSymbol(Map map)
        {
            return 'x';
        }
        public override int GetPriorityNumber()
        {
            return 95;
        }
    }
}
