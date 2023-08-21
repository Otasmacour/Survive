using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapLayout
    {
        public List<SecretDoor> secretDoors = new List<SecretDoor>();
        public Dictionary<SecretDoor, Coordinates> secretDoorsCoordinates = new Dictionary<SecretDoor, Coordinates>();
        public Dictionary<SecretDoor, Coordinates> secretTransitionsCoordinates = new Dictionary<SecretDoor, Coordinates>();
        public Dictionary<Direction, Coordinates> doorCoordinates = new Dictionary<Direction, Coordinates>();
        public Dictionary<Direction, Door> doors = new Dictionary<Direction, Door>();
        public Dictionary<Direction, Coordinates> transitionsCoordinates = new Dictionary<Direction, Coordinates>();
        public List<Coordinates> occupiedPlaces = new List<Coordinates>();
        public List<Coordinates> furnitureCoordinates =new List<Coordinates>();
    }
}