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
        public void Play()
        {
            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start(); //The stopwatch measures how fast the player managed to escape.
            bool run = true;
            while (run)
            {
                GameReset(stopwatch);
                bool result = Escape();
                TimeSpan elapsed = stopwatch.Elapsed;
                string formattedTime = $"{(int)elapsed.TotalMinutes}:{elapsed.Seconds:D2}.{elapsed.Milliseconds / 10:D2}";
                if (result) { Console.WriteLine("You escaped, your time: " + formattedTime); }
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
            Stopwatch displayStopwatch = new Stopwatch();
            Stopwatch playerActionStopwatch = new Stopwatch();
            Stopwatch monsterActionStopwatch = new Stopwatch();
            while (model.game.info.run)
            {
                MonsterAction(monsterActionStopwatch);
                DisplayUpdate(displayStopwatch);
                PlayerAction(playerActionStopwatch);
                model.functionsForController.soundsController.PlaySounds();
            }
            if (model.game.info.win)
            {
                return true;
            }
            return false;
        }
        void MonsterAction(Stopwatch monsterActionStopwatch)
        {
            int pauseBetweenMonsterAction = 1000;
            if (model.game.characters.monster.monsterChasingInformations.chasing || model.game.characters.monster.monsterSearchingInformation.searching || model.game.characters.monster.monsterWalkingInformations.followingTheNoise) { pauseBetweenMonsterAction = 500; }
            if(monsterActionStopwatch.IsRunning == false)
            {
                monsterActionStopwatch.Start();
            }
            if (monsterActionStopwatch.ElapsedMilliseconds >= pauseBetweenMonsterAction)
            {
                model.functionsForController.gameControlling.monsterActions.Action();
                monsterActionStopwatch.Restart();
            }
        }
        void DisplayUpdate(Stopwatch displayStopwatch)
        {
            if (displayStopwatch.IsRunning == false)
            {
                displayStopwatch.Start();
            }
            if (displayStopwatch.ElapsedMilliseconds >= 50)
            {
                model.functionsForController.gameControlling.collisionController.MonsterAndPlayerCollision();
                view.Display(model.game.characters.player.mapWhereIsLocated, model.functionsForController.gameControlling.gameInformations.GetMonsterDistance(), model.game.characters.player.inventory, model.functionsForController.gameControlling.gameInformations.GetItemsWithinPlayersReach(), model.functionsForController.gameControlling.gameInformations.alerts.GetAlerts(), model.game.characters.monster);
                displayStopwatch.Restart();
            }
        }
        void PlayerAction(Stopwatch stopwatch)
        {
            stopwatch.Restart();
            char c;
            while (stopwatch.ElapsedMilliseconds < 50)
            {
                if (Console.KeyAvailable)
                {
                    c = Console.ReadKey(intercept: true).KeyChar;
                    UserIntent userIntent = gameControlling.playerActions.Action(c);
                    if(userIntent == UserIntent.Move && model.game.characters.player.visible && model.game.characters.player.mapWhereIsLocated == model.game.characters.monster.mapWhereIsLocated)////When the player moves, the monster moves too, this negates the player's normally high speed, so he cannot espace easily.
                    {
                        gameControlling.monsterActions.Action();
                    }
                }
            }
        }
        void GameReset(Stopwatch stopwatch)
        {
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            view = new View(model.game.maps.mapsFunctions.mapHelper);
            stopwatch.Restart();
        }
    }
}