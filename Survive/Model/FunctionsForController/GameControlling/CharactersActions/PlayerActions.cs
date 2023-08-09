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
        public PlayerActions(Characters characters, Movement movement, DataIOManager dataIOManager, Player player)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.player = player;
        }
        public void Action()
        {
            char c = Console.ReadKey().KeyChar;
            UserIntents userIntents = dataIOManager.enumFunctions.GetUserIntents(c);
            switch(userIntents)
            {
                case UserIntents.Move:
                    PlayerMovement(player, movement, dataIOManager.enumFunctions.GetDirectionByChar(c));
                    return;
            }
        }
        static void PlayerMovement(Player player, Movement movement, Direction movementDirection)
        {
            movement.MoveCharacter(player, movementDirection);
        }
    }
}
