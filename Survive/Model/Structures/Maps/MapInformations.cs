using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapInformations
    {
        public Coordinates upperDoor;
        public Coordinates bottomDoor;
        public Coordinates leftDoor;
        public Coordinates rightDoor;
        public Coordinates upperTransition;
        public Coordinates bottomTransition;
        public Coordinates leftTransition;
        public Coordinates rightTransition;
        public List<Coordinates> occupiedPlaces = new List<Coordinates>();
    }
}
