using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Corpse : SuspiciousObject
    {
        public Corpse()
        {
            this.symbol = '?';
        }
        public override int GetPriorityNumber()
        {
            return 32;
        }
    }
}
