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
        public void Display(Map map, int monsterDistance)
        {
            PrintMap(map);
            PrintMonsterDistance(monsterDistance);
        }
        void PrintMap(Map map)
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
                    GameObject mostPreferredObject = mapHelper.returnFunctions.mostPreferredObjectInList(objects);
                    Console.Write(mostPreferredObject.symbol);
                }
                Console.WriteLine("");
            }
            string floorNumber = string.Empty;
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
    }
}

