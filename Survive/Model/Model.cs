using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Survive
{
    class Model
    {
        public DataIOManager dataIOManager;
        public Game game;
        public FunctionsForController functionsForController;
        public Model()
        {
            dataIOManager = new DataIOManager();
            game = new Game(dataIOManager);
            functionsForController = new FunctionsForController(game.maps.mapsFunctions.mapHelper, this, game.characters, dataIOManager);
        }
    }
}
