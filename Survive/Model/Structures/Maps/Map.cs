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
        public Map(MapsInitialization mapsInitialization, string fileName)
        {
            this.name = fileName;
            var tuple = mapsInitialization.CreatingTwoDArrayPlusInformationsOfIt(fileName);
            this.twoDArray = tuple.twoDArray;
            this.mapInformations = tuple.mapInformations;
            mapsInitialization.roomMapCollection.list.Add(this);
        }
    }
}
