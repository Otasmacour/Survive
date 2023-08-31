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
        Parsing parsing { get; set; }
        ReturnFunctions returnFunctions;
        public BoolFunctions(Monster monster, Player player, Parsing parsing, ReturnFunctions returnFunctions)
        {
            this.monster = monster;
            this.player = player;
            this.parsing = parsing;
            this.returnFunctions = new ReturnFunctions(this);
        }
        public bool HideoutThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Furniture)
                {
                    Furniture furniture = (Furniture)obj;
                    if(furniture.canHideThere)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsPlayerWithinRangeOfMonster()
        {
            var adjacentCoordinatesOfMonster = returnFunctions.GetAdjacentCoordinates(monster.mapWhereIsLocated.twoDArray, monster.coordinates, 8);
            foreach (Coordinates adjacentCoordinates in adjacentCoordinatesOfMonster.Values)
            {
                if (parsing.CoordinatesToTupple(adjacentCoordinates) == parsing.CoordinatesToTupple(player.coordinates)) // The player is within range of the monster
                {
                    return true;
                }
            }
            return false;
        }
        public bool MonsterSeesThePlayer()
        {
            if (monster.mapWhereIsLocated == player.mapWhereIsLocated && player.visible)
            {
                return true;
            }
            return false;
        }
        public bool PlayerCanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates) //At this moment, this method seems unnecessary, but in future there will be gameobjects, that monster cannot step on
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Wall)
                {
                    return false;
                }
            }
            return true;
        }
        public bool MonsterCanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Wall)
                {
                    return false;
                }
                if (obj is Furniture)
                {
                    return false;
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
        public bool JustFloorAndCharacterThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 2 && FloorThere(twoDArray, coordinates) && CharacterThere(list))
            {
                return true;
            }
            return false;
        }
        public bool PlayerThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Player)
                {
                    return true;
                }
            }
            return false;
        }
        public bool ChestThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Chest)
                {
                    return true;
                }
            }
            return false;
        }
        public bool MainDoorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is MainDoor)
                {
                    return true;
                }
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
        public bool ItemThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Item)
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
        public bool SecretDoorThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 1)
            {
                if (list[0] is SecretDoor)
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
        public bool AreTheCoordinatesAdjacent(List<GameObject>[,] twoDArray, Coordinates sourceCoordinates, Coordinates destinationCoordinates)
        {
            var adjacentCoordinates = returnFunctions.GetAdjacentCoordinates(twoDArray, sourceCoordinates, 4);
            foreach (var item in adjacentCoordinates)
            {
                if (parsing.CoordinatesToTupple(item.Value) == parsing.CoordinatesToTupple(destinationCoordinates))
                {
                    return true;
                }
            }
            return false; //this will happen, if the destinationCoordinates are not among the adjacent of sourceCoordinates
        }
    }
}
