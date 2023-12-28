# MVC
The whole game runs on the **MVC** (**Model**-**View**-**Controller**) architecture. Where **Model** takes care of all the logic and all the complex issues. **View** provides purely graphical output and nothing else. The **Controller** is the main game loop, where the mechanics from **Model**(running the game) and **View** (graphical output) are called. 

#### The advantages of the MVC architecture in terms of my usage are as follows:
 - **Clarity** These three main components clearly segment the mechanics of the program, thus avoiding spaghetti code up front.
 - **Easier modifiability** Thanks to the View component, the separated graphical output, it is not difficult to modify the game graphics without requiring changes to the **Model**, the logical side of the game.

# Controller
#### Class fields:
 - **viewConsole** instance of class **ViewConsole** from which the methods that print the output are called.
 - **viewForms** instance of class **ViewForms**, which is the **Windows Forms** alternative for **ViewConsole**
 - **model** instance of class **Model** from which the game logic methods are called or the properties output is taken.
 - **gameControlling** instance of class **GameControlling** from which methods are called that are directly made to be called in the controller and in which most of the game logic is grouped.
#### Class methods:
 - **Play()** The main method that is called at the beginning of the program. It runs a loop in which it repeatedly calls **Escape()**, according to the result of the game played, it shows whether the player won or lost and if he won (managed to escape), it shows the time. Next, this method calls functions that capture input from the player as to whether he wants to play again. When the *while(run)* loop ends, the entire program ends.
 - **Escape()** it has several instances of **Stopwatch** class: **displayStopwatch**(used for frame rate of Displaying game), **playerActionStopwatch**(used for timing of player action) and **monsterActionStopwatch**(used for timing of monster action). It runs a loop *while (model.game.info.run)* in which an instance of the game takes place. When the game ends (the player escapes or dies), the loop ends and the method returns bool (escaped-true, killed-false).
 - **MonsterAction()** uses **monsterActionStopwatch** for timing when the monster should take action. If mode chasing, or searching is enabled, the pause between actions is reduced from one second to half a second.
 - **PlayerAction()** uses **playerActionStopwatch** for timing when the player can perform the action. It uses **ReadyKey()** from **Proxy** static class, which retrieves the char of the key pressed by the player. **Action()** called from gameControlling gets the character of the pressed key and performs the action, then returns the enum **UserIntent** of the performed action (**Move** or item manipulation action - **Drop**, **PickUp**, **Use**, **SwitchItem** and it can also return **UserIntent.Null** if the player pressed a key on the keyboard that is not associated with any action.) And if the **UserIntent** was **Move** the monster moves too, this negates the player's normally high speed, so he cannot espace easily.
 - **GameReset()** resets the game data so the game can start again from the beginning.