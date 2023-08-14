using Survive.Survive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapPlacer
    {
        Maps maps;
        Characters characters;
        MapOperations mapOperations;
        ReturnFunctions returnFunctions;
        RoomMapCollection roomMapCollection;
        public MapPlacer(Maps maps, Characters characters)
        {
            this.maps = maps;
            this.characters = characters;
            this.mapOperations = maps.mapsFunctions.mapOperations;
            this.returnFunctions = maps.mapsFunctions.mapHelper.returnFunctions;
            this.roomMapCollection = maps.roomMapCollection;
        }
        public void Place()
        {
            PlacePlayerOnMap(maps, characters);
            PlaceMonsterOnMap(maps, characters);
            PlaceItemsOnMaps(maps);
        }
        void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates playerCoordinates = new Coordinates();
            playerCoordinates.y = 1;
            playerCoordinates.x = 7;
            mapOperations.PlaceCharacterOnMap(characters.player, roomMapCollection.roomsByFloor[1][0], playerCoordinates);
        }
        void PlaceMonsterOnMap(Maps maps, Characters characters)
        {
            Coordinates monsterCoordinates = new Coordinates();
            monsterCoordinates.y = 3;
            monsterCoordinates.x = 3;
            mapOperations.PlaceCharacterOnMap(characters.monster, roomMapCollection.roomsByFloor[1][0], monsterCoordinates);
        }
        void PlaceItemsOnMaps(Maps maps)
        {
            Coordinates plateCoordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(roomMapCollection.roomsByFloor[1][0], 1)[0];
            mapOperations.PlaceItemOnMap(new Plate(), roomMapCollection.roomsByFloor[1][0], plateCoordinates);
            Coordinates brokenPlateCoordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(roomMapCollection.roomsByFloor[1][0], 1)[0];
            mapOperations.PlaceItemOnMap(new BrokenPlate(), roomMapCollection.roomsByFloor[1][0], brokenPlateCoordinates);

        }
    }
}
