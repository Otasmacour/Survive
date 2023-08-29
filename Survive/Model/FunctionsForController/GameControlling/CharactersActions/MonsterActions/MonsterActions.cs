using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Stopwatch respawnStopwatch = new Stopwatch();
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager, RoomMapCollection roomMapCollection, MapHelper mapHelper, GameInformations gameInformations, SoundsController soundsController)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapHelper = mapHelper;
            this.monster = characters.monster;
            this.monsterMovement = new MonsterMovement(monster, mapHelper, movement, dataIOManager, characters.player, roomMapCollection, gameInformations.alerts);
            this.monsterHearing = new MonsterHearing(soundsController, gameInformations, monsterMovement, mapHelper, monster, characters.player);
        }
        public void Action()
        {
            if(monster.living == false) { MonsterRespawn(respawnStopwatch, monster); return; }
            monsterHearing.Hear();
            monsterMovement.Movement();
        }
        static void MonsterRespawn(Stopwatch respawnStopwatch, Monster monster)
        {
            int respawnTime = 1200;
            if(respawnStopwatch.IsRunning == false)
            {
                respawnStopwatch.Start();
            }
            else
            {
                if(respawnStopwatch.ElapsedMilliseconds >= respawnTime)
                {
                    monster.living = true;
                    respawnStopwatch.Reset();
                }
            }
        }
    }
}