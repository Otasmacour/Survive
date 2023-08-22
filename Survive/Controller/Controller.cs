using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                else { Console.WriteLine("Game over, "+model.game.info.theWayThePlayerDied); }
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
            //gameControlling.monsterActions.monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(model.game.maps.roomMapCollection.roomsByFloor[0][0]);
            Stopwatch playerActionStopwatch = new Stopwatch();
            Task gameUpdateTask = Task.Run(async () => await GameUpdating(view, model));//For the screen updating once a 50 millisecond
            Task monsterActionTask = Task.Run(async () => await MonsterAction(model));//For the monster moving once a second
            while (model.game.info.run)
            {
                PlayerAction(playerActionStopwatch);//When the player takes an action, the monster moves too, this negates the player's normally high speed, so he cannot espace easily
                gameControlling.gameInformations.SoundsUpdate();
                
            }
            if (model.game.info.win)
            {
                return true;
            }
            return false;
        }
        static async Task MonsterAction(Model model)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            while (model.game.info.run)
            {

                if (stopwatch.ElapsedMilliseconds >= 1000)
                {
                    model.functionsForController.gameControlling.monsterActions.Action();
                    stopwatch.Restart();
                }
            }
        }
        static async Task GameUpdating(View view, Model model)
        {
            Stopwatch displayStopwatch = Stopwatch.StartNew();
            displayStopwatch.Start();
            while (model.game.info.run)
            {
                
                if (displayStopwatch.ElapsedMilliseconds >= 50)
                {
                    model.functionsForController.gameControlling.collisionController.MonsterAndPlayerCollision();
                    view.Display(model.game.characters.player.mapWhereIsLocated, model.functionsForController.gameControlling.gameInformations.GetMonsterDistance(), model.game.characters.player.inventory, model.functionsForController.gameControlling.gameInformations.GetItemsWithinPlayersReach());
                    displayStopwatch.Restart();
                }
            }
        }
        void PlayerAction(Stopwatch stopwatch)
        {
            stopwatch.Reset();
            stopwatch.Start();
            char c = '\0';
            while (stopwatch.ElapsedMilliseconds < 50)
            {
                if (Console.KeyAvailable)
                {
                    c = Console.ReadKey(intercept: true).KeyChar;
                    gameControlling.playerActions.Action(c);
                    gameControlling.monsterActions.Action(); 
                }
            }
        }
        void GameReset()
        {
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            view = new View(model.game.maps.mapsFunctions.mapHelper);
        }
    }
}