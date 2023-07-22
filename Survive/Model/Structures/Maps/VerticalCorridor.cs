using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class VerticalCorridor : Map
    {
        public VerticalCorridor(MapsInitialization mapsInitialization)
        {
            var tuple = mapsInitialization.CreatingTwoDArrayPlusInformationsOfIt("VerticalCorridor");
            MapConstruktor(tuple.twoDArray, tuple.mapInformations, mapsInitialization.roomMapCollection);
            this.name = "VerticalCorridor";
        }
    }
}