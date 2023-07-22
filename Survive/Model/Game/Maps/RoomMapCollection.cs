using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class RoomMapCollection
    {
        public List<Map> list = new List<Map>();
        public Lobby lobby;
        public VerticalCorridor verticalCorridor;
        //public HorizontalCorridor horizontalCorridor; // finish this
        public Chapel chapel;
        public RoomMapCollection() 
        {

        }
    }
}
