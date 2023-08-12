using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{ 
    class MonsterMovement
    {
        Player player;
        Monster monster;
        MapHelper mapHelper;
        public MonsterWalking monsterWalking;
        public MonsterChasing monsterChasing;
        DataIOManager dataIOManager;
        public MonsterMovement(Monster monster, MapHelper mapHelper, Movement movement, DataIOManager dataIOManager, Player player)
        {
            this.monster = monster;
            this.mapHelper = mapHelper;
            this.monsterWalking = new MonsterWalking(movement, monster, mapHelper);
            this.monsterChasing = new MonsterChasing(movement, monster, mapHelper, player);
            this.dataIOManager = dataIOManager;
            this.player = player;
        }
        public void Movement() 
        {
            Update(monster, mapHelper);
            if(monster.monsterChasingInformations.chasing)
            {
                if (PlayerCoulBeKilled()) { return; }
                monsterChasing.ChasingUpdate();
                monsterChasing.Chase();
            }
            else if(monster.monsterWalkingInformations.onWay) //the monster is heading somewhere
            {
                monsterWalking.Walking(mapHelper, dataIOManager);
            }
        }
        static void Update(Monster monster, MapHelper mapHelper)
        {
            if(monster.monsterChasingInformations.chasing == false)
            {
                if (mapHelper.boolFunctions.MonsterSeesThePlayer())
                {
                    monster.monsterChasingInformations.chasing = true;
                }
            }
        }
        bool PlayerCoulBeKilled()
        {
            if(mapHelper.boolFunctions.IsPlayerWithinRangeOfMonster() && player.visible) { return true; }
            else { return false; }
        }
    }
}