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
        MonsterHearing monsterHearing;
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager, RoomMapCollection roomMapCollection, MapHelper mapHelper, GameInformations gameInformations)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapHelper = mapHelper;
            this.monster = characters.monster;
            this.monsterMovement = new MonsterMovement(monster, mapHelper, movement, dataIOManager, characters.player, roomMapCollection);
            this.monsterHearing = new MonsterHearing(gameInformations, monsterMovement, mapHelper, monster, characters.player);
        }
        public void Action()
        {
            monsterHearing.Hear();
            monsterMovement.Movement();
        }        
    }
}