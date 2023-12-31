﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterChasing
    {
        Movement movement;
        Monster monster;
        MonsterChasingInformation monsterChasingInformations;
        MonsterSearchingInformation monsterSearchingInformation;
        Player player;
        MapHelper mapHelper;
        public MonsterChasing(Movement movement, Monster monster, MapHelper mapHelper, Player player)
        {
            this.movement = movement;
            this.monster = monster;
            this.monsterChasingInformations = monster.monsterChasingInformation;
            this.monsterSearchingInformation = monster.monsterSearchingInformation;
            this.player = player;
            this.mapHelper = mapHelper;   
        }
        public void Chase()
        {
            if (monsterChasingInformations.playerPosition != null)
            {
                ChasePlayerInTwoDArray(mapHelper, movement, monster, player);
                monsterChasingInformations.playerPosition = null;
            }
            else if (monsterChasingInformations.whereThePlayerHasGone != null)
            {
                bool lastTime = false;
                if (mapHelper.boolFunctions.AreTheCoordinatesAdjacent(monster.mapWhereIsLocated.twoDArray, monster.coordinates, monsterChasingInformations.whereThePlayerHasGone))
                {
                    lastTime = true;
                }
                Direction directionToGo = mapHelper.twoDArrayFunctions.GetDirectionWhileWalkingOnTwoDArray(monsterChasingInformations.whereThePlayerHasGone, monster.coordinates, monster.mapWhereIsLocated.twoDArray);
                movement.MoveCharacter(monster, directionToGo);
                if (lastTime) { monsterChasingInformations.whereThePlayerHasGone = null; }
            }
            else
            {
                monsterSearchingInformation.searching = true;
                monsterChasingInformations.EndingOfChasing();
            }
        }
        static void ChasePlayerInTwoDArray(MapHelper mapHelper, Movement movement, Monster monster, Player player)
        {
            Direction directionToGo = mapHelper.twoDArrayFunctions.GetDirectionWhileWalkingOnTwoDArray(player.coordinates, monster.coordinates, player.mapWhereIsLocated.twoDArray);
            movement.MoveCharacter(monster, directionToGo);
        }
        public void ChasingUpdate()
        {
            if (mapHelper.boolFunctions.MonsterSeesThePlayer())
            {
                monsterChasingInformations.playerPosition = player.coordinates;
            }
        }
    }
}