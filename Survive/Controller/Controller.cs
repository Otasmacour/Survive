using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    public class Controller
    {
        ViewConsole viewConsole { get; set; }
        Model model { get; set; }
        GameControlling gameControlling { get; set; }
        public Controller() 
        {
        }
        public void Play()
        {
            Stopwatch stopwatch = new Stopwatch();//The stopwatch measures how fast the player managed to escape.
            bool run = true;
            while (run)
            {
                GameReset(stopwatch);
                bool result = Escape();//Returns whether the player managed to escape (true) or lost (false).
                TimeSpan elapsed = stopwatch.Elapsed;
                string formattedTime = $"{(int)elapsed.TotalMinutes}:{elapsed.Seconds:D2}.{elapsed.Milliseconds / 10:D2}";//This creates an elapsed time string in the format - minutes:seconds.milliseconds
                if (result) { Console.WriteLine("You've escaped, your time: " + formattedTime); }
                else { Console.WriteLine("Game over, " + model.game.info.theWayThePlayerDied); }
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
            Stopwatch displayStopwatch = new Stopwatch(); displayStopwatch.Start();
            Stopwatch playerActionStopwatch = new Stopwatch();
            Stopwatch monsterActionStopwatch = new Stopwatch(); monsterActionStopwatch.Start();
            while (model.game.info.run)
            {
                MonsterAction(monsterActionStopwatch);
                DisplayUpdate(displayStopwatch);
                PlayerAction(playerActionStopwatch);
                //Explanations can be found inside the definitions of each method
                model.functionsForController.soundsController.PlaySounds();//This plays all the sounds that happened in this iteration.
            }
            return model.game.info.win;
        }
        void MonsterAction(Stopwatch monsterActionStopwatch)
        {
            int pauseBetweenMonsterAction = 1000;
            if (model.game.characters.monster.monsterChasingInformation.chasing || model.game.characters.monster.monsterSearchingInformation.searching || model.game.characters.monster.monsterWalkingInformation.followingTheNoise) { pauseBetweenMonsterAction = 500; }
            //When the monster is chasing or searching for the player, its speed is faster than when just walking.
            if (monsterActionStopwatch.ElapsedMilliseconds >= pauseBetweenMonsterAction)//PauseBetweenMonsterAction determines how often the monster performs the action.
            {
                model.functionsForController.gameControlling.monsterActions.Action(); //All the mechanics of the monster are hidden here, be it respawning, hearing, or especially movement, which makes up the vast majority of the monster's game logic.
                monsterActionStopwatch.Restart();
            }
        }
        void DisplayUpdate(Stopwatch displayStopwatch)
        {
            if (displayStopwatch.ElapsedMilliseconds >= 50)
            {
                model.functionsForController.gameControlling.collisionController.MonsterAndPlayerCollision();//This method checks if the player is within range of the monster (and can be killed), if so it will end the game.
                if (Proxy.outputType == OutputType.Console) { viewConsole.Display(model.game.characters.player.mapWhereIsLocated, model.functionsForController.gameControlling.gameInformation.GetMonsterDistance(), model.game.characters.player.inventory, model.functionsForController.gameControlling.gameInformation.GetItemsWithinPlayersReach(), model.functionsForController.gameControlling.gameInformation.alerts.GetAlerts(), model.game.characters.monster); }
                //This Display method gets as arguments all required data and prints the game scene into the terminal/shows in forms
                displayStopwatch.Restart();
            }
        }
        void PlayerAction(Stopwatch stopwatch)
        {
            stopwatch.Restart();//Stopwatch starts measuring the time and as long as it is under 50 milliseconds, the loop waits for the player's reaction, when the time runs out, the program continues, thanks to this the monster can perform an action even if the player does nothing, because the program doesn't wait for the player's action forever.
            char c;
            while (stopwatch.ElapsedMilliseconds < 50)//The player can only take an action once every 50 milliseconds.
            {
                if (Proxy.KeyAvaible)
                {
                    c = Proxy.ReadKey();
                    UserIntent userIntent = gameControlling.playerActions.Action(c);//This handles the player's action and returns a UserIntent enum that tells what kind of action the player took, such as: Move or item manipulation action - Drop, PickUp, Use, SwitchItem and it can also return UserIntent.Null if the player pressed a key on the keyboard that is not associated with any action.
                    if (userIntent == UserIntent.Move)//When the player moves, the monster moves too, this negates the player's normally high speed, so he cannot espace easily.
                    {
                        gameControlling.monsterActions.Action(); //All the mechanics of the monster are hidden here, be it respawning, hearing, or especially movement, which makes up the vast majority of the monster's game logic.
                    }
                }
            }
        }
        void GameReset(Stopwatch stopwatch)
        {
            //This resets the game data so the game can start again from the beginning
            model = new Model();
            gameControlling = model.functionsForController.gameControlling;
            viewConsole = new ViewConsole(model.game.maps.mapsFunctions.mapHelper);
            stopwatch.Restart();
        }
    }
}