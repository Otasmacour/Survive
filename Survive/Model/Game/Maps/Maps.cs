using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Maps
    {
        public RoomMapCollection roomMapCollection;
        public MapsFunctions mapsFunctions;
        public Maps(Characters characters, DataIOManager dataIOManager)
        {
            this.roomMapCollection = new RoomMapCollection();
            this.mapsFunctions = new MapsFunctions(characters, dataIOManager, this.roomMapCollection);
        }
    }
}
