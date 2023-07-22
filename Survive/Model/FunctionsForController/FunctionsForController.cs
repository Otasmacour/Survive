using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class FunctionsForController
    {
        public ReturningFunctionsForView returningFunctionsForView;
        public GameControlling gameControlling;
        public FunctionsForController(MapHelper mapHelper, Model model, Characters characters, DataIOManager dataIOManager)
        {
            this.returningFunctionsForView = new ReturningFunctionsForView(mapHelper, model);
            this.gameControlling = new GameControlling(model.game.maps, characters, dataIOManager);
        }
    }
}
