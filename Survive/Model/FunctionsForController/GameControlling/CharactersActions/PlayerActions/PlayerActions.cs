using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class PlayerActions
    {
        Characters characters;
        Monster monster;
        Movement movement;
        DataIOManager dataIOManager;
        Player player;
        PlayerItemManipulation playerItemManipulation;
        public PlayerActions(Characters characters, Movement movement, DataIOManager dataIOManager, Player player, MapHelper mapHelper, MonsterActions monsterActions)
        {
            this.characters = characters;
            this.monster = characters.monster;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.player = player;
            this.playerItemManipulation = new PlayerItemManipulation(player, mapHelper, monster, monsterActions);
        }
        public void Action()
        {
            if(player.living == false)
            {
                return;
            }
            char c = Console.ReadKey().KeyChar;
            UserIntent userIntents = dataIOManager.enumFunctions.GetUserIntents(c);
            switch(userIntents)
            {
                case UserIntent.Move:
                    PlayerMovement(player, movement, dataIOManager.enumFunctions.GetDirectionByChar(c)); return;
                case UserIntent.Drop:
                    playerItemManipulation.DropItem(player); return;
                case UserIntent.PickUp:
                    playerItemManipulation.PickUpItem(player); return;
                case UserIntent.Use:
                    playerItemManipulation.UseItem(player); return;
                case UserIntent.SwitchItem:
                    playerItemManipulation.SwitchItem(player); return;
            }
        }
        static void PlayerMovement(Player player, Movement movement, Direction movementDirection)
        {
            movement.MoveCharacter(player, movementDirection);
        }
    }
}