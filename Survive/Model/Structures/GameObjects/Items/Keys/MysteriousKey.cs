using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MysteriousKey : Key
    {
        public override string getItemName()
        {
            return "Mysterious Key";
        }
        public MysteriousKey(SoundsController soundController) : base(soundController)
        {

        }
        public override char GetSymbol(Map map)
        {
            return 'M';
        }
    }
}
