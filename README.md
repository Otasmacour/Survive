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
- S **Hammer** se dá rozbít prkenná zábrana na hlavních dveřích, **WoodenChest** a **Chain**, který drží **Shovel** přidělanou ke zdi.
- S **Shovel** se dá v zahradě vykopat **BuriedChest**.
- **Bullets** se dají nabít do **Gun** a s tou se poté dá zastřelit monstrum.
- Při zvednutí **Backpack** ze země se hráči zvýší prostor v inventáři o dva.
- Upuštěním **Plate** na zem se způsobí velmi hlasitý zvuk a na jeho místě zbyde **BrokenPlate**.
- Použitím **PadlockCode** se dá z hlavních dveří sundat zámek na kód.
- **MasterKey** je třetí předmět ze tří, který je třeba použít u hlavních dveří, aby hráč mohl utéci.
- **WoodenKey** odemyká **WoodenChest**.
- **IronKey** odemyká **IronChest**.
- **MysteriousKey** odemyká **BuriedChest**.
- **WeaponKey** odemyká **WeaponChest**.
Map of house:
 ![Floor-1 drawio](https://github.com/user-attachments/assets/cd8a9ca1-dbb9-4164-b1ca-76e99b245930)
Floor -1 (basement)
![Floor0 drawio](https://github.com/user-attachments/assets/508d723c-7709-49d8-80b8-64bbc14492d8)
Floor 0
![Floor1 drawio](https://github.com/user-attachments/assets/0b73c071-23ec-4cfc-9292-bc7310bf62ed)
Floor 1
# Important
 - as I already hinted when I mentioned the heartbeat, there are sounds in the game for almost all actions the player can perform, so turn on the sound on your computer!!
