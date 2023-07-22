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
        public MovementDirection EnumMovementDirectionAssignment(char c)
        {
            MovementDirection movementDirection = new MovementDirection();
            movementDirection = MovementDirection.Null;
            if (c == 'w')
            {
                movementDirection = MovementDirection.Up;
            }
            else if (c == 's')
            {
                movementDirection = MovementDirection.Down;
            }
            else if (c == 'a')
            {
                movementDirection = MovementDirection.Left;
            }
            else if (c == 'd')
            {
                movementDirection = MovementDirection.Right;
            }
            return movementDirection;
        }
    }
}
