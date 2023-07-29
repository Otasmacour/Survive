using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class EnumFunctions
    {
        Dictionary<Direction, Direction> oppositeDirection = new Dictionary<Direction, Direction>();
        public EnumFunctions() 
        {
            // Initialization is here, bcs doing it in static method nonvisible from outside of this class, cannot be perform, as it cannot reach the Dictionary oppositeDirection
            oppositeDirection.Add(Direction.Left, Direction.Right);
            oppositeDirection.Add(Direction.Right, Direction.Left);
            oppositeDirection.Add(Direction.Up, Direction.Down);
            oppositeDirection.Add(Direction.Down, Direction.Up);
        }
        public Direction OppositeDirection(Direction direction)
        {
            return oppositeDirection[direction];
        }
        public Direction EnumMovementDirectionAssignment(char c)
        {
            Direction movementDirection = new Direction();
            movementDirection = Direction.Null;
            if (c == 'w')
            {
                movementDirection = Direction.Up;
            }
            else if (c == 's')
            {
                movementDirection = Direction.Down;
            }
            else if (c == 'a')
            {
                movementDirection = Direction.Left;
            }
            else if (c == 'd')
            {
                movementDirection = Direction.Right;
            }
            return movementDirection;
        }
    }
}
