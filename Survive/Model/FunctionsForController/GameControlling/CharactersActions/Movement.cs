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
                MoveUp(character, map);
            }
            else if (movementDirection == Direction.Left)
            {
                MoveLeft(character, map);
            }
            else if (movementDirection == Direction.Down)
            {
                MoveDown(character, map);
            }
            else if (movementDirection == Direction.Right)
            {
                MoveRight(character, map);
            }
        }
        void MoveUp(Character character, Map map)
        {
            PerformChacterMove(character, map, -1, 0, Direction.Up);
        }
        void MoveLeft(Character character, Map map)
        {
            PerformChacterMove(character, map, 0, -1, Direction.Left);
        }
        void MoveRight(Character character, Map map)
        {
            PerformChacterMove(character, map, 0, 1, Direction.Right);
        }
        void MoveDown(Character character, Map map)
        {
            PerformChacterMove(character, map, 1, 0, Direction.Down);
        }
        void StairsNameChangeAccordingToDirection(Map roomFromWhere, Door door,  Direction sourceDirection)
        {
            Map stairs = door.destinationMap;
            Map roomToWhere = stairs.mapInformations.mapLayout.doors[sourceDirection].destinationMap; //If error is here, then the stairs are not connected to the other one room.
            if (roomFromWhere.mapInformations.floorNumber > roomToWhere.mapInformations.floorNumber)
            {
                stairs.name = "Stairs Down";
            }
            else if (roomFromWhere.mapInformations.floorNumber < roomToWhere.mapInformations.floorNumber)
            {
                stairs.name = "Stairs up";
            }
        }
        void PerformChacterMove(Character character, Map map, int yOffset, int xOffset, Direction sourceDirection)
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
                if(door.destinationMap != null) //I wrote this when there could be door, that leads nowhere. I will probably remove this, when code is done.
                {
                    mapOperations.CharacterRelocation(character, map, door.destinationMap, character.coordinates, door.transitionPointCoordinates);
                    if(door.destinationMap.mapInformations.mapType == MapType.Stairs) 
                    {
                        StairsNameChangeAccordingToDirection(map, door, sourceDirection);
                    }
                }
            }
        }
    }
}
