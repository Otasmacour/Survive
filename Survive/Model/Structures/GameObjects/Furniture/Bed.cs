using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Bed : Furniture
    {
        public Bed()
        {
            this.symbol = 'b';
            this.canHideThere = true;
        }
    }
}