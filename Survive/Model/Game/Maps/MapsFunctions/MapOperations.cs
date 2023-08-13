using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapOperations
    {
        MapHelper mapHelper;
        Characters characters;

        public MapOperations(MapHelper mapHelper, Characters characters)
        {
            this.mapHelper = mapHelper;
            this.characters = characters;
        }
        public void CharacterRelocation(Character character, Map mapFromWhere, Map mapToWhere, Coordinates previousCoordinates, Coordinates newCoordinates)
        {
            mapFromWhere.twoDArray[previousCoordinates.y, previousCoordinates.x].Remove(character);
            mapToWhere.twoDArray[newCoordinates.y, newCoordinates.x].Add(character);
            character.mapWhereIsLocated = mapToWhere;
            mapFromWhere.mapInformations.charactersOnMap.Remove(character);
            mapToWhere.mapInformations.charactersOnMap.Add(character);
            character.coordinates = newCoordinates;
        }
        public void PlaceCharacterOnMap(Character character, Map map, Coordinates coordinates)
        {
            map.twoDArray[coordinates.y, coordinates.x].Add(character);
            character.mapWhereIsLocated = map;
            character.mapWhereIsLocated.mapInformations.charactersOnMap.Add(character);
            character.coordinates = coordinates;
        }
        public void PlaceItemOnMap(Item item, Map map, Coordinates coordinates)
        {
            map.twoDArray[coordinates.y, coordinates.x].Add(item);
            map.mapInformations.itemsOnMap.Add(item);
        }
    }
}