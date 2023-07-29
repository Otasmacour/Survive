using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class PlayerActions
    {
        Model model;
        Characters characters;
        Movement movement;
        DataIOManager dataIOManager;
        public PlayerActions(Characters characters, Movement movement, DataIOManager dataIOManager)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
        }
        public void Action()
        {
            PlayerMovement(characters, movement, dataIOManager);
        }
        static void PlayerMovement(Characters characters, Movement movement, DataIOManager dataIOManager)
        {
            char ch = Console.ReadKey().KeyChar;
            Direction movementDirection = dataIOManager.enumFunctions.EnumMovementDirectionAssignment(ch);
            movement.MoveCharacter(characters.player, movementDirection);
        }
    }
}
