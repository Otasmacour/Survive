using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WeaponKey : Key
    {
        public override string getItemName()
        {
            return "Weapon Key";
        }
        public WeaponKey(SoundsController soundController) : base(soundController)
        {

        }
        public override char GetSymbol(Map map)
        {
            return 'W';
        }
    }
}
