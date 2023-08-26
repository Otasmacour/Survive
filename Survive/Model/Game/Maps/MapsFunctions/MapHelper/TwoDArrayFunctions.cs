using System;
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
                foreach (Coordinates adjacentCoordinates in returnFunctions.GetAdjacentCoordinates(twoDArray, coordinates, 4).Values)
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
        public (Queue<Coordinates> path, bool pathExist) PathInTwoDArray(Dictionary<(int, int), int> depths, List<GameObject>[,] twoDArray, Coordinates destination, Coordinates start)
        {
            if(depths.ContainsKey(parsing.CoordinatesToTupple(destination)) == false)//It is here for a case. When the monster searches a room, it needs to find its way to the furniture in that room, but thanks to the MonsterCanGoThere feature (which also needs to be there, if it wasn't, the monster would endlessly walk headlong into the furniture), the furniture never gets depth, so it needs to check if the destination is in depth, if not, it needs to add itself in like this.
            {
                depths.Add(parsing.CoordinatesToTupple(destination), 1000);

            }
            Queue<Coordinates> path = new Queue<Coordinates>();
            Coordinates currentCoordinates = parsing.TuppleToCoordinates(parsing.CoordinatesToTupple(destination));
            path.Enqueue(currentCoordinates);
            int number = 0;
            while (parsing.CoordinatesToTupple(currentCoordinates) != parsing.CoordinatesToTupple(start))
            {
                number ++;
                if(number > 100)
                {
                    return (path, false);
                }
                foreach (Coordinates adjacentCoordinates in returnFunctions.GetAdjacentCoordinates(twoDArray, currentCoordinates, 4).Values)
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
            return (reversed, true);
        }
        public Direction GetDirectionWhileWalkingOnTwoDArray(Coordinates destination, Coordinates start, List<GameObject>[,] twoDArray)
        {
            Dictionary<(int, int), int> depths = TwoDArrayBFS(twoDArray, start);
            var tuple = PathInTwoDArray(depths, twoDArray, destination, start);
            Queue<Coordinates> path = tuple.path;
            if(tuple.pathExist)
            {
                return dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(start, path.First());
            }
            else
            {
                return Direction.Null;
            }
        }
        public Dictionary<(int, int), int> TwoDArrayBFSForDistance(List<GameObject>[,] twoDArray, Coordinates start)
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
                foreach (Coordinates adjacentCoordinates in returnFunctions.GetAdjacentCoordinates(twoDArray, coordinates, 4).Values)
                {
                    if (visited.Contains(parsing.CoordinatesToTupple(adjacentCoordinates)) == false)
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
                    else if (visited.Contains(parsing.CoordinatesToTupple(adjacentCoordinates)))
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
        int DistanceOfTwoGameObjects(List<GameObject>[,] twoDArray, Coordinates start, Coordinates destination)
        {

            var depths = TwoDArrayBFSForDistance(twoDArray, start);
            int distance = depths[parsing.CoordinatesToTupple(destination)];
            return distance;
        }
        List<(Coordinates coordinatesStart, Coordinates coordinatesDestination, Map map)> GetPassageList(Monster monster, Player player, List<Map> mapsPath)
        {
            List<(Coordinates coordinatesStart, Coordinates coordinatesDestination, Map map)> passageList = new List<(Coordinates doorStart, Coordinates doorDestination, Map map)>();
            if(monster.mapWhereIsLocated == player.mapWhereIsLocated)
            {
                (Coordinates doorStart, Coordinates doorDestination, Map map) PlayerAndMonster = (monster.coordinates, player.coordinates, monster.mapWhereIsLocated);
                passageList.Add(PlayerAndMonster);
                return passageList;
            }
            //Passage through the first map
            Map firstMap = mapsPath[0];
            Coordinates coordinatesStart = monster.coordinates;
            Coordinates coordinatesDestination = new Coordinates();
            foreach (GameObject gmDoor in firstMap.mapInformations.mapLayout.totalDoor)
            {
                if (gmDoor is Door)
                {
                    Door door = (Door)gmDoor;
                    if (door.destinationMap == mapsPath[1])
                    {
                        coordinatesDestination = firstMap.mapInformations.mapLayout.doorCoordinatesByDoor[door];
                    }
                }
                else if (gmDoor is SecretDoor)
                {
                    SecretDoor secretDoor = (SecretDoor)gmDoor;
                    if (secretDoor.destinationMap == mapsPath[1])
                    {
                        coordinatesDestination = firstMap.mapInformations.mapLayout.secretDoorsCoordinates[secretDoor];
                    }
                }
            }
            (Coordinates doorStart, Coordinates doorDestination, Map map) firstPassage = (coordinatesStart, coordinatesDestination, firstMap);
            passageList.Add(firstPassage);
            //Passage through the last map
            Map lastMap = mapsPath[mapsPath.Count - 1];
            Coordinates coordinatesStart2 = monster.coordinates;
            Coordinates coordinatesDestination2 = player.coordinates;
            foreach (GameObject gmDoor in lastMap.mapInformations.mapLayout.totalDoor)
            {
                if (gmDoor is Door)
                {
                    Door door = (Door)gmDoor;
                    if (door.destinationMap == mapsPath[mapsPath.Count - 2])
                    {
                        coordinatesStart2 = lastMap.mapInformations.mapLayout.doorCoordinatesByDoor[door];
                    }
                }
                else if (gmDoor is SecretDoor)
                {
                    SecretDoor secretDoor = (SecretDoor)gmDoor;
                    if (secretDoor.destinationMap == mapsPath[mapsPath.Count - 2])
                    {
                        coordinatesStart2 = lastMap.mapInformations.mapLayout.secretDoorsCoordinates[secretDoor];
                    }
                }
            }
            (Coordinates doorStart, Coordinates doorDestination, Map map) lastPassage = (coordinatesStart2, coordinatesDestination2, lastMap);
            passageList.Add(lastPassage);
            if (mapsPath.Count < 2)
            {
                return passageList;
            }
            //Distances the rest of the path
            for (int i = 1; i < mapsPath.Count - 1; i++)
            {
                Map beforeMap = mapsPath[i - 1];
                Map nowMap = mapsPath[i];
                Map nextMap = mapsPath[i + 1];
                Coordinates coordinatesDoorStart = new Coordinates();
                Coordinates coordinatesDoorDestination = new Coordinates();
                foreach (GameObject gmDoor in nowMap.mapInformations.mapLayout.totalDoor)
                {
                    if (gmDoor is Door)
                    {
                        Door door = (Door)gmDoor;
                        if (door.destinationMap == beforeMap)
                        {
                            coordinatesDoorStart = nowMap.mapInformations.mapLayout.doorCoordinatesByDoor[door];
                        }
                        else if (door.destinationMap == nextMap)
                        {
                            coordinatesDoorDestination = nowMap.mapInformations.mapLayout.doorCoordinatesByDoor[door];
                        }
                    }
                    else if (gmDoor is SecretDoor)
                    {
                        SecretDoor secretDoor = (SecretDoor)gmDoor;
                        if (secretDoor.destinationMap == beforeMap)
                        {
                            coordinatesDoorStart = nowMap.mapInformations.mapLayout.secretDoorsCoordinates[secretDoor];
                        }
                        else if (secretDoor.destinationMap == nextMap)
                        {
                            coordinatesDoorDestination = nowMap.mapInformations.mapLayout.secretDoorsCoordinates[secretDoor];
                        }

                    }
                }
                (Coordinates doorStart, Coordinates doorDestination, Map map) passage = (coordinatesDoorStart, coordinatesDoorDestination, nowMap);
                passageList.Add(passage);
            }
            return passageList;
        }
        public int DistanceOfMonster(Monster monster, Player player)
        {
            //Maps BFS
            Map start = player.mapWhereIsLocated;
            Map destination = monster.mapWhereIsLocated;
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            depths.Add(start, 0);
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count > 0)
            {
                Map map = queue.Dequeue();
                foreach (Door door in map.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;
                    if (visited.Contains(adjacentMap) == false)
                    {
                        depths.Add(adjacentMap, depths[map] + 1);
                        queue.Enqueue(adjacentMap);
                        visited.Add(adjacentMap);
                    }
                    else
                    {
                        if (depths[adjacentMap] > depths[map] + 1)
                        {
                            depths[adjacentMap] = depths[map] + 1;
                            queue.Enqueue(adjacentMap);
                        }
                    }
                }
                foreach (SecretDoor secretDoor in map.mapInformations.mapLayout.secretDoors)
                {
                    Map adjacentMap = secretDoor.destinationMap;
                    if (visited.Contains(adjacentMap) == false)
                    {
                        depths.Add(adjacentMap, depths[map] + 1);
                        queue.Enqueue(adjacentMap);
                        visited.Add(adjacentMap);
                    }
                    else
                    {
                        if (depths[adjacentMap] > depths[map] + 1)
                        {
                            depths[adjacentMap] = depths[map] + 1;
                            queue.Enqueue(adjacentMap);
                        }
                    }
                }
            }
            //Searching for a path between maps
            List<GameObject> doorsPath = new List<GameObject>();
            List<Map> mapsPath = new List<Map>();
            mapsPath.Add(monster.mapWhereIsLocated);
            Map currentMap = destination;
            while (currentMap != start)
            {
                foreach (Door door in currentMap.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;
                    if (depths[adjacentMap] < depths[currentMap])
                    {
                        currentMap = adjacentMap;
                        mapsPath.Add(adjacentMap);
                    }
                }
                foreach (SecretDoor secretDoor in currentMap.mapInformations.mapLayout.secretDoors)
                {
                    Map adjacentMap = secretDoor.destinationMap;
                    if (depths[adjacentMap] < depths[currentMap])
                    {
                        currentMap = adjacentMap;
                        mapsPath.Add(adjacentMap);
                    }
                }
            }
            List<(Coordinates coordinatesStart, Coordinates coordinatesDestination, Map map)> passageList = GetPassageList(monster, player, mapsPath);
            int totalDistance = 0;
            foreach (var passage in passageList)
            {
                totalDistance += DistanceOfTwoGameObjects(passage.map.twoDArray, passage.coordinatesStart, passage.coordinatesDestination);
            }
            return totalDistance;
        }
    }
}