using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class ViewConsole
    {
        MapHelper mapHelper;
        public string LastDisplay { get; set; } = string.Empty;

        public ViewConsole(MapHelper mapHelper)
        {
            this.mapHelper = mapHelper;
        }

        public void Display(Map playersMap, int monsterDistance, Inventory playersInventory, List<Item> itemsWithinPlayersReach, List<string> alerts, Monster monster)
        {
            string displayString = GetDisplayString(playersMap, monsterDistance, playersInventory, itemsWithinPlayersReach, alerts, monster);
            Console.Clear();
            Console.Write(displayString);
            LastDisplay = displayString;
        }

        public string GetDisplayString(Map playersMap, int monsterDistance, Inventory playersInventory, List<Item> itemsWithinPlayersReach, List<string> alerts, Monster monster)
        {
            StringBuilder display = new StringBuilder();
            display.AppendLine(GetMapString(playersMap, itemsWithinPlayersReach));
            if (monster.living) { display.AppendLine(GetMonsterDistanceString(monsterDistance)); }
            display.AppendLine(GetPlayersInventoryString(playersInventory));
            display.AppendLine(GetAlertsString(alerts));
            return display.ToString();
        }

        private string GetMapString(Map map, List<Item> itemsWithinPlayersReach)
        {
            StringBuilder mapDisplay = new StringBuilder();
            List<GameObject>[,] twoDArrayt = map.twoDArray;
            int mapHeight = twoDArrayt.GetLength(0);
            int mapWidth = twoDArrayt.GetLength(1);

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    List<GameObject> objects = twoDArrayt[y, x];
                    GameObject mostPreferredObject = mapHelper.returnFunctions.GetMostPreferredObjectInList(objects);
                    mapDisplay.Append(mostPreferredObject.GetSymbol(map));
                }
                mapDisplay.AppendLine();
            }

            mapDisplay.Append(GetItemsInPlayersReachString(itemsWithinPlayersReach, map));
            if (map.mapInformations.mapType != MapType.Stairs)
            {
                mapDisplay.AppendLine($"Floor {map.mapInformations.floorNumber}, {map.name}");
            }
            else
            {
                mapDisplay.AppendLine(map.name);
            }

            return mapDisplay.ToString();
        }

        private string GetMonsterDistanceString(int distance)
        {
            string roomOrrooms = distance > 1 ? "rooms" : "room";
            return $"The monster is {distance} {roomOrrooms} away";
        }

        private string GetPlayersInventoryString(Inventory playersInventory)
        {
            StringBuilder inventoryDisplay = new StringBuilder();
            if (playersInventory.currentlyHeldItem != null)
            {
                inventoryDisplay.AppendLine("Now you are holding: " + playersInventory.currentlyHeldItem.getItemName());
            }
            inventoryDisplay.AppendLine("In inventory:");
            List<int> IndexesOfItemsToPrint = new List<int>();
            int numberOfItemsThatAreAfterTheCurrentlyHeldItem = playersInventory.items.Count - 1 - playersInventory.items.IndexOf(playersInventory.currentlyHeldItem);
            int numberOfItemsThatAreBeforeTheCurrentlyHeldItem = playersInventory.items.Count - numberOfItemsThatAreAfterTheCurrentlyHeldItem - 1;
            for (int i = 1; i < numberOfItemsThatAreAfterTheCurrentlyHeldItem + 1; i++) IndexesOfItemsToPrint.Add(playersInventory.items.IndexOf(playersInventory.currentlyHeldItem) + i);
            for (int i = 0; i < numberOfItemsThatAreBeforeTheCurrentlyHeldItem; i++) IndexesOfItemsToPrint.Add(i);

            foreach (int index in IndexesOfItemsToPrint) inventoryDisplay.AppendLine(" - " + playersInventory.items[index].getItemName());
            for (int i = 0; i < playersInventory.inventorySize - 1 - IndexesOfItemsToPrint.Count; i++) { inventoryDisplay.AppendLine(" - "); }

            return inventoryDisplay.ToString();
        }

        private string GetAlertsString(List<string> alerts)
        {
            StringBuilder alertsDisplay = new StringBuilder();
            foreach (string alert in alerts)
            {
                alertsDisplay.AppendLine(alert);
            }
            return alertsDisplay.ToString();
        }

        private string GetItemsInPlayersReachString(List<Item> itemsWithinPlayersReach, Map map)
        {
            StringBuilder itemsDisplay = new StringBuilder();
            MainDoor mainDoor = itemsWithinPlayersReach.OfType<MainDoor>().FirstOrDefault();
            if (mainDoor != null)
            {
                if (mainDoor.possiblePassage) { itemsDisplay.AppendLine("m - main door"); }
                else
                {
                    itemsDisplay.AppendLine("_______");
                    itemsDisplay.AppendLine("m - main door");
                    if (mainDoor.plankLock) { itemsDisplay.AppendLine("Plank lock, maybe a hammer would be helpful..."); }
                    if (mainDoor.padlock) { itemsDisplay.AppendLine("Padlock"); }
                    if (mainDoor.codePadlock) { itemsDisplay.AppendLine("Code padlock"); }
                    itemsDisplay.AppendLine("-------");
                }
                itemsWithinPlayersReach.Remove(mainDoor);
            }
            foreach (Item item in itemsWithinPlayersReach)
            {
                itemsDisplay.AppendLine(item.GetSymbol(map) + " - " + item.getItemName());
            }

            return itemsDisplay.ToString();
        }
    }

}