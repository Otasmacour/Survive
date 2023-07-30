using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapsLinking
    {
        DataIOManager dataIOManager;
        public MapsLinking(DataIOManager dataIOManager)
        {
            this.dataIOManager = dataIOManager;
        }
        public void LinkingMaps(RoomMapCollection roomMapCollection, MapsInitialization mapsInitialization)
        {
            int floorNumber;
            //Rooms -1 Floor
            floorNumber = -1;
            Map RMS1 = new Map(mapsInitialization, "Room", floorNumber, "Room 1", MapType.Null);
            Map RMS2 = new Map(mapsInitialization, "Room", floorNumber, "Room 2", MapType.Null);
            Map RMS3 = new Map(mapsInitialization, "Room", floorNumber, "Room 3", MapType.Null);
            //Rooms 0  Floor
            floorNumber = 0;
            Map RM01 = new Map(mapsInitialization, "Room", floorNumber, "Room 1", MapType.Null);
            Map RM02 = new Map(mapsInitialization, "Room", floorNumber, "Room 2", MapType.Null);
            Map RM03 = new Map(mapsInitialization, "Room", floorNumber, "Room 3", MapType.Null);
            Map RM04 = new Map(mapsInitialization, "Room", floorNumber, "Room 4", MapType.Null);
            Map RM05 = new Map(mapsInitialization, "Room", floorNumber, "Room 5", MapType.Null);
            Map RM06 = new Map(mapsInitialization, "Room", floorNumber, "Room 6", MapType.Null);
            //Rooms 1  Floor
            floorNumber = 1;
            Map RMF1 = new Map(mapsInitialization, "Room", floorNumber, "Room 1", MapType.Null);
            Map RMF2 = new Map(mapsInitialization, "Room", floorNumber, "Room 2", MapType.Null);
            Map RMF3 = new Map(mapsInitialization, "Room", floorNumber, "Room 3", MapType.Null);
            Map RMF4 = new Map(mapsInitialization, "Room", floorNumber, "Room 4", MapType.Null);
            Map RMF5 = new Map(mapsInitialization, "Room", floorNumber, "Room 5", MapType.Null);
            //Vertical Stairs
            Map VS1 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs", MapType.Stairs);
            Map VS2 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs", MapType.Stairs);
            Map VS3 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs", MapType.Stairs);
            //Linking maps together
            void LinkingSFloor()
            {
                MapLink(RMS1, RMS2, Direction.Right);
                MapLink(VS1, RMS1, Direction.Down);
                MapLink(RMS1, RMS3, Direction.Down);
                MapLink(RMS3, VS3, Direction.Down);
            }
            void Linking0Floor()
            {
                MapLink(RM01, RM02, Direction.Right);
                MapLink(RM02, RM03, Direction.Right);
                MapLink(RM04, RM05, Direction.Right);
                MapLink(RM01, VS1, Direction.Down);
                MapLink(RM02, VS2, Direction.Down);
                MapLink(RM03, RM04, Direction.Down);
                MapLink(RM04, RM06, Direction.Down);
            }
            void Linking1Floor()
            {
                MapLink(RMF1, RMF3, Direction.Right);
                MapLink(RMF4, RMF5, Direction.Right);
                MapLink(VS3, RMF1, Direction.Down);
                MapLink(RMF1, RMF2, Direction.Down);
                MapLink(RMF4, RMF3, Direction.Down);
                MapLink(VS2, RMF4, Direction.Down);
            }
            LinkingSFloor();
            Linking0Floor();
            Linking1Floor();
        }
        public void MapLink(Map sourceMap, Map destinationMap, Direction sourceDirection)
        {
            PerformMapLink(sourceMap, destinationMap, sourceDirection, dataIOManager.enumFunctions.GetOppositeDirection(sourceDirection));
        }
        static void PerformMapLink(Map sourceMap, Map destinationMap, Direction sourceDirection, Direction destinationDirection)
        {
            Door sourceDoor = (Door)sourceMap.twoDArray[sourceMap.mapInformations.mapLayout.doorCoordinates[sourceDirection].y, sourceMap.mapInformations.mapLayout.doorCoordinates[sourceDirection].x][0];
            sourceDoor.destinationMap = destinationMap;
            sourceDoor.transitionPointCoordinates = destinationMap.mapInformations.mapLayout.transitionsCoordinates[destinationDirection];
            Door destinationDoor = (Door)destinationMap.twoDArray[destinationMap.mapInformations.mapLayout.doorCoordinates[destinationDirection].y, destinationMap.mapInformations.mapLayout.doorCoordinates[destinationDirection].x][0];
            destinationDoor.destinationMap = sourceMap;
            destinationDoor.transitionPointCoordinates = sourceMap.mapInformations.mapLayout.transitionsCoordinates[sourceDirection];
        }
    }
}