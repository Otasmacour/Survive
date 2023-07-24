using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapLayout
    {
        public Dictionary<Direction, Coordinates> doorCoordinates = new Dictionary<Direction, Coordinates>();
        public Dictionary<Direction, Coordinates> transitionsCoordinates = new Dictionary<Direction, Coordinates>();
        public List<Coordinates> occupiedPlaces = new List<Coordinates>();
    }
}
