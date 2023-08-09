using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class EnumFunctions
    {
        EnumsMappingManager enumsMappingManager;
        public Dictionary<Direction, Direction> oppositeDirection = new Dictionary<Direction, Direction>();
        public Dictionary<char, Direction> directionByChar = new Dictionary<char, Direction>();
        public Dictionary<char, UserIntents> itemManipulationIntentByChar = new Dictionary<char, UserIntents>();
        public EnumFunctions() 
        {
            enumsMappingManager = new EnumsMappingManager(this);
        }
        public Direction GetOppositeDirection(Direction direction)
        {
            return oppositeDirection[direction];
        }
        public Direction GetDirectionByChar(char c)
        {
            return directionByChar[c];
        }
        public UserIntents GetUserIntents(char c)
        {
            if(directionByChar.ContainsKey(c))
            {
                return UserIntents.Move;
            }
            if(itemManipulationIntentByChar.ContainsKey(c))
            {
                return itemManipulationIntentByChar[c];
            }
            return UserIntents.Null;
        }
        public Direction GetDirectionByAdjacentCoordinates(Coordinates sourceCoordinates, Coordinates destinationCoordinates)
        {
            if(sourceCoordinates.x > destinationCoordinates.x)
            {
                return Direction.Left;
            }
            else if(sourceCoordinates.x < destinationCoordinates.x)
            {
                return Direction.Right;
            }
            else if(sourceCoordinates.y > destinationCoordinates.y)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
        
    }
}