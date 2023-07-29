using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class EnumFunctions
    {
        
        public EnumFunctions() 
        {
        }
        private Dictionary<Direction, Direction> OppositeDirection;
        public Dictionary<Direction, Direction> oppositeDirection
        {
            get
            {
                if(OppositeDirection == null)
                {
                    OppositeDirection = new Dictionary<Direction, Direction>();
                    OppositeDirection.Add(Direction.Left, Direction.Right);
                    OppositeDirection.Add(Direction.Right, Direction.Left);
                    OppositeDirection.Add(Direction.Up, Direction.Down);
                    OppositeDirection.Add(Direction.Down, Direction.Up);
                }
                return OppositeDirection;
            }
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
    }
}
