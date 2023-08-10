using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class Player : Character
    {
        public Item item;
        public bool visible = true;
        public Player(Character character)
        {
            RoleConstructor(character);
        }
        public void VisibilityUpdate(Map map, Coordinates coordinates, Coordinates newCoordinates, MapHelper mapHelper)
        {
            if(CanGoThere(map.twoDArray, newCoordinates)) //I the cause, when this wasn't here, player could walk to wall and lose his invisibility
            {
                if (mapHelper.boolFunctions.HideoutThere(map.twoDArray, newCoordinates))
                {
                    this.visible = false;
                }
                else
                {
                    this.visible = true;
                }
            }
        }
        public override bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if(obj is Wall)
                {
                    return false;
                }
                if(obj is Furniture)
                {
                    Furniture furniture = (Furniture)obj;
                    if(furniture.canHideThere == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
