# About
High-school seminary work - 2023/2024  
Survive is terminal-based game where the player's objective is to break free from a locked house. To succeed, they must collect all the necessary items and unlock the main door, all while avoiding encounters with the monster.   
# Controls
  - **Movement:** `w`, `a`, `s`, `d`
  - **Switching between items in inventory:** `Spacebar`
  - **Use Item:** `u`
  - **Pick up Item:** `p`
  - **Drop Item:** `h`
  
![mainDoor](https://github.com/Otasmacour/Survive/assets/111227700/969d8eb0-5726-409a-825c-db0bc8387a48)  
This is what the game looks like from a player's perspective.  

# Important
 - as I already hinted when I mentioned the heartbeat, there are sounds in the game for almost all actions the player can perform, so turn on the sound on your computer!!

# Monster's modes
Monster has 3 modes:
- Walking, the most common, simply walks through the house and moves around random rooms.  
- Chasing, this mode is triggered when the monster spots the player, and when it spots the player, the monster chases the player around the room.  
- Searching, this last mode is triggered when the monster is in chase mode but the player escapes to another room, then the monster searches nearby rooms for a while until it gets tired of it, and there is a chance during this mode that it will search the closets in the room it is in.

![MonsterMovement2 drawio (1)](https://github.com/user-attachments/assets/11e8b9fe-7922-41e7-8a34-7386926d5211)

# Hints
 - The volume and frequency of the heartbeat tells the player the distance of the monster.
 - The monster is 7 feet tall, so the secret tunnel is too low for it to enter.  
 - If the player makes a noise that the monster can hear from the distance where it is, the monster will come.
 - In addition to gradually unlocking the main door (in this case just stand in front of it), for all chests or other objects where an item is used, the player must stand right where the chest/whatever is located, just like when collecting items.
 - if you shoot the monster, you have one minute before the monster respawns in the basement
 - What specific items are used for is pretty intuitive in my opinion, especially with the alerts that pops up when a player uses an item the wrong way when it doesn't meet the right conditions for use - usually being used in the wrong place).
   
For almost all items you'll see the names, but for some things where it doesn't make sense, it doesn't show, so to make sure you don't get lost, here's an explanation of items without name:  
  - **Monster:** `A`
  - **Player:** `T`
  - **Wall:** `x`
  - **Door:** `d`
  - **Secret Door:** `s`
  - **Closet:** `c`
  - **Floor:** `.`
# Gameplay(skip if you don't want spoilers)
Items usage:
- With a **Hammer**, you can break the wooden barrier on the main door, the **WoodenChest**, and the **Chain** holding the **Shovel** on the wall.
- With a **Shovel**, you can dig up the **BuriedChest** in the garden.
- **Bullets** can be loaded into the **Gun**, which can then be used to shoot the monster.
- Picking up the **Backpack** from the ground increases the player's inventory space by two.
- Dropping the **Plate** on the ground causes a very loud noise and leaves behind a **BrokenPlate**.
- Using the **PadlockCode** can remove the coded lock from the main door.
- The **MasterKey** is the third item of three needed to use at the main door for the player to escape.
- The **WoodenKey** unlocks the **WoodenChest**.
- The **IronKey** unlocks the **IronChest**.
- The **MysteriousKey** unlocks the **BuriedChest**.
- The **WeaponKey** unlocks the **WeaponChest**.

Map of house:

 ![Floor-1 drawio](https://github.com/user-attachments/assets/cd8a9ca1-dbb9-4164-b1ca-76e99b245930)
 
Floor -1 (basement)

![Floor0 drawio](https://github.com/user-attachments/assets/508d723c-7709-49d8-80b8-64bbc14492d8)

Floor 0

![Floor1 drawio](https://github.com/user-attachments/assets/0b73c071-23ec-4cfc-9292-bc7310bf62ed)

Floor 1
