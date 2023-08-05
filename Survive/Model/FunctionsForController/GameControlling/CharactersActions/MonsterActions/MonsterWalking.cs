using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterWalking
    {
        Movement movement;
        Monster monster;
        MapHelper mapHelper;
        public MonsterWalking(Movement movement, Monster monster, MapHelper mapHelper)
        {
            this.movement = movement;
            this.monster = monster;
            this.mapHelper = mapHelper;
        }
        public void whereTheMonsterShouldGoForAWalk(Map map)
        {
            monster.monsterWalkingInformations.onWay = true;
            monster.monsterWalkingInformations.Destination = map;
            monster.monsterWalkingInformations.unreachableDoors = new HashSet<Door>();
            GeneratingPath(monster, mapHelper);
        }
        public void Walking(MapHelper mapHelper, DataIOManager dataIOManager)
        {
            Direction directionToGo = WhichDirectionToGo(monster.monsterWalkingInformations.path, monster, mapHelper, dataIOManager);
            if (directionToGo != Direction.Null)
            {
                movement.MoveCharacter(monster, directionToGo);
            }
            else
            {
                if (monster.monsterWalkingInformations.path.Count == 1)
                {
                    //it arrived
                    monster.monsterWalkingInformations.UponArrival();
                }
                else
                {
                    //the next door cannot be reached
                    GeneratingPath(monster, mapHelper);
                }
            }
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
                if (path.Count == 1)
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
            foreach (var item in currentMap.mapInformations.mapLayout.doors)
            {
                if (item.Value.destinationMap == nextMap)
                {
                    door = item.Value;
                    doorDirection = item.Key;
                }
            }
            Dictionary<(int, int), int> depths = mapHelper.twoDArrayFunctions.TwoDArrayBFS(currentMap.twoDArray, monster.coordinates);
            if (ISNextMapAccessible(nextMap, monster, mapHelper))
            {
                Queue<Coordinates> path = mapHelper.twoDArrayFunctions.PathInTwoDArray(depths, currentMap.mapInformations.mapLayout.doorCoordinates[doorDirection], monster.coordinates);
                Direction direction = dataIOManager.enumFunctions.GetDirectionByAdjacentCoordinates(monster.coordinates, path.First());
                return direction;
            }
            return Direction.Null;
        }
        static void GeneratingPath(Monster monster, MapHelper mapHelper)
        {
            monster.monsterWalkingInformations.path = PathBetweenMaps(monster, monster.monsterWalkingInformations.Destination, mapHelper).Item2;
        }
        static bool ISNextMapAccessible(Map nextMap, Monster monster, MapHelper mapHelper)
        {
            bool result = false;
            foreach (Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper, monster))
            {
                if (door.destinationMap == nextMap)
                {
                    result = true;
                }
            }
            return result;
        }
        static List<Door> DoorsThatCanBeReached(Map map, Coordinates sourceCoordinates, MapHelper mapHelper, Monster monster)
        {
            Dictionary<(int, int), int> depths = mapHelper.twoDArrayFunctions.TwoDArrayBFS(map.twoDArray, sourceCoordinates);
            List<Door> doors = new List<Door>();
            foreach (var item in map.mapInformations.mapLayout.doorCoordinates)
            {
                if (depths.ContainsKey(mapHelper.parsing.CoordinatesToTupple(item.Value)))
                {
                    //the door can be reached
                    doors.Add(map.mapInformations.mapLayout.doors[item.Key]);
                }
                else
                {
                    //the door cannot be reached
                    monster.monsterWalkingInformations.unreachableDoors.Add(map.mapInformations.mapLayout.doors[item.Key]);
                }
            }
            return doors;
        }
        static (bool wayFound, Dictionary<Map, int> depths) MapBFS(Monster monster, Map Destination, MapHelper mapHelper)
        {
            bool wayFound = false;
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            foreach (Door door in DoorsThatCanBeReached(monster.mapWhereIsLocated, monster.coordinates, mapHelper, monster))//adding adjacent maps (of destination map), that can be reached
            {
                Console.WriteLine(door);
                depths.Add(door.destinationMap, 0);
                queue.Enqueue(door.destinationMap);
                visited.Add(door.destinationMap);
            }
            while (queue.Count > 0)
            {
                Map map = queue.Dequeue();
                foreach (Door door in map.mapInformations.mapLayout.doors.Values)
                {
                    if (monster.monsterWalkingInformations.unreachableDoors.Contains(door) == false) //there could be a doors, that monster already found unreachable and it knows about them 
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
        static (bool wayFound, Queue<Map>) PathBetweenMaps(Monster monster, Map DestinationMap, MapHelper mapHelper)
        {

            (bool wayFound, Dictionary<Map, int> depths) = MapBFS(monster, DestinationMap, mapHelper);
            if (wayFound == false)
            {
                Console.WriteLine("This will never happen?");
                return (wayFound, new Queue<Map>());
            }
            Queue<Map> path = new Queue<Map>();
            Map currentMap = DestinationMap;
            path.Enqueue(currentMap);
            while (currentMap != monster.mapWhereIsLocated)
            {
                foreach (Door door in currentMap.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = door.destinationMap;

                    if (depths[adjacentMap] < depths[currentMap] && adjacentMap != monster.mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                        path.Enqueue(adjacentMap);
                    }
                    else if (adjacentMap == monster.mapWhereIsLocated)
                    {
                        currentMap = adjacentMap;
                    }
                }
            }
            Queue<Map> reversed = new Queue<Map>(path.Reverse());
            return (wayFound, reversed);
        }
    }
}