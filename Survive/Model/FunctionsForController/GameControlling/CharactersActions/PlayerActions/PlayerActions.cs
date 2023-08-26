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
        public PlayerActions(Characters characters, Movement movement, DataIOManager dataIOManager, Player player, MapHelper mapHelper, MonsterActions monsterActions, Alerts alerts)
        {
            this.characters = characters;
            this.monster = characters.monster;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.player = player;
            this.playerItemManipulation = new PlayerItemManipulation(player, mapHelper, monster, monsterActions, alerts);
        }
        public UserIntent Action(char c)
        {
            UserIntent userIntent = dataIOManager.enumFunctions.GetUserIntents(c);
            switch(userIntent)
            {
                case UserIntent.Move:
                    PlayerMovement(player, movement, dataIOManager.enumFunctions.GetDirectionByChar(c)); break;
                case UserIntent.Drop:
                    playerItemManipulation.DropItem(player); break;
                case UserIntent.PickUp:
                    playerItemManipulation.PickUpItem(player); break;
                case UserIntent.Use:
                    playerItemManipulation.UseItem(player); break;
                case UserIntent.SwitchItem:
                    playerItemManipulation.SwitchItem(player); break;
            }
            return userIntent;
        }
        static void PlayerMovement(Player player, Movement movement, Direction movementDirection)
        {
            movement.MoveCharacter(player, movementDirection);
        }
    }
}