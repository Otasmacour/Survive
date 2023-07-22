using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Lobby : Map
    {
        public Lobby(MapsInitialization mapsInitialization)
        {
            var tuple = mapsInitialization.CreatingTwoDArrayPlusInformationsOfIt("Lobby");
            MapConstruktor(tuple.twoDArray, tuple.mapInformations, mapsInitialization.roomMapCollection);
            this.name = "Lobby";
        }
    }
}
