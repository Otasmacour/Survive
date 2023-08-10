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
        Player player;
        MapHelper mapHelper;
        public MonsterChasing(Movement movement, Monster monster, MapHelper mapHelper, Player player)
        {
            this.movement = movement;
            this.monster = monster;
            this.monsterChasingInformations = monster.monsterChasingInformations;
            this.mapHelper = mapHelper;
            this.player = player;
        }
        public void Chase()
        {
            if(monsterChasingInformations.specificLocation != null)
            {
                Console.WriteLine("It is about to Chase player");
                ChasePlayerInTwoDArray(mapHelper, movement, monster, player);
                monsterChasingInformations.specificLocation = null;
            }
            else
            {
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
                monsterChasingInformations.specificLocation = player.coordinates;
                Console.WriteLine("Monster sees player");
            }
            //here will be other statements for causes where the monster can't see the player but knows which door he left or which closet he entered etc.
        }
    }
}