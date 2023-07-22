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
        public void MoveCharacter(Character character, MovementDirection movementDirection)
        {
            Map map = character.mapWhereIsLocated;
            if (movementDirection == MovementDirection.Up)
            {
                MoveUp(character, map);
            }
            else if (movementDirection == MovementDirection.Left)
            {
                MoveLeft(character, map);
            }
            else if (movementDirection == MovementDirection.Down)
            {
                MoveDown(character, map);
            }
            else if (movementDirection == MovementDirection.Right)
            {
                MoveRight(character, map);
            }
        }
        void MoveUp(Character character, Map map)
        {
            PerformChacterMove(character, map, -1, 0);
        }
        void MoveLeft(Character character, Map map)
        {
            PerformChacterMove(character, map, 0, -1);
        }
        void MoveRight(Character character, Map map)
        {
            PerformChacterMove(character, map, 0, 1);
        }
        void MoveDown(Character character, Map map)
        {
            PerformChacterMove(character, map, 1, 0);
        }
        void PerformChacterMove(Character character, Map map, int yOffset, int xOffset)
        {
            Coordinates newCoordinates = new Coordinates();
            newCoordinates.y = character.coordinates.y + yOffset;
            newCoordinates.x = character.coordinates.x + xOffset;
            if (mapHelper.JustFloorThere(map, newCoordinates))
            {
                mapOperations.CharacterRelocation(character, map, map, character.coordinates, newCoordinates);
            }
            else if(mapHelper.DoorThere(map, newCoordinates))
            {
                Door door = mapHelper.ReturnDoorThere(map, newCoordinates);
                if(door.map != null)
                {
                    mapOperations.CharacterRelocation(character, map, door.map, character.coordinates, door.transitionPointCoordinates);
                }
            }
        }
    }
}
