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
            GameReset();
        }
        public void Play()
        {
            bool run = true;
            while (run)
            {
                GameReset();
                bool result = Escape();
                if (result) { Console.WriteLine("You escaped"); }
                else { Console.WriteLine("Game over"); }
                Console.WriteLine("Do you want to play again? [Y/n]");
                bool answered = false;
                while(answered != true) 
                {
                    char answer = Console.ReadKey().KeyChar;
                    if(answer == 'y' || answer == 'Y') { answered = true;}
                    else if (answer == 'n' || answer == 'N') { answered = true; run = false;}
                }  
            }
        }
        bool Escape()
        {
            gameControlling.functionsForInitialization.Initialization();
            gameControlling.monsterActions.monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(model.game.maps.roomMapCollection.roomsByFloor[0][0]);
            while(model.game.info.run)
            {
                ResetScreen();
                gameControlling.monsterActions.Action();
                ResetScreen();
                gameControlling.collisionController.MonsterAndPlayerCollision();
                gameControlling.playerActions.Action();
            }
            if(model.game.info.win)
            {
                return true;
            }
            return false;
        }
        void ResetScreen()
        {
            Console.Clear();
            view.PrintMap(model.game.characters.player.mapWhereIsLocated);
        }
        void GameReset()
        {
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            view = new View(model.game.maps.mapsFunctions.mapHelper);
        }
    }
}
