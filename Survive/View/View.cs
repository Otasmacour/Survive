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
        public void PrintMap(Map map)
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
                    GameObject mostPreferredObject = mapHelper.mostPreferredObjectInList(objects);
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
        public void PrintCharactersOnMap(List<(char, string)> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line.Item1 + " - " + line.Item2);
            }
        }
        static void PrintTestsStuff(List<String> list)
        {
            Console.WriteLine();
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
            list.Clear();
        }
    }
}

