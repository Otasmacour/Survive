using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class EnumsMappingManager
    {
        public EnumsMappingManager(EnumFunctions enumFunctions) 
        {
            FillingOppositeDirection(enumFunctions.oppositeDirection);
            FillingDirectionByChar(enumFunctions.directionByChar);
            FillingItemManipulationIntentByChar(enumFunctions.itemManipulationIntentByChar);
        }
        static void FillingOppositeDirection(Dictionary<Direction, Direction> oppositeDirections)
        {
            oppositeDirections.Add(Direction.Left, Direction.Right);
            oppositeDirections.Add(Direction.Right, Direction.Left);
            oppositeDirections.Add(Direction.Up, Direction.Down);
            oppositeDirections.Add(Direction.Down, Direction.Up);
        }
        static void FillingDirectionByChar(Dictionary<char, Direction> directionByChar)
        {
            directionByChar.Add('w', Direction.Up);
            directionByChar.Add('W', Direction.Up);
            directionByChar.Add('a', Direction.Left);
            directionByChar.Add('A', Direction.Left);
            directionByChar.Add('s', Direction.Down);
            directionByChar.Add('S', Direction.Down);
            directionByChar.Add('d', Direction.Right);
            directionByChar.Add('D', Direction.Right);
        }
        static void FillingItemManipulationIntentByChar(Dictionary<char, UserIntents> ItemManipulationIntentByChar)
        {
            ItemManipulationIntentByChar.Add('p', UserIntents.PickUp);
            ItemManipulationIntentByChar.Add('P', UserIntents.PickUp);
            ItemManipulationIntentByChar.Add('u', UserIntents.Use);
            ItemManipulationIntentByChar.Add('U', UserIntents.Use);
            ItemManipulationIntentByChar.Add('h', UserIntents.Drop);
            ItemManipulationIntentByChar.Add('H', UserIntents.Drop);
        }
    }
}