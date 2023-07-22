using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapHelper
    {
        public bool JustFloorThere(Map map, Coordinates coordinates)
        {
            List<GameObject> list = map.twoDArray[coordinates.y, coordinates.x];
            if(list.Count == 1)
            {
                if (list[0] is Floor)
                {
                    return true;
                }
            }
            return false;
        }
        public bool DoorThere(Map map, Coordinates coordinates)
        {
            List<GameObject> list = map.twoDArray[coordinates.y, coordinates.x];
            if (list.Count == 1)
            {
                if (list[0] is Door)
                {
                    return true;
                }
            }
            return false;
        }
        public Door ReturnDoorThere(Map map, Coordinates coordinates)
        {
            foreach (GameObject obj in map.twoDArray[coordinates.y,coordinates.x])
            {
                if (obj is Door)
                {
                    return (Door)obj;
                }
            }
            return new Door(); //this can obviously never happen
        }
        static bool CharacterThereBool(List<GameObject> list)
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
        static Character ReturnCharacterThere(List<GameObject> list)
        {
            foreach (GameObject obj in list)
            {
                if (obj is Character)
                {
                    return (Character)obj;
                }
            }
            return new Character(); //this can obviously never happen
        }
        public List<Coordinates> RandomAvailableCoordinatesonMap(Map map, int numberOfRequestedCoordinates)
        {
            List<Coordinates> list = new List<Coordinates>();
            HashSet<(int, int)> coordinatesHash = new HashSet<(int, int)>();
            int mapHeight = map.twoDArray.GetLength(0);
            int mapWidth = map.twoDArray.GetLength(1);
            Random random = new Random();
            for (int i = 0; i < numberOfRequestedCoordinates; i++)
            {
                (int y, int x) coordinatesTupple = (random.Next(1, mapHeight - 1), random.Next(1, mapWidth - 1));
                Coordinates coordinates = new Coordinates();
                coordinates.y = coordinatesTupple.y;
                coordinates.x = coordinatesTupple.x;
                while (coordinatesHash.Contains(coordinatesTupple) || JustFloorThere(map, coordinates) == false || map.mapInformations.occupiedPlaces.Contains(coordinates))
                {
                    coordinatesTupple = (random.Next(1, mapHeight - 1), random.Next(1, mapWidth - 1));
                    coordinates.y = coordinatesTupple.y;
                    coordinates.x = coordinatesTupple.x;
                }
                coordinatesHash.Add(coordinatesTupple);
                list.Add(coordinates);
            }
            return list;
        }
        public GameObject mostPreferredObjectInList(List<GameObject> objects)
        {
            // 1 - corpse/character
            // 2 - floor/wall
            GameObject mostPreferredObject = objects[0];
            int priorityNumber = 10;
            foreach (GameObject obj in objects)
            {
                if (obj is Corpse && priorityNumber > 1)
                {
                    mostPreferredObject = obj;
                    priorityNumber = 1;
                }
                else if (obj is Character && priorityNumber > 1)
                {
                    mostPreferredObject = obj;
                    priorityNumber = 1;
                }
                else if (obj is Floor && priorityNumber > 2)
                {
                    mostPreferredObject = obj;
                    priorityNumber = 2;
                }
                else if (obj is Wall && priorityNumber > 2)
                {
                    mostPreferredObject = obj;
                    priorityNumber = 2;
                }
            }
            return mostPreferredObject;
        }
        public List<Character> SearchAdjacentCharacter(Character character)
        {
            List<Character> AdjacentCharacters = new List<Character>();
            List<GameObject>[,] twoDArray = character.mapWhereIsLocated.twoDArray;
            int y = character.coordinates.y;
            int x = character.coordinates.x;
            if (y - 1 >= 0)
            {
                List<GameObject> up = twoDArray[y - 1, x];
                //Console.WriteLine("up: " + mostPreferredObjectInList(up));
                if (CharacterThereBool(up))
                {
                    AdjacentCharacters.Add(ReturnCharacterThere(up));
                }
            }
            if (y + 1 < twoDArray.GetLength(0))
            {
                List<GameObject> down = twoDArray[y + 1, x];
                // Console.WriteLine("down: " + mostPreferredObjectInList(down));
                if (CharacterThereBool(down))
                {
                    AdjacentCharacters.Add(ReturnCharacterThere(down));
                }
            }
            if (x - 1 >= 0)
            {
                List<GameObject> left = twoDArray[y, x - 1];
                // Console.WriteLine("left: " + mostPreferredObjectInList(left));
                if (CharacterThereBool(left))
                {
                    AdjacentCharacters.Add(ReturnCharacterThere(left));
                }
            }
            if (x + 1 < twoDArray.GetLength(1))
            {
                List<GameObject> right = twoDArray[y, x + 1];
                // Console.WriteLine("right: " + mostPreferredObjectInList(right));
                if (CharacterThereBool(right))
                {
                    AdjacentCharacters.Add(ReturnCharacterThere(right));
                }
            }
            return AdjacentCharacters;
        }
    }
}
