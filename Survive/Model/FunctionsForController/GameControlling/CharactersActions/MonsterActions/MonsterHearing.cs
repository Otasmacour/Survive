using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterHearing
    {
        
        GameInformation gameInformation;
        SoundsController soundsController;
        MonsterMovement monsterMovement;
        MapHelper mapHelper;
        Monster monster;
        Player player;
        public MonsterHearing(SoundsController soundsController, GameInformation gameInformation, MonsterMovement monsterMovement, MapHelper mapHelper, Monster monster, Player player)
        {
            this.soundsController = soundsController;
            this.gameInformation = gameInformation;
            this.monsterMovement = monsterMovement;
            this.mapHelper = mapHelper;
            this.monster = monster;
            this.player = player;
        }
        public void Hear()
        {
            List<(Map map, Sound sound)> sounds = new List<(Map map, Sound sound)>(soundsController.unHeardByAMonster);
            foreach(var item in sounds) { soundsController.unHeardByAMonster.Remove(item); }
            if (monster.monsterChasingInformation.chasing || sounds.Count == 0) //If monster can see player, it won't pay attention to dropped items
            {
                return;
            }
            Map nearestAudibleMap = null;
            Console.Clear();
            foreach(var item in sounds)
            {
                if (mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, item.map) <= item.sound.noiceLevel) //That noise level is high enough for monster to hear it.
                {
                    if(nearestAudibleMap == null)
                    {
                        nearestAudibleMap = item.map;
                    }
                    else
                    {
                        if(mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, item.map) < mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, nearestAudibleMap)) //Selecting better options, if there are multiple sounds that can be heard, select the closest audible map.
                        {
                            nearestAudibleMap = item.map;
                        }
                    }
                }
            }
            if(nearestAudibleMap != null)
            {
                if(nearestAudibleMap is Tunnel)//Monster can't go into the tunnel, in this case it'S going into one of the normal adjacent rooms.
                {
                    monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(nearestAudibleMap.mapInformations.mapLayout.secretDoors[0].destinationMap, true);
                }
                else
                {
                    monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(nearestAudibleMap, true);
                }
                monster.monsterSearchingInformation.EndingOfSearching();
            }
        }
    }
}