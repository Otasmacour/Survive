using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class FunctionsForController
    {
        public GameControlling gameControlling;
        public SoundsController soundsController;
        public FunctionsForController(MapHelper mapHelper, Model model, Characters characters, DataIOManager dataIOManager)
        {
            this.gameControlling = new GameControlling(model.game.maps, characters, dataIOManager);
            this.soundsController = new SoundsController(dataIOManager.mainFolderPath);
        }

    }
}
