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
        }
        static void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates coordinates = new Coordinates();
            coordinates.y = 3;
            coordinates.x = 3;
            maps.mapsFunctions.mapOperations.PlaceCharacterOnMap(characters.player, maps.roomMapCollection.list[0], coordinates);
        }
    }
}
