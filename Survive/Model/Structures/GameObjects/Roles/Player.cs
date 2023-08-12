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
        public MapHelper mapHelper;
        public Monster monster;
        public Player(Character character, Informations info)
        {
            RoleConstructor(character, info);
        }
        public void VisibilityUpdate(Map map, Coordinates newCoordinates, MapHelper mapHelper)
        {
            if(CanGoThere(map.twoDArray, newCoordinates)) //When this wasn't here, player could walk to wall and lose his invisibility
            {
                if (mapHelper.boolFunctions.HideoutThere(map.twoDArray, newCoordinates) && mapWhereIsLocated!= monster.mapWhereIsLocated)
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
                if (obj is Door && mapHelper.boolFunctions.MonsterSeesThePlayer())// In this case, the player goes through the door, but the monster is in the same room, so it starts chasing the player into the room where the door leads
                {
                    monster.monsterChasingInformations.whereThePlayerHasGone = coordinates;
                }
                if (obj is Wall)
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
        public override void Die()
        {
            die();
            info.win = false;
            info.run = false;
        }
        public override int GetPriorityNumber()
        {
            return 31;
        }
    }
}
