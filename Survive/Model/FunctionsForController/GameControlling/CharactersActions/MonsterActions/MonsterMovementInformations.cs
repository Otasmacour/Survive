using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterMovementInformations
    {
        public bool onWay;
        public Map Destination;
        public Queue<Map> path;
        public HashSet<Door> unreachableDoors;
        public void UponArrival()
        {
            this.onWay = false;
            this.Destination = null;
            this.unreachableDoors = new HashSet<Door>();
        }
    }
}