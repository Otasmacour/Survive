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
        public EnumFunctions() 
        {
            enumsMappingManager = new EnumsMappingManager(this);
        }
        public Direction GetOppositeDirection(Direction direction)
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