using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapsCleaning
    {
        public MapsCleaning() 
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
                //Physically from the TwoDArray of map
                List<GameObject> list;
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Up].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Up].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes (list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Down].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Down].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Left].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Left].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
                list = map.twoDArray[map.mapInformations.mapLayout.doorCoordinates[Direction.Right].y, map.mapInformations.mapLayout.doorCoordinates[Direction.Right].x];
                CheckDoorUsingAndPerformRemovingNotUsedOnes(list);
                //From the Dictionary by Direction
                List<(Direction,Door)> doorsToRemove = new List<(Direction,Door)>();
                foreach (var item in map.mapInformations.mapLayout.doors)
                {
                    if(item.Value.destinationMap == null)
                    {
                        doorsToRemove.Add((item.Key,item.Value));
                    }
                }
                foreach(var (key, value) in doorsToRemove) 
                {
                    map.mapInformations.mapLayout.doors.Remove(key);
                }
            }
        }
    }
}
