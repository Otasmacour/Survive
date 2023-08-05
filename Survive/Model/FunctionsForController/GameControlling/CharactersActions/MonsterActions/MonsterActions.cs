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
        MonsterMovementInformations monsterMovementInformations = new MonsterMovementInformations();
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
            if (monsterMovementInformations.onWay)
            {
                Movement(monster, monsterMovementInformations, mapHelper, dataIOManager, movement);
            }
        }
        static void Movement(Monster monster, MonsterMovementInformations monsterMovementInformations, MapHelper mapHelper, DataIOManager dataIOManager, Movement movement)
        {
            Direction directionToGo = WhichDirectionToGo(monsterMovementInformations.path, monster, mapHelper, dataIOManager, monsterMovementInformations);
            if (directionToGo != Direction.Null)
            {
                MonsterMovement(monster, movement, directionToGo);
            }
            else
            {
                if (monsterMovementInformations.path.Count == 1)
                {
                    //it arrived
                    monsterMovementInformations.UponArrival();
                }
                else
                {
                    //the next door cannot be reached
                    GeneratingPath(monster, monsterMovementInformations, mapHelper);
                }
            }
        }
        static void GeneratingPath(Monster monster, MonsterMovementInformations monsterMovementInformations, MapHelper mapHelper)
        {
            monsterMovementInformations.path = PathBetweenMaps(monster, monsterMovementInformations.Destination, mapHelper, monsterMovementInformations).Item2;
        }
        public void whereTheMonsterShouldGoForAWalk(Map map)
        {
            monsterMovementInformations.onWay = true;
            monsterMovementInformations.Destination = map;
            monsterMovementInformations.unreachableDoors = new HashSet<Door>();
            GeneratingPath(monster, monsterMovementInformations, mapHelper);
        }
        static void MonsterMovement(Monster monster, Movement movement, Direction directionToGo)
        {
            movement.MoveCharacter(monster, directionToGo);
        }
        static Direction WhichDirectionToGo(Queue<Map> path, Monster monster, MapHelper mapHelper, DataIOManager dataIOManager, MonsterMovementInformations monsterMovementInformations)
        {
            
            if (monster.mapWhereIsLocated != path.First())
            {
                //Goes to the next map
                return DirectionToGo(monster.mapWhereIsLocated, path.First(), monster, mapHelper, dataIOManager, monsterMovementInformations);
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
                    return DirectionToGo(monster.mapWhereIsLocated, path.First(), monster, mapHelper, dataIOManager, monsterMovementInformations);
                }
            }   
        }
        static Direction DirectionToGo(Map currentMap, Map nextMap, Monster monster, MapHelper mapHelper, DataIOManager dataIOManager, MonsterMovementInformations monsterMovementInformations)
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
            if (ISNextMapAccessible(nextMap, monster, mapHelper, monsterMovementInformations))
            {
                Queue<Coordinates> path = PathInTwoDArray(depths, currentMap.mapInformations.mapLayout.doorCoordinates[doorDirection], monster.coordinates, mapHelper);
                Direction direction = dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(monster.coordinates, path.First());
                return direction;
            }
            return Direction.Null;
        }
        static bool ISNextMapAccessible(Map nextMap, Monster monster, MapHelper mapHelper, MonsterMovementInformations monsterMovementInformations)
        {
            bool result = false;
            foreach(Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper, monsterMovementInformations))
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
        static (bool wayFound, Queue<Map>) PathBetweenMaps(Monster monster, Map DestinationMap, MapHelper mapHelper, MonsterMovementInformations monsterMovementInformations)
        {
            
            (bool wayFound, Dictionary<Map,int> depths) = MapBFS(monster, DestinationMap, mapHelper, monsterMovementInformations);
            if(wayFound == false)
            {
                Console.WriteLine("This will never happen?");
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
        static List<Door> DoorsThatCanBeReached(Map map, Coordinates sourceCoordinates, MapHelper mapHelper, MonsterMovementInformations monsterMovementInformations)
        {
            Dictionary<(int, int), int> depths = TwoDArrayBFS(map.twoDArray, sourceCoordinates, mapHelper);
            List <Door> doors = new List <Door>();
            foreach (var item in map.mapInformations.mapLayout.doorCoordinates)
            {
                if(depths.ContainsKey(mapHelper.CoordinatesToTupple(item.Value)))
                {
                    //the door can be reached
                    doors.Add(map.mapInformations.mapLayout.doors[item.Key]);
                }
                else
                {
                    //the door cannot be reached
                    monsterMovementInformations.unreachableDoors.Add(map.mapInformations.mapLayout.doors[item.Key]);
                }
            }
            return doors;
        }
        static (bool wayFound, Dictionary<Map, int> depths) MapBFS(Monster monster, Map Destination, MapHelper mapHelper, MonsterMovementInformations monsterMovementInformations)
        {
            bool wayFound = false;
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            foreach(Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper, monsterMovementInformations))//adding adjacent maps (of destination map), that can be reached
            {
                Console.WriteLine(door);
                depths.Add(door.destinationMap, 0);
                queue.Enqueue(door.destinationMap);
                visited.Add(door.destinationMap);
            }
            while (queue.Count > 0)
            {
                Map map = queue.Dequeue();
                foreach(Door door in map.mapInformations.mapLayout.doors.Values)
                {
                    if(monsterMovementInformations.unreachableDoors == null)
                    {
                        Console.WriteLine("What?");
                    }
                    if(monsterMovementInformations.unreachableDoors.Contains(door) == false) //there could be a doors, that monster already found unreachable and it knows about them 
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