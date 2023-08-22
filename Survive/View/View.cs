using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class View
    {
        MapHelper mapHelper;
        public View(MapHelper mapHelper)
        {
            this.mapHelper = mapHelper;
        }
        public void Display(Map map, int monsterDistance, Inventory playersInventory, List<Item> itemsWithinPlayersReach)
        {
            PrintMap(map, itemsWithinPlayersReach);
            PrintMonsterDistance(monsterDistance);
            PrintPlayersInventory(playersInventory);
        }
        static void PrintItemsInPlayersReach(List<Item> itemsWithinPlayersReach)
        {
            foreach(Item item in itemsWithinPlayersReach)
            {
                Console.WriteLine(item.symbol.ToString() + " - " + item.itemName);
            }
        }
        void PrintMap(Map map, List<Item> itemsWithinPlayersReach)
        {
            List<GameObject>[,] twoDArrayt = map.twoDArray;
            int mapHeight = twoDArrayt.GetLength(0);
            int mapWidth = twoDArrayt.GetLength(1);
            for (int i = 0; i < mapWidth; i++)
            {
                if (i == 0)
                {
                    Console.Write(' ');
                }
                Console.Write(i);
                if (i == mapWidth + 1)
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            for (int y = 0; y < mapHeight; y++)
            {
                Console.Write(y);
                for (int x = 0; x < mapWidth; x++)
                {
                    List<GameObject> objects = twoDArrayt[y, x];
                    GameObject mostPreferredObject = mapHelper.returnFunctions.GetMostPreferredObjectInList(objects);
                    Console.Write(mostPreferredObject.symbol);
                }
                Console.WriteLine("");
            }
            string floorNumber = string.Empty;
            PrintItemsInPlayersReach(itemsWithinPlayersReach);
            if(map.mapInformations.mapType != MapType.Stairs)
            {
                floorNumber = "Floor "+map.mapInformations.floorNumber.ToString()+", ";
            }
            Console.WriteLine("\n"+floorNumber+map.name);
        }
        void PrintMonsterDistance(int distance)
        {
            string roomOrrooms = String.Empty;
            if (distance > 1) { roomOrrooms = "rooms"; }
            else { roomOrrooms = "room"; }
            Console.WriteLine("The monster is " + distance.ToString() + " "+roomOrrooms+" away");
        }
        void PrintPlayersInventory(Inventory playersInventory)
        {
            if(playersInventory.currentlyHeldItem != null) { Console.WriteLine("Now you are holding: " + playersInventory.currentlyHeldItem.itemName); }
            Console.WriteLine("In inventory:");
            //Preparing indexes of items in the inventory for printing, according to how they should stand in a row, to make it intuitive for the player 
            List<int> IndexesOfItemsToPrint = new List<int>();
            int numberOfItemsThatAreAfterTheCurrentlyHeldItem = playersInventory.items.Count - 1 - playersInventory.items.IndexOf(playersInventory.currentlyHeldItem);
            int numberOfItemsThatAreBeforeTheCurrentlyHeldItem = playersInventory.items.Count - numberOfItemsThatAreAfterTheCurrentlyHeldItem - 1;
            for (int i = 1; i < numberOfItemsThatAreAfterTheCurrentlyHeldItem+1; i++) IndexesOfItemsToPrint.Add(playersInventory.items.IndexOf(playersInventory.currentlyHeldItem) + i);
            for (int i = 0; i < numberOfItemsThatAreBeforeTheCurrentlyHeldItem; i++) IndexesOfItemsToPrint.Add(i);
            //The explanation is above
            foreach (int index in IndexesOfItemsToPrint) Console.WriteLine(" - " + playersInventory.items[index].itemName);//prints items in inventory except currentlyHeldItem
            for (int i = 0; i < playersInventory.inventorySize - 1 - IndexesOfItemsToPrint.Count; i++) { Console.WriteLine(" - "); }//shows the empty space in inventory
        }
    }
}