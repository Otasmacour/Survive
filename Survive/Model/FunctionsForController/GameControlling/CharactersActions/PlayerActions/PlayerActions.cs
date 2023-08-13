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
        Movement movement;
        DataIOManager dataIOManager;
        Player player;
        PlayerItemManipulation playerItemManipulation;
        public PlayerActions(Characters characters, Movement movement, DataIOManager dataIOManager, Player player, MapHelper mapHelper)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.player = player;
            this.playerItemManipulation = new PlayerItemManipulation(player, mapHelper);
        }
        public void Action()
        {
            if(player.living == false)
            {
                return;
            }
            char c = Console.ReadKey().KeyChar;
            UserIntents userIntents = dataIOManager.enumFunctions.GetUserIntents(c);
            switch(userIntents)
            {
                case UserIntents.Move:
                    PlayerMovement(player, movement, dataIOManager.enumFunctions.GetDirectionByChar(c)); return;
                case UserIntents.Drop:
                    playerItemManipulation.DropItem(player); return;
                case UserIntents.PickUp:
                    playerItemManipulation.PickUpItem(player); return;
                case UserIntents.Use:
                    playerItemManipulation.UseItem(player); return;
            }
        }
        static void PlayerMovement(Player player, Movement movement, Direction movementDirection)
        {
            movement.MoveCharacter(player, movementDirection);
        }
    }
}