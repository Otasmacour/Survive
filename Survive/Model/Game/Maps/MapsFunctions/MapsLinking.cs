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
            //draw a plan
            //Do it in general
            //MapLink(roomMapCollection.chapel, roomMapCollection.lobby, Direction.Down, Direction.Up);
            //MapLink(roomMapCollection.lobby, roomMapCollection.verticalCorridor, Direction.Down, Direction.Up);
            Map lobby = new Map(mapsInitialization, "Lobby");
            Map room1 = new Map(mapsInitialization, "Room");
            MapLink(lobby, room1, Direction.Right);

        }
        public void MapLink(Map sourceMap, Map destinationMap, Direction sourceDirection)
        {
            PerformMapLink(sourceMap, destinationMap, sourceDirection, dataIOManager.enumFunctions.OppositeDirection(sourceDirection));
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
