using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Wall : GameObject
    {
        public Wall() 
        {
            this.symbol = 'x';
        }
        public override int GetPriorityNumber()
        {
            return 95;
        }
    }
}
