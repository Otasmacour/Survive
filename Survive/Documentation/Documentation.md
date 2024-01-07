# MVC
The whole game runs on the **MVC** (**Model**-**View**-**Controller**) architecture. Where **Model** takes care of all the logic and all the complex issues. **View** provides purely graphical output and nothing else. The **Controller** is the main game loop, where the mechanics from **Model**(running the game) and **View** (graphical output) are called. 

#### The advantages of the MVC architecture in terms of my usage are as follows:
 - **Clarity** These three main components clearly segment the mechanics of the program, thus avoiding spaghetti code up front.
 - **Easier modifiability** Thanks to the View component, the separated graphical output, it is not difficult to modify the game graphics without requiring changes to the **Model**, the logical side of the game.
# About the documentation 
in my opinion, the vast majority of the code is very readable, and therefore it was often not necessary to write comments in the code itself. in the following documentation I will not discuss everything that happens in the code, because it would often be unnecessary. I will focus on those parts of the code that are more difficult to understand and require more complex reasoning, or are part of mechanics that are spread over multiple places in the code and thus difficult to understand from just one part of the code. I will also explain some of my decisions about why I did a given thing the way I did it.
The entire documentation is written in such a way that it is best understood by having the source code in question open at the same time as reading it.
# Code structure
Code structure.
The code is pretty much segmented and consists of 86 C# files, each for one class, exceptionally not a class but multiple enums.
At the beginning I didn't have it so segmented, but as the volume of code grew, it became difficult to find my way around. 
It's much clearer this way, each class contains exactly the mechanics one would expect from its name (or from the other methods in it, if the name is not quite clear). I followed the rule that if there are too many methods somewhere, and those methods could be split into subsets depending on what they are for, it's time to separate the methods and make their own classes.
This way the code is much more modifiable, it is immediately clear where some new mechanics should belong. It also makes the whole game code more readable, because it's easier to search through it, one just has to click through the folders one by one and eventually find one of the subsets of the mechanics one is looking for.
The way it is implemented is that each class has the classes whose mechanics it needs to use stored at the top as properies. In most cases this works like a pyramid, and classes rarely use the classes "above" them. The fact that there isn't any significant mess in this is ensured by the fact that constructor classes are called in the ones "above".
# Controller
In terms of branching out, the Controller is the top block of the pyramid, below which it branches out into multiple blocks (classes). 
I think one pretty well just needs to read the code in detail throughout the class to get a pretty good idea of how the whole program works. In fact, there are 2 main loops in the Controller, the first of which repeatedly calls the Escape method, which turns the game on, and when the program returns from that method, the player chooses if they want to play again if they lost, and if they won, they are shown the time it took to finish. Calling the Escape method starts an instance of the game that takes place entirely in the other loop, which consists of the 4 main methods for running the game. First the montrum moves, then the screen refreshes, then the player performs an action, and finally the sounds play. In this way, the loop of one game instance runs over and over again until the player runs away from the house or the monster catches him.
There are comments in the code that make understanding the sub-methods much easier.
What can be confusing at first in terms of what it does there and why, are the stopwatches that are used in three of the four sub-methods called in Escape. For the game to work, the mechanics of these three methods must be triggered with some delay. 
I can think of three ways to do this: 
- **Thread**, it's a class that provides a Thread.Sleep(int milliseconds) method that literally puts the program to sleep for a certain number of milliseconds at any given time. Using Thread for my purpose is a pretty terrible idea, the moment you need to manage more than one process (three in my case) it doesn't work together. I tried it and the result was as I expected, the game was stuttering.
- **Asynchronous loops**, it's exactly what it sounds like. Instead of the program running synchronously, it runs in multiple places at the same time. A possibility would be that each time Escape is called, three loops would start, each for one process, and that process would run in that loop. I tried this and it worked, but I ran into the problem that asynchronous loops can't actually be debugged. Normally (when the program is running synchronously), if I index an empty list somewhere, for example, the program will stop and Visual Studio will report an error. On the other hand, when such an error occurs in an asynchronous loop, the whole program freezes and it is very difficult to find out what the error is. That's why I rejected this option.
- **StopWatch**, a built-in class from which instances can be created so I can measure the elapsed time and reset it after startup. In the MonsterAction and DisplayUpdate methods, I use this to check if it's time to start the mechanics again. If it is, the monster moves, or the screen is updates, and then the stopwatch is reset again.
# Map
Since the game is in 2D, I implemented the game field as a two-dimensional array that contains the lists of the GameObject. Because when a player enters a position, both he and the GameObject that was there before he entered are in that position. There could have been more than one GameObject there originally, typically a floor and an item. That's why you need to have a list for that position, where everything that needs to be added is added, not just a slot that gets overwritten.
Map mechanics are divided into several classes according to the problems they handle:
 - **Maps**, the signpost, which branches further and is the main block under which everything about the maps is located. By making this class.
 - **RoomMapCollection**, a place where map information is stored. a list of all maps, a dictionary of maps by floor (which is a subset of that list), and a map where the monster respawns.
 - **MapOperations**, contains methods that manipulate the content of maps. They place items on them and change the player's position on the maps.
 - **MapCleaning**, it takes care of removing unused doors from the maps (it is explained a little bit below what unused doors are).
 - **MapsInitialization**, it creates maps and especially generates those two-dimensional arrays representing the game field.
 - **MapsLinking**, a Map instances are created here. Then there is a method for linking maps. Linking maps means that they link to each other through doors, so that when the player then steps through a door in a room, it moves the player in front of the door of the map where the door leads (the map that the door is linked to).
 - **MapHelper**, another such signpost that has multiple classes underneath it, all of which have in common that they return something and calling them does not directly start any processes that would affect the game itself
Classes, that are underneath the MapHelper
# What could I have done better + possible improvements for the future
This project is not perfect and is much bigger than anything I have done before. In making it, I basically locked myself in my room for half of the summer of 2023. I didn't follow any instructions for making the game and only watched a short video about what a model-view-controller is, after Peter Vincenc told me I should follow this architecture. So apart from the MVC pattern, the rest of the game's architecture is completely off the top of my head, and since I've moved on a bit in programming since writing Survive, I can see things in hindsight that could have been done better.
- **Unnecessary re-creation of an unnecessarily high number of classes and not using of static classes**
To be honest, while making off, I didn't know about the existence of static class feature, or at least I didn't know, what they are good for. Lots of the classes could be static without need of regenerating of them every game instance. Some of them even wouldn't require the existence of methods, to reset the game data inside the game logic to work. For example this classes could be static:
    - Every class underneath the MapHelper
    -  DataIOManager classes 
    -  Alerts
    -  SoundsController
- **Multiplayer**

- **Another Items**

- **Bigger house**

- **It's not just about Survive**

