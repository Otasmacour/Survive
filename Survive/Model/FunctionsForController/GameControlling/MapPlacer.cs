using Survive.Survive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapPlacer
    {
        GameInformations gameInformations;
        Maps maps;
        Characters characters;
        MapOperations mapOperations;
        ReturnFunctions returnFunctions;
        RoomMapCollection roomMapCollection;
        Random random = new Random();
        public MapPlacer(Maps maps, Characters characters, GameInformations gameInformations)
        {
            this.gameInformations = gameInformations;
            this.maps = maps;
            this.characters = characters;
            this.mapOperations = maps.mapsFunctions.mapOperations;
            this.returnFunctions = maps.mapsFunctions.mapHelper.returnFunctions;
            this.roomMapCollection = maps.roomMapCollection;
            Place();
        }
        void Place()
        {
            PlacePlayerOnMap(maps, characters);
            PlaceMonsterOnMap(maps, characters);
            PlaceItemsOnMaps(maps);
        }
        void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates playerCoordinates = new Coordinates();
            playerCoordinates.y = 1;
            playerCoordinates.x = 1;
            mapOperations.PlaceCharacterOnMap(characters.player, roomMapCollection.roomsByFloor[10][0], playerCoordinates);
        }
        void PlaceMonsterOnMap(Maps maps, Characters characters)
        {
            Coordinates monsterCoordinates = new Coordinates();
            monsterCoordinates.y = 4;
            monsterCoordinates.x = 3;
            mapOperations.PlaceCharacterOnMap(characters.monster, roomMapCollection.roomsByFloor[0][4], monsterCoordinates);
        }
        void PlaceItemsOnMaps(Maps maps)
        {
            List<Item> items = new List<Item>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(Item).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    ConstructorInfo constructor = type.GetConstructor(new[] { typeof(GameInformations) });
                    if (constructor != null)
                    {
                        Item item = (Item)constructor.Invoke(new object[] {gameInformations});
                        items.Add(item);
                    }
                }
            }
            foreach (Item item in items)
            {
                Map map = roomMapCollection.roomsByFloor[item.floorNumberWhereItemSpawns][random.Next(roomMapCollection.roomsByFloor[item.floorNumberWhereItemSpawns].Count)];
                Coordinates coordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(map, 1)[0];
                mapOperations.PlaceItemOnMap(item, map, coordinates);
            }
        }
    }
}
