using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WoodenKey : Key
    {
        public override string itemName => "Wooden key";
        public WoodenKey(SoundsController soundController) : base(soundController) 
        {

        }
        public override char GetSymbol(Map map)
        {
            return 'W';
        }
    }
}
