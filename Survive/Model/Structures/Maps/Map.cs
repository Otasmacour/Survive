using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Map
    {
        public List<GameObject>[,] twoDArray;
        public MapInformations mapInformations;
        public List<Character> charactersOnMap = new List<Character>();
        public string name;
        public void MapConstruktor(List<GameObject>[,] twoDArray, MapInformations mapInformations, RoomMapCollection roomMapCollection) 
        {
            this.twoDArray = twoDArray;
            this.mapInformations = mapInformations;
            roomMapCollection.list.Add(this);
        }
    }
}
