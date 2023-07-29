using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapCleaning
    {
        public MapCleaning() 
        {
        }
        static void CheckDoorUsingAndPerformRemovingNotUsedOnes(List<GameObject> list)
        {
            Door door = (Door)list[0];
            if(door.destinationMap == null)
            {
                list.Remove(door);
                list.Add(new Wall());
            }
        }
        public void RemovingOfUnusedDoors(RoomMapCollection roomMapCollection)
        {
            foreach(Map map in roomMapCollection.list)
            {
                List<GameObject> list;
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Up].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Up].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes (list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Down].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Down].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Left].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Left].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Right].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Right].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
            }
        }
    }
}
