using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterMovementInformations
    {
        Monster monster;
        public HashSet<Map> unVisitedMaps = new HashSet<Map>();
        public List<Map> path = new List<Map>();
        public MonsterMovementInformations(Monster monster)
        {
            this.monster = monster;
        }
        public void Update(RoomMapCollection roomMapCollection)
        {
            //path.Add(monster.mapWhereIsLocated);
            if (unVisitedMaps.Count == 0)
            {
                //path.Clear();
                //Console.WriteLine("All room visited, path:");
                //Map currentMap = monster.monsterMovementInformations.path[0];
                //Console.WriteLine("Floor " + currentMap.mapInformations.floorNumber.ToString() + " " + currentMap.name);
                //foreach (Map map in monster.monsterMovementInformations.path)
                //{
                //    if (currentMap != map)
                //    {
                //        Console.WriteLine("Floor " + map.mapInformations.floorNumber.ToString() + " " + map.name);
                //        currentMap = map;
                //    }
                //}
                //Console.ReadLine();
                AddRangeToHashSet(unVisitedMaps, roomMapCollection.list);
            }
            else
            {
                if (unVisitedMaps.Contains(monster.mapWhereIsLocated))
                {
                    unVisitedMaps.Remove(monster.mapWhereIsLocated);
                }
            }
        }
        static void AddRangeToHashSet<T>(HashSet<T> hashSet, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                hashSet.Add(item);
            }
        }
        public Map DecideWhereToGoForAWalk()
        {
            //Console.WriteLine("Monster is deciding, where to go");
            //Console.WriteLine("where it could go:");
            //foreach (Map map in monster.monsterMovementInformations.unVisitedMaps)
            //{
            //    Console.WriteLine("Floor " + map.mapInformations.floorNumber.ToString() + " " + map.name);
            //}
            //Console.WriteLine();
            //Console.WriteLine("Path:");
            //Map currentMap = monster.monsterMovementInformations.path[0];
            //Console.WriteLine("Floor " + currentMap.mapInformations.floorNumber.ToString() + " " + currentMap.name);
            //foreach (Map map in monster.monsterMovementInformations.path)
            //{
            //    if (currentMap != map)
            //    {
            //        Console.WriteLine("Floor " + map.mapInformations.floorNumber.ToString() + " " + map.name);
            //        currentMap = map;
            //    }
            //}
            Map mapToGo = unVisitedMaps.First();
            //Console.WriteLine();
            //Console.WriteLine("IT could go to " + "Floor " + mapToGo.mapInformations.floorNumber.ToString() + " " + mapToGo.name);
            //Console.ReadLine();
            return mapToGo;
        }
    }
}