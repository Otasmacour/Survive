﻿using System;
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
            
            if (monsterInformations.onWay)
            {
                GeneratingPath(monster, monsterInformations, mapHelper);
                Direction directionToGo = WhichDirectionToGo(monsterInformations.path, monster, mapHelper, dataIOManager);
                if (directionToGo != Direction.Null)
                {
                    MonsterMovement(monster, movement, directionToGo);              
                }
                else
                {
                    if (monsterInformations.path.Count == 1)
                    {
                        monsterInformations.onWay = false; monsterInformations.Destination = null;
                    } 
                }
            }
        }
        static void GeneratingPath(Monster monster, MonsterInformations monsterInformations, MapHelper mapHelper)
        {
            monsterInformations.path = PathBetweenMaps(monster, monsterInformations.Destination, mapHelper).Item2;
        }
        public void whereTheMonsterShouldGoForAWalk(Map map)
        {
            monsterInformations.Destination = map;
            monsterInformations.onWay = true;
            GeneratingPath(monster, monsterInformations, mapHelper);
        }
        static void MonsterMovement(Monster monster, Movement movement, Direction directionToGo)
        {
            movement.MoveCharacter(monster, directionToGo);
        }
        static Direction WhichDirectionToGo(Queue<Map> path, Monster monster, MapHelper mapHelper, DataIOManager dataIOManager)
        {
            
            if (monster.mapWhereIsLocated != path.First())
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
            Dictionary<(int, int), int> depths = TwoDArrayBFS(currentMap.twoDArray, monster.coordinates, mapHelper);
            if (ISNextMapAccessible(nextMap, monster, mapHelper))
            {
                Queue<Coordinates> path = PathInTwoDArray(depths, currentMap.mapInformations.mapLayout.doorCoordinates[doorDirection], monster.coordinates, mapHelper);
                Direction direction = dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(monster.coordinates, path.First());
                return direction;
            }
            return Direction.Null;
        }
        static bool ISNextMapAccessible(Map nextMap, Monster monster, MapHelper mapHelper)
        {
            bool result = false;
            foreach(Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper))
            {
                if(door.destinationMap == nextMap)
                {
                    result = true;
                }
            }
            return result;
        }
        static Queue<Coordinates> PathInTwoDArray(Dictionary<(int, int), int> depths, Coordinates destination, Coordinates start, MapHelper mapHelper)
        {
            Console.WriteLine("here");
            Queue<Coordinates> path = new Queue<Coordinates>();
            Coordinates currentCoordinates =mapHelper.TuppleToCoordinates(mapHelper.CoordinatesToTupple(destination));
            path.Enqueue(currentCoordinates);
            Console.WriteLine("there");
            Console.WriteLine("destination: ["+destination.y.ToString()+"] [" +destination.x.ToString()+ "]");
            Console.WriteLine(depths[mapHelper.CoordinatesToTupple(destination)]);
            while (mapHelper.CoordinatesToTupple(currentCoordinates) != mapHelper.CoordinatesToTupple(start))
            {
                //Console.WriteLine("in loop");
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
        static Dictionary<(int ,int),int> TwoDArrayBFS(List<GameObject>[,] twoDArray, Coordinates start, MapHelper mapHelper)
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
        static (bool wayFound, Queue<Map>) PathBetweenMaps(Monster monster, Map DestinationMap, MapHelper mapHelper)
        {
            
            (bool wayFound, Dictionary<Map,int> depths) = MapBFS(monster, DestinationMap, mapHelper);
            if(wayFound == false)
            {
                return (wayFound, new Queue<Map>());
            }
            Queue<Map> path = new Queue<Map>();
            Map currentMap = DestinationMap;
            path.Enqueue(currentMap);
            while(currentMap != monster.mapWhereIsLocated)
            {
                foreach(Door door in currentMap.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;
                    
                    if (depths[adjacentMap] < depths[currentMap] && adjacentMap != monster.mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                        path.Enqueue(adjacentMap);
                    }
                    else if(adjacentMap == monster.mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                    }
                }
            }
            Queue<Map> reversed = new Queue<Map>(path.Reverse());
            return (wayFound, reversed);
        }
        static List<Door> DoorsThatCanBeReached(Map map, Coordinates sourceCoordinates, MapHelper mapHelper)
        {
            Dictionary<(int, int), int> depths = TwoDArrayBFS(map.twoDArray, sourceCoordinates, mapHelper);
            List <Door> doors = new List <Door>();

            string result = string.Empty;
            foreach (var item in map.mapInformations.mapLayout.doorCoordinates)
            {
                if(depths.ContainsKey(mapHelper.CoordinatesToTupple(item.Value)))
                {
                    result = " OK";
                    doors.Add(map.mapInformations.mapLayout.doors[item.Key]);
                }
                else
                {
                    result = " NOT";
                }
                //Console.WriteLine();
                //Console.Write(mapHelper.CoordinatesToTupple(item.Value));
                Console.Write(result);
            }
            return doors;
        }
        static (bool wayFound, Dictionary<Map, int> depths) MapBFS(Monster monster, Map Destination, MapHelper mapHelper) //Breadth First Search
        {
            bool wayFound = false;
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            //depths.Add(monster.mapWhereIsLocated, 0);
            //queue.Enqueue(monster.mapWhereIsLocated);
            //visited.Add(monster.mapWhereIsLocated);
            foreach(Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper))
            {
                depths.Add(door.destinationMap, 0);
                queue.Enqueue(door.destinationMap);
                visited.Add(door.destinationMap);
            }
            while (queue.Count > 0)
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
            if (depths.ContainsKey(Destination))
            {
                wayFound = true;
            }
            return (wayFound, depths);
        }
    }
}