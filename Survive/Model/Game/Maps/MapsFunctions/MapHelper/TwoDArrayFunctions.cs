﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class TwoDArrayFunctions
    {
        Parsing parsing;
        ReturnFunctions returnFunctions;
        BoolFunctions boolFunctions;
        DataIOManager dataIOManager;
        public TwoDArrayFunctions(Parsing parsing, ReturnFunctions returnFunctions, BoolFunctions boolFunctions, DataIOManager dataIOManager)
        {
            this.parsing = parsing;
            this.returnFunctions = returnFunctions;
            this.boolFunctions = boolFunctions;
            this.dataIOManager = dataIOManager;
        }
        public Dictionary<(int, int), int> TwoDArrayBFS(List<GameObject>[,] twoDArray, Coordinates start)
        {
            Dictionary<(int, int), int> depths = new Dictionary<(int, int), int>();
            Queue<(int y, int x)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            depths.Add(parsing.CoordinatesToTupple(start), 0);
            queue.Enqueue(parsing.CoordinatesToTupple(start));
            visited.Add(parsing.CoordinatesToTupple(start));
            while (queue.Count > 0)
            {
                Coordinates coordinates = parsing.TuppleToCoordinates(queue.Dequeue());
                foreach (Coordinates adjacentCoordinates in returnFunctions.AdjacentCoordinates(twoDArray, coordinates).Values)
                {
                    if (visited.Contains(parsing.CoordinatesToTupple(adjacentCoordinates)) == false && boolFunctions.MonsterCanGoThere(twoDArray, adjacentCoordinates))
                    {
                        depths.Add(parsing.CoordinatesToTupple(adjacentCoordinates), depths[parsing.CoordinatesToTupple(coordinates)] + 1);
                        queue.Enqueue(parsing.CoordinatesToTupple(adjacentCoordinates));
                        visited.Add(parsing.CoordinatesToTupple(adjacentCoordinates));
                    }
                    else if (visited.Contains(parsing.CoordinatesToTupple(adjacentCoordinates)) == false && boolFunctions.DoorThere(twoDArray, adjacentCoordinates))
                    {
                        depths.Add(parsing.CoordinatesToTupple(adjacentCoordinates), depths[parsing.CoordinatesToTupple(coordinates)] + 1);
                        visited.Add(parsing.CoordinatesToTupple(adjacentCoordinates));
                    }
                    else if (visited.Contains(parsing.CoordinatesToTupple(adjacentCoordinates)) && boolFunctions.MonsterCanGoThere(twoDArray, adjacentCoordinates))
                    {
                        if (depths[parsing.CoordinatesToTupple(adjacentCoordinates)] > depths[parsing.CoordinatesToTupple(coordinates)] + 1)
                        {
                            depths[parsing.CoordinatesToTupple(adjacentCoordinates)] = depths[parsing.CoordinatesToTupple(coordinates)] + 1;
                            queue.Enqueue(parsing.CoordinatesToTupple(adjacentCoordinates));
                        }
                    }
                }
            }
            return depths;
        }
        public Queue<Coordinates> PathInTwoDArray(Dictionary<(int, int), int> depths, List<GameObject>[,] twoDArray, Coordinates destination, Coordinates start)
        {
            Queue<Coordinates> path = new Queue<Coordinates>();
            Coordinates currentCoordinates = parsing.TuppleToCoordinates(parsing.CoordinatesToTupple(destination));
            path.Enqueue(currentCoordinates);
            while (parsing.CoordinatesToTupple(currentCoordinates) != parsing.CoordinatesToTupple(start))
            {
                foreach (Coordinates adjacentCoordinates in returnFunctions.AdjacentCoordinates(twoDArray, currentCoordinates).Values)
                {
                    if (depths.ContainsKey(parsing.CoordinatesToTupple(adjacentCoordinates))) //adjacent Coordinates could be in place of wall, or something that obviously cannot be giwen depth and so cannot be placed in that Dictionary
                    {

                        if (depths[parsing.CoordinatesToTupple(adjacentCoordinates)] < depths[parsing.CoordinatesToTupple(currentCoordinates)] && parsing.CoordinatesToTupple(adjacentCoordinates) != parsing.CoordinatesToTupple(start))
                        {
                            currentCoordinates = adjacentCoordinates;
                            path.Enqueue(adjacentCoordinates);
                        }
                        else if (parsing.CoordinatesToTupple(adjacentCoordinates) == parsing.CoordinatesToTupple(start))
                        {
                            currentCoordinates = adjacentCoordinates;
                        }
                    }
                }
            }
            Queue<Coordinates> reversed = new Queue<Coordinates>(path.Reverse());
            return reversed;
        }
        public Direction GetDirectionWhileWalkingOnTwoDArray(Coordinates destination, Coordinates start, List<GameObject>[,] twoDArray)
        {
            Dictionary<(int, int), int> depths = TwoDArrayBFS(twoDArray, start);
            Queue<Coordinates> path = PathInTwoDArray(depths, twoDArray, destination, start);
            Direction direction = dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(start, path.First());
            return direction;
        }
    }
}
