using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapsFunctions
    {
        public MapHelper mapHelper;
        public MapOperations mapOperations;
        MapsInitialization mapsInitialization;
        public MapsFunctions(Characters characters, DataIOManager dataIOManager, RoomMapCollection roomMapCollection)
        {
            this.mapHelper = new MapHelper(characters, dataIOManager);
            this.mapOperations = new MapOperations(mapHelper, characters);
            this.mapsInitialization = new MapsInitialization(dataIOManager,roomMapCollection);
        }
    }
}