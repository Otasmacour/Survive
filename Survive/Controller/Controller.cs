using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    public class Controller
    {
        Model model { get; set; }
        GameControlling gameControlling { get; set; }
        View view { get; set; }
        public Controller() 
        {
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            view = new View(model.game.maps.mapsFunctions.mapHelper);
        }
        public void Play()
        {
            gameControlling.functionsForInitialization.Initialization();
            while(model.game.info.run)
            {
                Console.Clear();
                view.PrintMap(model.game.characters.player.mapWhereIsLocated);
                gameControlling.monsterActions.Action();
                gameControlling.playerActions.Action();
            }
        }
    }
}
