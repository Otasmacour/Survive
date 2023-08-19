﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class GameControlling
    {
        public MapPlacer mapPlacer { get; set; }
        public Movement movement { get; set; }
        public PlayerActions playerActions { get; set; }
        public MonsterActions monsterActions { get; set; }
        public CollisionController collisionController { get; set; }
        public GameInformations gameInformations { get; set; }
        public GameControlling(Maps maps, Characters characters, DataIOManager dataIOManager)
        {
            this.gameInformations = new GameInformations(maps.mapsFunctions.mapHelper, characters);
            this.mapPlacer = new MapPlacer(maps, characters, gameInformations);
            this.movement = new Movement(maps.mapsFunctions.mapHelper, maps.mapsFunctions.mapOperations);
            this.monsterActions = new MonsterActions(characters,this.movement,dataIOManager, maps.roomMapCollection, maps.mapsFunctions.mapHelper, gameInformations);
            this.playerActions = new PlayerActions(characters, this.movement, dataIOManager, characters.player, maps.mapsFunctions.mapHelper, monsterActions);
            this.collisionController = new CollisionController(characters, maps.mapsFunctions.mapHelper);
        }
    }
}