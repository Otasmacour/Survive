using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Parsing
    {
        public (int, int) CoordinatesToTupple(Coordinates coordinates)
        {
            return (coordinates.y, coordinates.x);
        }
        public Coordinates TuppleToCoordinates((int y, int x) tupple)
        {
            Coordinates coordinates = new Coordinates();
            coordinates.y = tupple.y; coordinates.x = tupple.x;
            return coordinates;
        }
    }
}
