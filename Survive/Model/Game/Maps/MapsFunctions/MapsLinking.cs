using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapsLinking
    {
        public void LinkingMaps(RoomMapCollection roomMapCollection, MapsInitialization mapsInitialization)
        {
            //draw a plan
            //Do it in general
            roomMapCollection.lobby = new Lobby(mapsInitialization);
            roomMapCollection.verticalCorridor = new VerticalCorridor(mapsInitialization);
            roomMapCollection.chapel = new Chapel(mapsInitialization);
            Random random = new Random();
            MapLink(roomMapCollection.chapel, roomMapCollection.lobby, Direction.Down, Direction.Up);
            MapLink(roomMapCollection.lobby, roomMapCollection.verticalCorridor, Direction.Down, Direction.Up);
            Lobby lobby = new Lobby(mapsInitialization);
            MapLink(lobby, roomMapCollection.verticalCorridor, Direction.Right, Direction.Left);
            
        }
        static void MapLink(Map sourceMap, Map destinationMap, Direction sourceDirection, Direction destinationDirection)
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
