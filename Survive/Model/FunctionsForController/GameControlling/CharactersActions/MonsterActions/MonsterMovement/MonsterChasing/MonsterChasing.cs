using System;
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
        MonsterChasingInformations monsterChasingInformations;
        MonsterSearchingInformation monsterSearchingInformation;
        Player player;
        MapHelper mapHelper;
        Random random = new Random();
        public MonsterChasing(Movement movement, Monster monster, MapHelper mapHelper, Player player)
        {
            this.movement = movement;
            this.monster = monster;
            this.monsterChasingInformations = monster.monsterChasingInformations;
            this.monsterSearchingInformation = monster.monsterSearchingInformation;
            this.player = player;
            this.mapHelper = mapHelper;   
        }
        public void Chase()
        {
            if (monsterChasingInformations.playerPosition != null)
            {
                //Console.WriteLine("It is about to Chase player");
                //Thread.Sleep(1000);
                ChasePlayerInTwoDArray(mapHelper, movement, monster, player);
                monsterChasingInformations.playerPosition = null;
            }
            else if (monsterChasingInformations.whereThePlayerHasGone != null)
            {
                //Console.Write("Where the player has gone: ");
                //Console.WriteLine(mapHelper.parsing.CoordinatesToTupple(monsterChasingInformations.whereThePlayerHasGone));
                //Thread.Sleep(1000);
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
        bool StartSearching()
        {
            if (monster.mapWhereIsLocated.mapInformations.mapLayout.doors.Count == 1 || true) //the true is just temporary YesOrNo(5) should be there
            {
                return true;
            }
            return false;
        }
        bool YesOrNo(int Probability)
        {
            int number = random.Next(1, Probability);
            if (number != 1) { return true; }
            else { return false; }
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