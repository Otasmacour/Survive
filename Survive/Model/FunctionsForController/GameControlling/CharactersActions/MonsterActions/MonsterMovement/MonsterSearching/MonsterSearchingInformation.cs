using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterSearchingInformation
    {
        public bool searching;
        public bool firstRoomThenRooms;
        public bool searchingRooms;
        public int count;
        public Coordinates whereToSearch;
        public HashSet<Map> visitedRooms = new HashSet<Map>();
        public bool searchingRoom;
        public Queue<Coordinates> furnitureToSearch = new Queue<Coordinates>();
        public Coordinates CurrentFurnitureToSearch;

        public void EndingOfSearching()
        {
            this.searching = false;
            this.firstRoomThenRooms = false;
            this.searchingRooms = false;
            this.count = 0;
            this.whereToSearch = null;
            this.visitedRooms.Clear();
            this.searchingRoom = false;
            this.furnitureToSearch.Clear();
            this.CurrentFurnitureToSearch = null;
        }
        public void EndOfSearchingRoomStartToSearchingRooms()
        {
            this.searchingRoom = false;
            this.furnitureToSearch.Clear();
            this.CurrentFurnitureToSearch = null;
            this.searchingRooms = true;
        }
    }
}