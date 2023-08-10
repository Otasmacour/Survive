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
        DataIOManager dataIOManager;
        public MonsterMovement(Monster monster, MapHelper mapHelper, Movement movement, DataIOManager dataIOManager, Player player)
        {
            this.monster = monster;
            this.mapHelper = mapHelper;
            this.monsterWalking = new MonsterWalking(movement, monster, mapHelper);
            this.dataIOManager = dataIOManager;
            this.player = player;
        }
        public void Movement()
        {
            if(monster.mapWhereIsLocated == player.mapWhereIsLocated)
            {
                Console.WriteLine("They are in the same room!!");
                Console.ReadKey();
            }
            if(monster.monsterWalkingInformations.onWay) //the monster is heading somewhere
            {
                monsterWalking.Walking(mapHelper, dataIOManager);
            }
        }  
    }
}
