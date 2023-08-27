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
        SoundsController soundsController;
        Random random = new Random();
        public MapPlacer(Maps maps, Characters characters, GameInformations gameInformations, SoundsController soundsController)
        {
            this.gameInformations = gameInformations;
            this.maps = maps;
            this.characters = characters;
            this.mapOperations = maps.mapsFunctions.mapOperations;
            this.returnFunctions = maps.mapsFunctions.mapHelper.returnFunctions;
            this.roomMapCollection = maps.roomMapCollection;
            this.soundsController = soundsController;
            Place();
        }
        void Place()
        {
            PlacePlayerOnMap(maps, characters);
            PlaceMonsterOnMap(maps, characters);
            PlaceItemsOnMaps(maps);
            PlaceChests();
            //PlaceKeys();
        }
        void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates playerCoordinates = new Coordinates();
            playerCoordinates.y = 2;
            playerCoordinates.x = 2;
            mapOperations.PlaceCharacterOnMap(characters.player, roomMapCollection.roomsByFloor[0][3], playerCoordinates);
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
                    ConstructorInfo constructor = type.GetConstructor(new[] { typeof(SoundsController) });
                    if (constructor != null)
                    {
                        Item item = (Item)constructor.Invoke(new object[] {soundsController});
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
        void PlaceChests()
        {
            Queue<Item> contents = new Queue<Item>();
            contents.Enqueue(new Plate(soundsController));
            contents.Enqueue(new Plate(soundsController));
            contents.Enqueue(new Gun(soundsController));
            List<Chest> chests = new List<Chest>();
            WoodenChest woodenChest = new WoodenChest(soundsController, contents.Dequeue()); chests.Add(woodenChest);
            IronChest ironChest = new IronChest(soundsController, contents.Dequeue()); chests.Add(ironChest);
            WeaponChest weaponChest = new WeaponChest(soundsController, contents.Dequeue()); chests.Add(weaponChest);
            for(int i = 0; i < chests.Count; i++)
            {
                Chest chest = chests[i];
                Map map = roomMapCollection.roomsByFloor[chest.floorNumberWhereItemSpawns][random.Next(roomMapCollection.roomsByFloor[chest.floorNumberWhereItemSpawns].Count)];
                Coordinates coordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(map, 1)[0];
                mapOperations.PlaceItemOnMap(chest, map, coordinates);
            }
        }
        void PlaceKeys()
        {
            List<Key> keys = new List<Key>();
            WoodenKey woodenKey = new WoodenKey(soundsController); keys.Add(woodenKey);
            WeaponKey weaponKey = new WeaponKey(soundsController); keys.Add(weaponKey);
            foreach (Key key in keys)
            {
                Map map = roomMapCollection.roomsByFloor[key.floorNumberWhereItemSpawns][random.Next(roomMapCollection.roomsByFloor[key.floorNumberWhereItemSpawns].Count)];
                Coordinates coordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(map, 1)[0];
                mapOperations.PlaceItemOnMap(key, map, coordinates);
            }
        }
    }
}
