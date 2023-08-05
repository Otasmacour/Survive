using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Monster : Character
    {
        public Monster()
        {
            this.symbol = 'M';
            this.name = "Monster";
        }
        public MonsterWalkingInformations monsterWalkingInformations = new MonsterWalkingInformations();
    }
}