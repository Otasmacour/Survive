using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class ReturnFunctions
    {
        BoolFunctions boolFunctions;
        public ReturnFunctions(BoolFunctions boolFunctions) 
        {
            this.boolFunctions = boolFunctions;
        }
        public Item GetItemThere(Map map, Coordinates coordinates)
        {
            foreach (GameObject obj in map.twoDArray[coordinates.y, coordinates.x])
            {
                if (obj is Item)
                {
                    return (Item)obj;
                }
            }
            throw new NotImplementedException();
        }
        public Door GetDoorThere(Map map, Coordinates coordinates)
        {
            foreach (GameObject obj in map.twoDArray[coordinates.y, coordinates.x])
            {
                if (obj is Door)
                {
                    return (Door)obj;
                }
            }
            return new Door(); //this can obviously never happen
        }
        public Character GetCharacterThere(List<GameObject> list)
        {
            foreach (GameObject obj in list)
            {
                if (obj is Character)
                {
                    return (Character)obj;
                }
            }
            return new NullCharacter(); //this can obviously never happen
        }
        public List<Coordinates> GetRandomAvailableCoordinatesonMap(Map map, int numberOfRequestedCoordinates)
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
                while (coordinatesHash.Contains(coordinatesTupple) || boolFunctions.JustFloorThere(map.twoDArray, coordinates) == false || map.mapInformations.mapLayout.occupiedPlaces.Contains(coordinates))
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
        public GameObject GetMostPreferredObjectInList(List<GameObject> objects)
        {
            GameObject mostPreferredObject = objects[0];
            int priorityNumber = mostPreferredObject.GetPriorityNumber();
            foreach (GameObject obj in objects)
            {
                int priority = obj.GetPriorityNumber();
                if (priorityNumber > priority)
                {
                    mostPreferredObject = obj;
                    priorityNumber = priority;
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
                if (boolFunctions.CharacterThere(up))
                {
                    AdjacentCharacters.Add(GetCharacterThere(up));
                }
            }
            if (y + 1 < twoDArray.GetLength(0))
            {
                List<GameObject> down = twoDArray[y + 1, x];
                // Console.WriteLine("down: " + mostPreferredObjectInList(down));
                if (boolFunctions.CharacterThere(down))
                {
                    AdjacentCharacters.Add(GetCharacterThere(down));
                }
            }
            if (x - 1 >= 0)
            {
                List<GameObject> left = twoDArray[y, x - 1];
                // Console.WriteLine("left: " + mostPreferredObjectInList(left));
                if (boolFunctions.CharacterThere(left))
                {
                    AdjacentCharacters.Add(GetCharacterThere(left));
                }
            }
            if (x + 1 < twoDArray.GetLength(1))
            {
                List<GameObject> right = twoDArray[y, x + 1];
                // Console.WriteLine("right: " + mostPreferredObjectInList(right));
                if (boolFunctions.CharacterThere(right))
                {
                    AdjacentCharacters.Add(GetCharacterThere(right));
                }
            }
            return AdjacentCharacters;
        }
        public Dictionary<Direction, Coordinates> GetAdjacentCoordinates(List<GameObject>[,] twoDArray, Coordinates coordinates, int fourOrEight)
        {
            Dictionary<Direction, Coordinates> adjacentCoordinates = new Dictionary<Direction, Coordinates>();
            bool Left = false; bool Right = false; bool Up = false; bool Down = false;
            if (coordinates.x > 0)
            {
                Left = true;
                Coordinates left = new Coordinates();
                left.y = coordinates.y;
                left.x = coordinates.x - 1;
                adjacentCoordinates.Add(Direction.Left, left);
            }
            if(coordinates.x <= twoDArray.GetLength(1) - 2)
            {
                Right = true;
                Coordinates right = new Coordinates();
                right.y = coordinates.y;
                right.x = coordinates.x + 1;
                adjacentCoordinates.Add(Direction.Right, right);
            }
            if(coordinates.y > 0)
            {
                Up = true;
                Coordinates up = new Coordinates();
                up.y = coordinates.y - 1;
                up.x = coordinates.x;
                adjacentCoordinates.Add(Direction.Up, up);
            }
            if(coordinates.y <= twoDArray.GetLength(0) - 2)
            {
                Down = true;
                Coordinates down = new Coordinates();
                down.y = coordinates.y + 1;
                down.x = coordinates.x;
                adjacentCoordinates.Add(Direction.Down, down);
            }
            if(fourOrEight == 8)
            {         
                if (Left && Up)
                {
                    Coordinates topLeft = new Coordinates();
                    topLeft.y = coordinates.y - 1;
                    topLeft.x = coordinates.x - 1;
                    adjacentCoordinates.Add(Direction.TopLeft, topLeft);
                }
                if (Right && Up)
                {
                    Coordinates topRight = new Coordinates();
                    topRight.y = coordinates.y - 1;
                    topRight.x = coordinates.x + 1;
                    adjacentCoordinates.Add(Direction.TopRight, topRight);
                }
                if (Left && Down)
                {
                    Coordinates bottomLeft = new Coordinates();
                    bottomLeft.y = coordinates.y + 1;
                    bottomLeft.x = coordinates.x - 1;
                    adjacentCoordinates.Add(Direction.BottomLeft, bottomLeft);
                }
                if (Right && Down)
                {
                    Coordinates bottomRight = new Coordinates();
                    bottomRight.y = coordinates.y + 1;
                    bottomRight.x = coordinates.x + 1;
                    adjacentCoordinates.Add(Direction.BottomRight, bottomRight);
                }
            }    
            return adjacentCoordinates;
        }
        public int GetDistanceOfTwoMaps(Map sourceMap, Map destinationMap)
        {
            Dictionary<Map, int> depths = new Dictionary<Map, int>();
            Queue<Map> queue = new Queue<Map>();
            HashSet<Map> visited = new HashSet<Map>();
            depths.Add(sourceMap, 0); queue.Enqueue(sourceMap); visited.Add(sourceMap);
            while (queue.Count > 0)
            {
                Map map = queue.Dequeue();
                foreach(var item in map.mapInformations.mapLayout.doors.Values)
                {
                    Map adjacentMap = item.destinationMap;
                    if(visited.Contains(adjacentMap))
                    {
                        if (depths[map] + 1 < depths[adjacentMap]) { depths[adjacentMap] = depths[map] + 1; queue.Enqueue(adjacentMap); } 
                    }
                    else
                    {
                        depths.Add(adjacentMap, depths[map] + 1); queue.Enqueue(adjacentMap); visited.Add(adjacentMap);
                    }
                }
            }
            return depths[destinationMap];
        }
    }
}