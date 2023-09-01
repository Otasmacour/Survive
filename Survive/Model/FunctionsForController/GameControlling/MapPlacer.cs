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
            PlaceMonsterOnMap();
            roomMapCollection.roomsByFloor[0][1].twoDArray[0, 6][0] = new MainDoor(soundsController);
            PlaceItemsOnMaps();
            PlaceChestsPlusitemsThatAppearInChests();
        }
        void PlacePlayerOnMap(Maps maps, Characters characters)
        {
            Coordinates playerCoordinates = new Coordinates();
            playerCoordinates.y = 2;
            playerCoordinates.x = 2;
            mapOperations.PlaceCharacterOnMap(characters.player, roomMapCollection.roomsByFloor[0][5], playerCoordinates);
        }
        void PlaceMonsterOnMap()
        {
            Coordinates monsterCoordinates = new Coordinates();
            monsterCoordinates.y = 4;
            monsterCoordinates.x = 3;
            mapOperations.PlaceCharacterOnMap(characters.monster, roomMapCollection.roomsByFloor[-1][1], monsterCoordinates);
        }
        void PlaceItemsOnMaps()
        {
            List<Item> items = new List<Item> { new Hammer(soundsController), new MysteriousKey(soundsController), new WeaponKey(soundsController), new IronKey(soundsController), new WoodenKey(soundsController), new BackPack(soundsController), new Plate(soundsController) };
            foreach (Item item in items)
            {
                int floorNumber = random.Next(-1,2); int roomNumber = random.Next(roomMapCollection.roomsByFloor[floorNumber].Count);
                Map map = roomMapCollection.roomsByFloor[floorNumber][roomNumber];
                Coordinates coordinates = returnFunctions.GetRandomAvailableCoordinatesonMap(map, 1)[0];
                mapOperations.PlaceItemOnMap(item, map, coordinates);
            }
        }
        void PlaceChestsPlusitemsThatAppearInChests()
        {
            List<Item> itemsThatAppearInChests = new List<Item> { new PadlockCode(soundsController), new MasterKey(soundsController), new Bullets(soundsController)};
            int itemCount = itemsThatAppearInChests.Count;
            while (itemCount > 1)
            {
                int index = random.Next(itemCount);
                itemCount--;
                Item temp = itemsThatAppearInChests[index];
                itemsThatAppearInChests[index] = itemsThatAppearInChests[itemCount];
                itemsThatAppearInChests[itemCount] = temp;
            }
            Queue<Item> contents = new Queue<Item>(itemsThatAppearInChests);
            List<Chest> chests = new List<Chest>();
            Chain chain = new Chain(soundsController, new Shovel(soundsController)); chests.Add(chain);
            WeaponChest weaponChest = new WeaponChest(soundsController, new Gun(soundsController)); chests.Add(weaponChest);
            BuriedChest buriedChest = new BuriedChest(soundsController, contents.Dequeue()); chests.Add(buriedChest);
            WoodenChest woodenChest = new WoodenChest(soundsController, contents.Dequeue()); chests.Add(woodenChest);
            IronChest ironChest = new IronChest(soundsController, contents.Dequeue()); chests.Add(ironChest);
            foreach(Chest chest in chests)
            {
                Map map = roomMapCollection.roomsByFloor[chest.locationMap.floorNumber][chest.locationMap.roomNumber];
                Coordinates coordinates = new Coordinates(); coordinates.y = chest.locationCoordinates.y; coordinates.x = chest.locationCoordinates.x;
                mapOperations.PlaceItemOnMap(chest, map, coordinates);
            }
        }
    }
}
