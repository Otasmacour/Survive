using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterActions
    {
        Characters characters;
        Movement movement;
        DataIOManager dataIOManager;
        MonsterInformations monsterInformations = new MonsterInformations();
        RoomMapCollection roomMapCollection;
        MapHelper mapHelper;
        Monster monster;
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager, RoomMapCollection roomMapCollection, MapHelper mapHelper)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapHelper = mapHelper;
            this.monster = characters.monster;
        }
        public void Action()
        {
            if(monsterInformations.onWay)
            {
                ReGeneratingPath(monsterInformations, monster.mapWhereIsLocated);
                Direction directionToGo = WhichDirectionToGo(monsterInformations.path, monster, mapHelper, dataIOManager);
                if(directionToGo != Direction.Null)
                {
                    MonsterMovement(monster, movement, directionToGo);
                }
                else
                {
                    monsterInformations.onWay = false; monsterInformations.path = null;
                }
            }
        }
        static void ReGeneratingPath(MonsterInformations monsterInformations, Map mapWhereMonsterIsLocated)
        {
            monsterInformations.path = PathBetweenMaps(mapWhereMonsterIsLocated, monsterInformations.Destination);
        }
        public void whereTheMonsterShouldGoForAWalk(Map map)
        {
            monsterInformations.Destination = map;
            monsterInformations.onWay = true;
            monsterInformations.path = PathBetweenMaps(monster.mapWhereIsLocated, map);
        }
        static void MonsterMovement(Monster monster, Movement movement, Direction directionToGo)
        {
            movement.MoveCharacter(monster, directionToGo);
        }
        static Direction WhichDirectionToGo(Queue<Map> path, Monster monster, MapHelper mapHelper, DataIOManager dataIOManager)
        {
            if(monster.mapWhereIsLocated != path.First())
            {
                //Goes to the next map
                return DirectionToGo(monster.mapWhereIsLocated, path.First(), monster, mapHelper, dataIOManager);
            }
            else
            {
                if(path.Count == 1)
                {
                    //it arrived
                    return Direction.Null;
                }
                else
                {
                    path.Dequeue();
                    //Goes to the next map
                    return DirectionToGo(monster.mapWhereIsLocated, path.First(), monster, mapHelper, dataIOManager);
                }
            }
        }
        static Direction DirectionToGo(Map currentMap, Map nextMap, Monster monster, MapHelper mapHelper, DataIOManager dataIOManager)
        {
            Door door;
            Direction doorDirection = new Direction();
            foreach(var item in currentMap.mapInformations.mapLayout.doors)
            {
                if(item.Value.destinationMap == nextMap)
                {
                    door = item.Value;
                    doorDirection = item.Key;
                }
            }
            Dictionary<(int, int), int> depths = TwoDArrayBFS(currentMap.twoDArray, monster.coordinates, currentMap.mapInformations.mapLayout.doorCoordinates[doorDirection], mapHelper);
            Queue<Coordinates> path = PathInTwoDArray(depths, currentMap.mapInformations.mapLayout.doorCoordinates[doorDirection], monster.coordinates, mapHelper);
            Direction direction = dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(monster.coordinates, path.First());
            return direction;
        }
        static Queue<Coordinates> PathInTwoDArray(Dictionary<(int, int), int> depths, Coordinates destination, Coordinates start, MapHelper mapHelper)
        {
            Queue<Coordinates> path = new Queue<Coordinates>();
            Coordinates currentCoordinates =mapHelper.TuppleToCoordinates(mapHelper.CoordinatesToTupple(destination));
            path.Enqueue(currentCoordinates);
            while (mapHelper.CoordinatesToTupple(currentCoordinates) != mapHelper.CoordinatesToTupple(start))
            {
                foreach (Coordinates adjacentCoordinates in mapHelper.AdjacentCoordinates(currentCoordinates).Values)
                {
                    if(depths.ContainsKey(mapHelper.CoordinatesToTupple(adjacentCoordinates))) //adjacent Coordinates could be in place of wall, or something that obviously cannot be giwen depth and so cannot be placed in that Dictionary
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
        static Dictionary<(int ,int),int> TwoDArrayBFS(List<GameObject>[,] twoDArray, Coordinates start, Coordinates destination, MapHelper mapHelper)
        {
            Dictionary<(int,int), int> depths = new Dictionary<(int,int), int>();
            Queue<(int y, int x)> queue = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            depths.Add(mapHelper.CoordinatesToTupple(start), 0);
            queue.Enqueue(mapHelper.CoordinatesToTupple(start));
            visited.Add(mapHelper.CoordinatesToTupple(start));
            while(queue.Count > 0)
            {
                Coordinates coordinates = mapHelper.TuppleToCoordinates(queue.Dequeue());
                foreach(Coordinates adjacentCoordinates in mapHelper.AdjacentCoordinates(coordinates).Values)
                {
                    if (visited.Contains(mapHelper.CoordinatesToTupple(adjacentCoordinates)) == false && mapHelper.JustFloorThere(twoDArray,adjacentCoordinates))
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
                    else if(visited.Contains(mapHelper.CoordinatesToTupple(adjacentCoordinates)) && mapHelper.JustFloorThere(twoDArray, adjacentCoordinates))
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
        static Queue<Map> PathBetweenMaps(Map mapWhereIsLocated, Map DestinationMap)
        {
            Dictionary<Map,int> depths = MapBFS(mapWhereIsLocated, DestinationMap);
            Queue<Map> path = new Queue<Map>();
            Map currentMap = DestinationMap;
            path.Enqueue(currentMap);
            while(currentMap != mapWhereIsLocated)
            {
                foreach(Door door in currentMap.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;
                    
                    if (depths[adjacentMap] < depths[currentMap] && adjacentMap != mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                        path.Enqueue(adjacentMap);
                    }
                    else if(adjacentMap == mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                    }
                }
            }
            Queue<Map> reversed = new Queue<Map>(path.Reverse());
            return reversed;
        }
        static Dictionary<Map, int> MapBFS(Map Start, Map Destination) //Breadth First Search
        {
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            depths.Add(Start, 0);
            queue.Enqueue(Start);
            visited.Add(Start);           
            while(queue.Count > 0)
            {
                Map map = queue.Dequeue();
                foreach(Door door in map.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;
                    if(visited.Contains(adjacentMap) == false)
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
            return depths;
        }
    }
}