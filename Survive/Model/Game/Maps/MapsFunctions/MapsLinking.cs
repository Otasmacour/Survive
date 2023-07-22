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
            LinkingUpToDown(roomMapCollection.chapel, roomMapCollection.lobby);
            LinkingUpToDown(roomMapCollection.lobby, roomMapCollection.verticalCorridor);
            Random random = new Random();
        }
        static void LinkingUpToDown(Map upperMap, Map bottomMap)
        {
            Door bottomDoor = (Door) upperMap.twoDArray[upperMap.mapInformations.bottomDoor.y, upperMap.mapInformations.bottomDoor.x][0];
            bottomDoor.map = bottomMap;
            bottomDoor.transitionPointCoordinates = bottomMap.mapInformations.upperTransition;
            Door upperDoor = (Door)bottomMap.twoDArray[bottomMap.mapInformations.upperDoor.y, bottomMap.mapInformations.upperDoor.x][0];
            upperDoor.map = upperMap;
            upperDoor.transitionPointCoordinates = upperMap.mapInformations.bottomTransition;
        }
    }
}
