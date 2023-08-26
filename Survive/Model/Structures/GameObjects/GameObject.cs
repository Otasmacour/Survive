using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class GameObject
    {
        public abstract char GetSymbol(Map map);
        public abstract int GetPriorityNumber();
    }
}
