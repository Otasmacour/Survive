using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BoolFunctions
    {
        public bool JustFloorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 1)
            {
                if (list[0] is Floor)
                {
                    return true;
                }
            }
            return false;
        }
        public bool DoorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 1)
            {
                if (list[0] is Door)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CharacterThere(List<GameObject> list)
        {
            foreach (GameObject obj in list)
            {
                if (obj is Character)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
