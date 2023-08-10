using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BoolFunctions
    {
        Monster monster;
        Player player;
        public BoolFunctions(Monster monster, Player player)
        {
            this.monster = monster;
            this.player = player;
        }
        public bool MonsterSeesThePlayer()
        {
            if (monster.mapWhereIsLocated == player.mapWhereIsLocated && player.visible)
            {
                return true;
            }
            return false;
        }
        public bool MonsterCanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Wall)
                {
                    return true;
                }
            }
            return true;
        }
        public bool JustFloorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 1 && FloorThere(twoDArray, coordinates))
            {
                    return true;
            }
            return false;
        }
        static bool FloorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Floor)
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
