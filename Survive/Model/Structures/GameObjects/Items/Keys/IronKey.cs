using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class IronKey : Key
    {
        public override string getItemName()
        {
            return "Iron Key";
        }
        public IronKey(SoundsController soundController) : base(soundController)
        {

        }
        public override char GetSymbol(Map map)
        {
            return 'I';
        }
    }
}
