using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapInformations
    {
        public MapLayout mapLayout = new MapLayout();
        public List<Character> charactersOnMap = new List<Character>();
        public List<Item> itemsOnMap = new List<Item>();
        public int floorNumber;
        public MapType mapType;
    }
}
