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
        MapOperations mapOperations;
        Monster monster;
        public MonsterMovement monsterMovement;
        MonsterHearing monsterHearing;
        Stopwatch respawnStopwatch = new Stopwatch();
        public MonsterActions(Characters characters, Movement movement, DataIOManager dataIOManager, RoomMapCollection roomMapCollection, MapsFunctions mapsFunctions, GameInformation gameInformation, SoundsController soundsController)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapHelper = mapsFunctions.mapHelper;
            this.mapOperations = mapsFunctions.mapOperations;
            this.monster = characters.monster;
            this.monsterMovement = new MonsterMovement(monster, mapHelper, movement, dataIOManager, characters.player, roomMapCollection, gameInformation.alerts);
            this.monsterHearing = new MonsterHearing(soundsController, gameInformation, monsterMovement, mapHelper, monster, characters.player);
        }
        public void Action()
        {
            if(monster.living == false) { MonsterRespawn(respawnStopwatch, monster, mapOperations, roomMapCollection.monsterRespawnMap, mapHelper); return; }
            monsterHearing.Hear();
            monsterMovement.Movement();
        }
        static void MonsterRespawn(Stopwatch respawnStopwatch, Monster monster, MapOperations mapOperations, Map respawnMap, MapHelper mapHelper)
        {
            int respawnTime = 60000;
            if(respawnStopwatch.IsRunning == false)
            {  
                respawnStopwatch.Start();
            }
            else
            {
                if(respawnStopwatch.ElapsedMilliseconds >= respawnTime)
                {
                    Coordinates coordinates = mapHelper.returnFunctions.GetRandomAvailableCoordinatesonMap(respawnMap, 1)[0];
                    mapOperations.CharacterRelocation(monster, monster.mapWhereIsLocated, respawnMap, monster.coordinates, coordinates);
                    monster.living = true;
                    respawnStopwatch.Reset();
                }
            }
        }
    }
}