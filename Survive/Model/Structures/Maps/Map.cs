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
        public MapInformation mapInformations;
        public string name;
        public Map(MapsInitialization mapsInitialization, string fileName, int floorNumber, string mapName, MapType mapType) //the mapName will be remove, after all rooms will have their names, it just temporary, bcs without names like "R1" (room 1), the mapLinking functions would be confusing 
        {
            this.name = mapName;
            var tuple = mapsInitialization.CreatingTwoDArrayPlusInformationsOfIt(fileName);
            this.twoDArray = tuple.twoDArray;
            this.mapInformations = tuple.mapInformations;
            this.mapInformations.floorNumber = floorNumber;
            this.mapInformations.mapType = mapType;
            if(mapType != MapType.Abnormal) { mapsInitialization.roomMapCollection.list.Add(this); } 
            if (mapsInitialization.roomMapCollection.roomsByFloor.ContainsKey(floorNumber))
            {
                mapsInitialization.roomMapCollection.roomsByFloor[floorNumber].Add(this);
            }
            else
            {
                mapsInitialization.roomMapCollection.roomsByFloor.Add(floorNumber, new List<Map> { this });
            }
        }
    }
}
