﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterMovementInformation
    {
        Monster monster;
        public HashSet<Map> unVisitedMaps = new HashSet<Map>();
        public HashSet<Map> searchedMaps = new HashSet<Map>();
        public List<Map> path = new List<Map>();
        public MonsterMovementInformation(Monster monster)
        {
            this.monster = monster;
        }
        public void Update(RoomMapCollection roomMapCollection)
        {
            if (unVisitedMaps.Count == 0)
            {
                AddRangeToHashSet(unVisitedMaps, roomMapCollection.list);
                searchedMaps.Clear();
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
            Map mapToGo = unVisitedMaps.First();
            return mapToGo;
        }
    }
}