using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterActions
    {
        Characters characters;
        Movement movement;
        DataIOManager dataIOManager;
        MonsterInformations movementInformations = new MonsterInformations();
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
        }
        public void Action()
        {
            Queue<Map> path = PathBetweenMaps(characters.monster.mapWhereIsLocated, characters.player.mapWhereIsLocated);
            foreach (Map map in path)
            {
                Console.WriteLine(map.name);
            }
            MonsterMovement(characters, movement, dataIOManager);

        }
        static void MonsterMovement(Characters characters, Movement movement, DataIOManager dataIOManager)
        {
            Direction movementDirection = Direction.Up;
            movement.MoveCharacter(characters.monster, movementDirection);
        }
        static Queue<Map> PathBetweenMaps(Map mapWhereIsLocated, Map DestinationMap)
        {
            Dictionary<Map,int> depths = BFS(mapWhereIsLocated, DestinationMap);
            Queue<Map> path = new Queue<Map>();
            Map currentMap = DestinationMap;
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
        static Dictionary<Map, int> BFS(Map Start, Map Destination) //Breadth First Search
        {
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            depths.Add(Start, 0);
            queue.Enqueue(Start);
            visited.Add(Start);           
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
            return depths;
        }
    }
}
