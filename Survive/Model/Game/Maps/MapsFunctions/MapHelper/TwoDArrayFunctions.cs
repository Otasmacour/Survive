using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class TwoDArrayFunctions
    {
        public Queue<Coordinates> PathInTwoDArray(Dictionary<(int, int), int> depths, Coordinates destination, Coordinates start, MapHelper mapHelper)
        {
            Queue<Coordinates> path = new Queue<Coordinates>();
            Coordinates currentCoordinates = mapHelper.TuppleToCoordinates(mapHelper.CoordinatesToTupple(destination));
            path.Enqueue(currentCoordinates);
            while (mapHelper.CoordinatesToTupple(currentCoordinates) != mapHelper.CoordinatesToTupple(start))
            {
                foreach (Coordinates adjacentCoordinates in mapHelper.AdjacentCoordinates(currentCoordinates).Values)
                {
                    if (depths.ContainsKey(mapHelper.CoordinatesToTupple(adjacentCoordinates))) //adjacent Coordinates could be in place of wall, or something that obviously cannot be giwen depth and so cannot be placed in that Dictionary
                    {

                        if (depths[mapHelper.CoordinatesToTupple(adjacentCoordinates)] < depths[mapHelper.CoordinatesToTupple(currentCoordinates)] && mapHelper.CoordinatesToTupple(adjacentCoordinates) != mapHelper.CoordinatesToTupple(start))
                        {
                            currentCoordinates = adjacentCoordinates;
                            path.Enqueue(adjacentCoordinates);
                        }
                        else if (mapHelper.CoordinatesToTupple(adjacentCoordinates) == mapHelper.CoordinatesToTupple(start))
                        {
                            currentCoordinates = adjacentCoordinates;
                        }
                    }
                }
            }
            Queue<Coordinates> reversed = new Queue<Coordinates>(path.Reverse());
            return reversed;
        }
        public Dictionary<(int, int), int> TwoDArrayBFS(List<GameObject>[,] twoDArray, Coordinates start, MapHelper mapHelper)
        {
            Dictionary<(int, int), int> depths = new Dictionary<(int, int), int>();
            Queue<(int y, int x)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            depths.Add(mapHelper.CoordinatesToTupple(start), 0);
            queue.Enqueue(mapHelper.CoordinatesToTupple(start));
            visited.Add(mapHelper.CoordinatesToTupple(start));
            while (queue.Count > 0)
            {
                Coordinates coordinates = mapHelper.TuppleToCoordinates(queue.Dequeue());
                foreach (Coordinates adjacentCoordinates in mapHelper.AdjacentCoordinates(coordinates).Values)
                {
                    if (visited.Contains(mapHelper.CoordinatesToTupple(adjacentCoordinates)) == false && mapHelper.JustFloorThere(twoDArray, adjacentCoordinates))
                    {
                        depths.Add(mapHelper.CoordinatesToTupple(adjacentCoordinates), depths[mapHelper.CoordinatesToTupple(coordinates)] + 1);
                        queue.Enqueue(mapHelper.CoordinatesToTupple(adjacentCoordinates));
                        visited.Add(mapHelper.CoordinatesToTupple(adjacentCoordinates));
                    }
                    else if (visited.Contains(mapHelper.CoordinatesToTupple(adjacentCoordinates)) == false && mapHelper.DoorThere(twoDArray, adjacentCoordinates))
                    {
                        depths.Add(mapHelper.CoordinatesToTupple(adjacentCoordinates), depths[mapHelper.CoordinatesToTupple(coordinates)] + 1);
                        visited.Add(mapHelper.CoordinatesToTupple(adjacentCoordinates));
                    }
                    else if (visited.Contains(mapHelper.CoordinatesToTupple(adjacentCoordinates)) && mapHelper.JustFloorThere(twoDArray, adjacentCoordinates))
                    {
                        if (depths[mapHelper.CoordinatesToTupple(adjacentCoordinates)] > depths[mapHelper.CoordinatesToTupple(coordinates)] + 1)
                        {
                            depths[mapHelper.CoordinatesToTupple(adjacentCoordinates)] = depths[mapHelper.CoordinatesToTupple(coordinates)] + 1;
                            queue.Enqueue(mapHelper.CoordinatesToTupple(adjacentCoordinates));
                        }
                    }
                }
            }
            return depths;
        }
    }
}
