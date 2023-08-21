﻿using System;
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
        
        GameInformations gameInformations;
        MonsterMovement monsterMovement;
        MapHelper mapHelper;
        Monster monster;
        Player player;
        public MonsterHearing(GameInformations gameInformations, MonsterMovement monsterMovement, MapHelper mapHelper, Monster monster, Player player)
        {
            this.gameInformations = gameInformations;
            this.monsterMovement = monsterMovement;
            this.mapHelper = mapHelper;
            this.monster = monster;
            this.player = player;
        }
        public void Hear()
        {
            Dictionary<Sound, Map> sounds = new Dictionary<Sound, Map>(gameInformations.sounds);
            if (monster.monsterChasingInformations.chasing || sounds.Count == 0) //If monster can see player, it won't pay attention to dropped items
            {
                return;
            }
            Map nearestAudibleMap = null;
            foreach(var sound in sounds)
            {
                if(mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, sound.Value) <= sound.Key.noiceLevel) //That noise level is high enough for monster to hear it
                {
                    if(nearestAudibleMap == null)
                    {
                        nearestAudibleMap = sound.Value;
                    }
                    else
                    {
                        if(mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, sound.Value) < mapHelper.returnFunctions.GetDistanceOfTwoMaps(monster.mapWhereIsLocated, nearestAudibleMap))
                        {
                            nearestAudibleMap = sound.Value;
                        }
                    }
                }
            }
            if(nearestAudibleMap != null)
            {
                if(nearestAudibleMap is Tunnel)
                {
                    //In tunnel, one of the door are closest to Player and then the destinationMap, where the door leads is closer to Player, this method finds the closer adjacent map and call whereTheMonsterShouldGoForAWalk with that map as an arqument
                    var depths = mapHelper.twoDArrayFunctions.TwoDArrayBFS(nearestAudibleMap.twoDArray, player.coordinates);
                    (SecretDoor doorLeadingToNearestAdjacentMap, int depth) bestOption = (nearestAudibleMap.mapInformations.mapLayout.secretDoors[0], 1000000);
                    foreach(SecretDoor secretDoor in nearestAudibleMap.mapInformations.mapLayout.secretDoors)
                    {
                        int doorDepth = depths[mapHelper.parsing.CoordinatesToTupple(nearestAudibleMap.mapInformations.mapLayout.secretDoorsCoordinates[secretDoor])];
                        if(doorDepth < bestOption.depth)
                        {
                            bestOption.doorLeadingToNearestAdjacentMap = secretDoor;
                            bestOption.depth = doorDepth;
                        }
                    }
                    monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(bestOption.doorLeadingToNearestAdjacentMap.destinationMap);            
                }
                else
                {
                    monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(nearestAudibleMap);
                }
                monster.monsterSearchingInformation.EndingOfSearching();
            }
        }
    }
}