using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class FunctionsForInitialization
    {
        Maps maps;
        Characters characters;
        public FunctionsForInitialization(Maps maps, Characters characters)
        {
            this.maps = maps;
            this.characters = characters;
        }
        public void Initialization()
        {
            PlacePlayerOnMap(maps, characters);
            PlaceMonsterOnMap(maps, characters);
        }
        static void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates playerCoordinates = new Coordinates();
            playerCoordinates.y = 3;
            playerCoordinates.x = 5;
            maps.mapsFunctions.mapOperations.PlaceCharacterOnMap(characters.player, maps.roomMapCollection.roomsByFloor[1][0], playerCoordinates);
        }
        static void PlaceMonsterOnMap(Maps maps, Characters characters)
        {
            Coordinates monsterCoordinates = new Coordinates();
            monsterCoordinates.y = 3;
            monsterCoordinates.x = 3;
            maps.mapsFunctions.mapOperations.PlaceCharacterOnMap(characters.monster, maps.roomMapCollection.roomsByFloor[1][1], monsterCoordinates);
        }
    }
}
