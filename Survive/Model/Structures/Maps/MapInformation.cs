﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapInformation
    {
        public MapLayout mapLayout = new MapLayout();
        public List<Character> charactersOnMap = new List<Character>();
        public List<Map> adjacentMaps = new List<Map>();
        public int floorNumber;
        public MapType mapType;
    }
}
