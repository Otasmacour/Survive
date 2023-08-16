using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterSearchingInformation
    {
        public int count;
        public bool searching;
        public bool searchingRooms;
        public bool searchingRoom;
        public Coordinates whereToSearch;
        public HashSet<Map> visitedRooms = new HashSet<Map>();
        public void EndingOfSearching()
        {
            this.count = 0;
            this.searching = false;
            this.searchingRooms = false;
            this.searchingRoom = false;
            this.whereToSearch = null;
            this.visitedRooms.Clear();
        }
    }
}