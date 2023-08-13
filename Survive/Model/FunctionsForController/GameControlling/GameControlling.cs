using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class GameControlling
    {
        public FunctionsForInitialization functionsForInitialization { get; set; }
        public Movement movement { get; set; }
        public PlayerActions playerActions { get; set; }
        public MonsterActions monsterActions { get; set; }
        public CollisionController collisionController { get; set; }
        public GameInformations gameInformations { get; set; }
        public GameControlling(Maps maps, Characters characters, DataIOManager dataIOManager)
        {
            this.functionsForInitialization = new FunctionsForInitialization(maps, characters);
            this.movement = new Movement(maps.mapsFunctions.mapHelper, maps.mapsFunctions.mapOperations);
            this.playerActions = new PlayerActions(characters, this.movement, dataIOManager, characters.player);
            this.monsterActions = new MonsterActions(characters,this.movement,dataIOManager, maps.roomMapCollection, maps.mapsFunctions.mapHelper);
            this.collisionController = new CollisionController(characters, maps.mapsFunctions.mapHelper);
            this.gameInformations = new GameInformations(maps.mapsFunctions.mapHelper, characters);
        }
    }
}