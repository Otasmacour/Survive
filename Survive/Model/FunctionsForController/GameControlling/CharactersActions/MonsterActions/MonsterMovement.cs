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
        Monster monster;
        MapHelper mapHelper;
        public MonsterWalking monsterWalking;
        DataIOManager dataIOManager;
        public MonsterMovement(Monster monster, MapHelper mapHelper, Movement movement, DataIOManager dataIOManager)
        {
            this.monster = monster;
            this.mapHelper = mapHelper;
            this.monsterWalking = new MonsterWalking(movement, monster, mapHelper, this);
            this.dataIOManager = dataIOManager;
        }
        public void Movement()
        {
            if(monster.monsterWalkingInformations.onWay) //the monster is heading somewhere
            {
                monsterWalking.Walking(mapHelper, dataIOManager);
            }
        }  
    }
}
