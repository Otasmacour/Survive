using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class RoomMapCollection
    {
        public List<Map> list = new List<Map>();
        public Dictionary<int,List<Map>> roomsByFloor = new Dictionary<int, List<Map>>();
        public Map monsterRespawnMap;
    }
}
