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
            this.canHideThere = true;
        }
        public override char symbol => 'b';
        public override int GetPriorityNumber()
        {
             return 20;
        }
    }
}