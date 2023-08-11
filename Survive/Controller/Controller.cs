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
            gameControlling.monsterActions.monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(model.game.maps.roomMapCollection.roomsByFloor[0][0]);
            while(model.game.info.run)
            {
                Console.Clear();
                view.PrintMap(model.game.characters.player.mapWhereIsLocated);
                gameControlling.monsterActions.Action();
                foreach(var item in model.game.maps.mapsFunctions.mapHelper.returnFunctions.AdjacentCoordinates(model.game.characters.player.mapWhereIsLocated.twoDArray, model.game.characters.player.coordinates, 8))
                {
                    Console.WriteLine(item.Key.ToString()+" "+model.game.maps.mapsFunctions.mapHelper.parsing.CoordinatesToTupple(item.Value));
                }
                gameControlling.playerActions.Action();
            }
        }
    }
}
