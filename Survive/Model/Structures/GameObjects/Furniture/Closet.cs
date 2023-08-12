using System;
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
            this.symbol = 'c';
            this.canHideThere = true;
        }
        public override int GetPriorityNumber()
        {
            return 20;
        }
    }
}