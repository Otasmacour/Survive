using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Movement
    {
        public Movement(MapHelper mapHelper, MapOperations mapOperations)
        {
            this.mapHelper = mapHelper;
            this.mapOperations = mapOperations;
        }
        MapHelper mapHelper;
        MapOperations mapOperations;
        public void MoveCharacter(Character character, Direction movementDirection)
        {
            Map map = character.mapWhereIsLocated;
            if (movementDirection == Direction.Up)
            {
                MoveUp(character);
            }
            else if (movementDirection == Direction.Left)
            {
                MoveLeft(character);
            }
            else if (movementDirection == Direction.Down)
            {
                MoveDown(character);
            }
            else if (movementDirection == Direction.Right)
            {
                MoveRight(character);
            }
        }
        void MoveUp(Character character)
        {
            PerformChacterMove(character, -1, 0, Direction.Up);
        }
        void MoveLeft(Character character)
        {
            PerformChacterMove(character, 0, -1, Direction.Left);
        }
        void MoveRight(Character character)
        {
            PerformChacterMove(character, 0, 1, Direction.Right);
        }
        void MoveDown(Character character)
        {
            PerformChacterMove(character, 1, 0, Direction.Down);
        }
        void PerformChacterMove(Character character, int yOffset, int xOffset, Direction sourceDirection)
        {
            Map mapFromWhere = character.mapWhereIsLocated;
            Map mapToWhere = mapFromWhere;
            Coordinates newCoordinates = new Coordinates();
            newCoordinates.y = character.coordinates.y + yOffset;
            newCoordinates.x = character.coordinates.x + xOffset;
            if (mapHelper.boolFunctions.DoorThere(mapFromWhere.twoDArray, newCoordinates))
            {
                Door door = mapHelper.returnFunctions.GetDoorThere(mapFromWhere, newCoordinates);
                door.CheckForPossibleStairsAndChangeTheNameInCase(mapFromWhere, door, sourceDirection);
                newCoordinates = door.transitionPointCoordinates;
                mapToWhere = door.destinationMap;
            }
            else if(mapHelper.boolFunctions.SecretDoorThere(mapFromWhere.twoDArray, newCoordinates))
            {
                SecretDoor secretDoor = mapHelper.returnFunctions.GetSecretDoorThere(mapToWhere, newCoordinates);
                newCoordinates = secretDoor.transitionPointCoordinates;
                mapToWhere = secretDoor.destinationMap;
            }
            if (character.CanGoThere(mapToWhere.twoDArray, newCoordinates))//Each character may have it differently
            {
                mapOperations.CharacterRelocation(character, mapFromWhere, mapToWhere, character.coordinates, newCoordinates);
            }
            if (character is Player)
            {
                Player player = (Player)character;
                player.VisibilityUpdate(mapToWhere, newCoordinates, mapHelper);
            }
        }
    }
}