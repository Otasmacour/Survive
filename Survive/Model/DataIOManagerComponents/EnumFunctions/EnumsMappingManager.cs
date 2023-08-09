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
        }
        static void FillingOppositeDirection(Dictionary<Direction, Direction> oppositeDirections)
        {
            oppositeDirections.Add(Direction.Left, Direction.Right);
            oppositeDirections.Add(Direction.Right, Direction.Left);
            oppositeDirections.Add(Direction.Up, Direction.Down);
            oppositeDirections.Add(Direction.Down, Direction.Up);
        }       
    }
}