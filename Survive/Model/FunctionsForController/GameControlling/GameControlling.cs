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
        Movement movement { get; set; }
        public PlayerActions playerActions { get; set; }
        public GameControlling(Maps maps, Characters characters, DataIOManager dataIOManager)
        {
            this.functionsForInitialization = new FunctionsForInitialization(maps, characters);
            this.movement = new Movement(maps.mapsFunctions.mapHelper, maps.mapsFunctions.mapOperations);
            this.playerActions = new PlayerActions(characters,this.movement,dataIOManager);
        }
    }
}
