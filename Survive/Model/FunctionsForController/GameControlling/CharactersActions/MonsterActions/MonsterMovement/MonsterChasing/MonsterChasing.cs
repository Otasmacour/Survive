using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterChasing
    {
        Movement movement;
        Monster monster;
        MapHelper mapHelper;
        public MonsterChasing(Movement movement, Monster monster, MapHelper mapHelper)
        {
            this.movement = movement;
            this.monster = monster;
            this.mapHelper = mapHelper;
        }
        public void Chase()
        {

        }
    }
}
