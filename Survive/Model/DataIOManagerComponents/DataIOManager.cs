using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class DataIOManager
    {
        public string mainFolderPath;
        public TxtFunctions txtFunctions;
        public JsonFunctions jsonFunctions;
        public DataIOManager()
        {
            mainFolderPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName + @"\Source\";
            txtFunctions = new TxtFunctions(mainFolderPath);
            jsonFunctions = new JsonFunctions(mainFolderPath, this, txtFunctions);
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
