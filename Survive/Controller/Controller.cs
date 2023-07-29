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
        ReturningFunctionsForView returningFunctionsForView { get; set; }
        View view { get; set; }
        public Controller() 
        {
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            this.returningFunctionsForView = model.functionsForController.returningFunctionsForView;
            view = new View();
        }
        public void Play()
        {
            gameControlling.functionsForInitialization.Initialization();
            while(model.game.info.run)
            {
                Console.Clear();
                view.PrintMap(returningFunctionsForView.mapWherePlayerIsLocated());
                Console.WriteLine(model.game.maps.roomMapCollection.list.Count.ToString());
                gameControlling.playerActions.Action();
            }
        }
    }
}
