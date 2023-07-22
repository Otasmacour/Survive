using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class View
    {
        public void Display(char[,] charMap, string mapTitle, List<(char, string)> lines, List<string> list)
        {
            PrintCharactersOnMap(lines);
            PrintMap((charMap, mapTitle));
            PrintTestsStuff(list);
        }
        public void PrintMap((char[,] charMap, string mapTitle) tupple)
        {
            char[,] charMap = tupple.charMap;
            int mapHeight = charMap.GetLength(0);
            int mapWidth = charMap.GetLength(1);
            Console.WriteLine("");
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
            for (int i = 0; i < mapHeight * mapWidth; i++)
            {
                if (i % mapWidth == 0)
                {
                    Console.WriteLine("");
                    Console.Write(i / mapWidth);
                }
                int height = (i) / mapWidth;
                int width = i - ((i / mapWidth) * mapWidth);
                char symbol = charMap[height, width];
                Console.Write(symbol);
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

