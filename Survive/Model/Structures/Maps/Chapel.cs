using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Chapel : Map
    {
        public Chapel(MapsInitialization mapsInitialization)
        {
            var tuple = mapsInitialization.CreatingTwoDArrayPlusInformationsOfIt("Chapel");
            MapConstruktor(tuple.twoDArray, tuple.mapInformations, mapsInitialization.roomMapCollection);
            this.name = "Chapel";
        }
    }
}
