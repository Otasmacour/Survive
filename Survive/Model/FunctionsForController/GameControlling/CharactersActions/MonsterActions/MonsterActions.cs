using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterActions
    {
        Characters characters;
        Movement movement;
        DataIOManager dataIOManager;
        RoomMapCollection roomMapCollection;
        MapHelper mapHelper;
        Monster monster;
        public MonsterMovement monsterMovement;
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager, RoomMapCollection roomMapCollection, MapHelper mapHelper)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapHelper = mapHelper;
            this.monster = characters.monster;
            this.monsterMovement = new MonsterMovement(monster, mapHelper);
        }
        public void Action()
        {
            if (monster.monsterMovementInformations.onWay)
            {
                monsterMovement.Movement(monster, mapHelper, dataIOManager, movement);
            }
        }        
    }
}