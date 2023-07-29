using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class View
    {
        public void PrintMap((char[,] charMap, string mapTitle) tupple)
        {
            char[,] charMap = tupple.charMap;
            int mapHeight = charMap.GetLength(0);
            int mapWidth = charMap.GetLength(1);
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
                    char c = charMap[y, x];
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n"+tupple.mapTitle);
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

