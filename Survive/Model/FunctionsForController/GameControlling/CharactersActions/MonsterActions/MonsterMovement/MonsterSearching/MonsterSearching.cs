using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class MonsterSearching
    {
        Movement movement;
        Monster monster;
        MonsterSearchingInformation monsterSearchingInformation;
        Player player;
        MapHelper mapHelper;
        public MonsterSearching(Movement movement, Monster monster, MapHelper mapHelper, Player player)
        {
            this.movement = movement;
            this.monster = monster;
            this.mapHelper = mapHelper;
            this.monsterSearchingInformation = monster.monsterSearchingInformation;
            this.player = player;
        }
        public void Search()
        {
            if (monsterSearchingInformation.searchingRoom)
            {
                SearchTheRoom();
            }
            else if (monsterSearchingInformation.searchingRooms)
            {
                SearchRooms();
            }
            else
            {
                //The monster was chasing the player and now can't see him
                //Here should also be some chance, that if the room contains a hideout, the monster will search it
                if (monster.mapWhereIsLocated.mapInformations.mapLayout.doors.Count == 1)
                {
                    monsterSearchingInformation.searchingRoom = true;
                    SearchTheRoom();
                }
                else
                {
                    monsterSearchingInformation.searchingRooms = true;
                    SearchRooms();
                }
            }
        }
        void SearchTheRoom()
        {
            Console.WriteLine("Searching the room, but not implemented yet");
            Thread.Sleep(2000);
        }
        void SearchRooms()
        {
            if (monsterSearchingInformation.count > 10)
            {
                //The monster got tired of it
                monsterSearchingInformation.EndingOfSearching();
                return;
            }
            if (monsterSearchingInformation.whereToSearch == null)
            {
                WhereToSearchAssigment();
            }
            PerformSearchingRoomsMove();
        }
        void WhereToSearchAssigment()
        {
            monsterSearchingInformation.count++;
            monsterSearchingInformation.whereToSearch = DecideWhereToSearch();
            monsterSearchingInformation.visitedRooms.Contains(mapHelper.returnFunctions.GetDoorThere(monster.mapWhereIsLocated, monsterSearchingInformation.whereToSearch).destinationMap);
        }
        void PerformSearchingRoomsMove()
        {
            bool lastTime = false;
            if (mapHelper.boolFunctions.AreTheCoordinatesAdjacent(monster.mapWhereIsLocated.twoDArray, monster.coordinates, monsterSearchingInformation.whereToSearch))
            {
                lastTime = true;
            }
            Direction directionToGo = mapHelper.twoDArrayFunctions.GetDirectionWhileWalkingOnTwoDArray(monsterSearchingInformation.whereToSearch, monster.coordinates, monster.mapWhereIsLocated.twoDArray);
            movement.MoveCharacter(monster, directionToGo);
            if (lastTime) { monsterSearchingInformation.whereToSearch = null; }
        }
        Coordinates DecideWhereToSearch()
        {
            Coordinates result = new Coordinates();
            if (monster.mapWhereIsLocated.mapInformations.mapLayout.doorCoordinates.Count == 1)
            {
                return monster.mapWhereIsLocated.mapInformations.mapLayout.doorCoordinates.First().Value;
            }
            else
            {
                Dictionary<(int, int), int> depths = mapHelper.twoDArrayFunctions.TwoDArrayBFS(monster.mapWhereIsLocated.twoDArray, monster.coordinates);
                List<Coordinates> doorsCoordinates = new List<Coordinates>(monster.mapWhereIsLocated.mapInformations.mapLayout.doorCoordinates.Values);
                //Removing the nearest door (the one room the monster just came from)
                Coordinates nearestDoor = doorsCoordinates[0];
                foreach(Coordinates coordinates in doorsCoordinates)
                {
                    if (depths[mapHelper.parsing.CoordinatesToTupple(coordinates)] < depths[mapHelper.parsing.CoordinatesToTupple(nearestDoor)])
                    {
                        nearestDoor = coordinates;
                    }
                }
                doorsCoordinates.Remove(nearestDoor);
                monsterSearchingInformation.visitedRooms.Add(mapHelper.returnFunctions.GetDoorThere(monster.mapWhereIsLocated, nearestDoor).destinationMap);
                // Search for the best option(a room that hasn't been visited yet)
                Coordinates bestOption = doorsCoordinates[0];
                foreach (Coordinates coordinates in doorsCoordinates)
                {
                    Door door = mapHelper.returnFunctions.GetDoorThere(monster.mapWhereIsLocated, coordinates);
                    if(monsterSearchingInformation.visitedRooms.Contains(door.destinationMap) == false)
                    {
                        bestOption = coordinates;
                    }

                }
                result = bestOption;
            }
            return result;
        }
    }
}