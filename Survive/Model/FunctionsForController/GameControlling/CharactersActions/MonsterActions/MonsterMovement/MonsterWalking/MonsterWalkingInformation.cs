using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterWalkingInformation
    {
        public bool followingTheNoise;
        public bool onWay;
        public Map Destination;
        public Queue<Map> path;
        public HashSet<Door> unreachableDoors = new HashSet<Door>();
        public void UponArrival()
        {
            this.onWay = false;
            this.Destination = null;
            this.unreachableDoors.Clear();
        }
    }
}