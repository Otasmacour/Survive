using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class EnumFunctions
    {
        public Dictionary<Direction, Direction> oppositeDirections = new Dictionary<Direction, Direction>();
        public EnumFunctions() 
        {
            FillingOppositeDirectionDictionary(oppositeDirections);
        }
        private Dictionary<Direction, Direction> OppositeDirection;
        static void FillingOppositeDirectionDictionary(Dictionary<Direction,Direction> oppositeDirections)
        {
            oppositeDirections.Add(Direction.Left, Direction.Right);
            oppositeDirections.Add(Direction.Right, Direction.Left);
            oppositeDirections.Add(Direction.Up, Direction.Down);
            oppositeDirections.Add(Direction.Down, Direction.Up);
        }
        public Direction GetOppositeDirection(Direction direction)
        {
            return oppositeDirections[direction];
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