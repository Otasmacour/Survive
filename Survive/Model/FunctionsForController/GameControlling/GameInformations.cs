using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class GameInformations
    {
        MapHelper mapHelper;
        public GameInformations(MapHelper mapHelper)
        {
            this.mapHelper = mapHelper;
        }
        public int GetMonsterDistance()
        {
            return 10;
        }
    }
}
