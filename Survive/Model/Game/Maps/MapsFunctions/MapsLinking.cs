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
            Map RMS1 = new Map(mapsInitialization, "Room", floorNumber, "Room 1");
            Map RMS2 = new Map(mapsInitialization, "Room", floorNumber, "Room 2");
            Map RMS3 = new Map(mapsInitialization, "Room", floorNumber, "Room 3");
            //Rooms 0  Floor
            floorNumber = 0;
            Map RM01 = new Map(mapsInitialization, "Room", floorNumber, "Room 1");
            Map RM02 = new Map(mapsInitialization, "Room", floorNumber, "Room 2");
            Map RM03 = new Map(mapsInitialization, "Room", floorNumber, "Room 3");
            Map RM04 = new Map(mapsInitialization, "Room", floorNumber, "Room 4");
            Map RM05 = new Map(mapsInitialization, "Room", floorNumber, "Room 5");
            Map RM06 = new Map(mapsInitialization, "Room", floorNumber, "Room 6");
            //Rooms 1  Floor
            floorNumber = 1;
            Map RMF1 = new Map(mapsInitialization, "Room", floorNumber, "Room 1");
            Map RMF2 = new Map(mapsInitialization, "Room", floorNumber, "Room 2");
            Map RMF3 = new Map(mapsInitialization, "Room", floorNumber, "Room 3");
            Map RMF4 = new Map(mapsInitialization, "Room", floorNumber, "Room 4");
            Map RMF5 = new Map(mapsInitialization, "Room", floorNumber, "Room 5");
            //Vertical Stairs
            Map VS1 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs");
            Map VS2 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs");
            Map VS3 = new Map(mapsInitialization, "VerticalStairs", 10, "Stairs");


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