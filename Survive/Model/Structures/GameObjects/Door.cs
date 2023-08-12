using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Door : GameObject
    {
        public Map destinationMap;
        public Coordinates transitionPointCoordinates;
        public Door()
        {
            this.symbol = 'd';
        }
        public void CheckForPossibleStairsAndChangeTheNameInCase(Map roomFromWhere, Door door, Direction sourceDirection)
        {
            if(this.destinationMap.mapInformations.mapType == MapType.Stairs)
            {
                StairsNameChangeAccordingToOrientatio(roomFromWhere, door, sourceDirection);
            }
        }
        static void StairsNameChangeAccordingToOrientatio(Map roomFromWhere, Door door, Direction sourceDirection)
        {
            Map stairs = door.destinationMap;
            Map roomToWhere = stairs.mapInformations.mapLayout.doors[sourceDirection].destinationMap; //If error is here, then the stairs are not connected to the other one room.
            if (roomFromWhere.mapInformations.floorNumber > roomToWhere.mapInformations.floorNumber)
            {
                stairs.name = "Stairs Down";
            }
            else if (roomFromWhere.mapInformations.floorNumber < roomToWhere.mapInformations.floorNumber)
            {
                stairs.name = "Stairs up";
            }
        }
        public override int GetPriorityNumber()
        {
            return 90;
        }
    }
}
