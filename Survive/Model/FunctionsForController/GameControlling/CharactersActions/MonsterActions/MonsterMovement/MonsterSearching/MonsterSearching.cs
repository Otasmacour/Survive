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
        Random random = new Random();
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
                    if (monster.mapWhereIsLocated.mapInformations.mapLayout.furnitureCoordinates.Count == 0) { monsterSearchingInformation.EndingOfSearching(); return; } //The player could hide in such a way that the monster doesn't know about it (not in a hiding place in the form of furniture), in which case the search would be over.
                    monsterSearchingInformation.searchingRoom = true;
                    monsterSearchingInformation.furnitureToSearch = new Queue<Coordinates>(monster.mapWhereIsLocated.mapInformations.mapLayout.furnitureCoordinates);
                    SearchTheRoom();
                }
                else
                {

                    if (FiftyFifTychance(random)) { monsterSearchingInformation.searchingRooms = true; SearchRooms(); }
                    else { monsterSearchingInformation.searchingRoom = true; monsterSearchingInformation.firstRoomThenRooms = true; monsterSearchingInformation.furnitureToSearch = new Queue<Coordinates>(monster.mapWhereIsLocated.mapInformations.mapLayout.furnitureCoordinates); SearchTheRoom(); }
                }
            }

        }
        void SearchTheRoom()
        {
            if (monsterSearchingInformation.CurrentFurnitureToSearch == null)
            {
                if (monsterSearchingInformation.furnitureToSearch.Count == 0)  //The player could somehow leave the room without the monster noticing and triggering the chase mode.
                {
                    if (monsterSearchingInformation.firstRoomThenRooms)
                    {
                        monsterSearchingInformation.EndOfSearchingRoomStartToSearchingRooms(); return;
                    }
                    else { monsterSearchingInformation.EndingOfSearching(); return; }
                }
                else
                {
                    monsterSearchingInformation.CurrentFurnitureToSearch = monsterSearchingInformation.furnitureToSearch.Dequeue();
                }
            }
            PerformSearchingRoom();
        }
        void PerformSearchingRoom()
        {
            bool lastTime = false;
            if (mapHelper.boolFunctions.AreTheCoordinatesAdjacent(monster.mapWhereIsLocated.twoDArray, monster.coordinates, monsterSearchingInformation.CurrentFurnitureToSearch))
            {
                lastTime = true;
            }
            Direction directionToGo = mapHelper.twoDArrayFunctions.GetDirectionWhileWalkingOnTwoDArray(monsterSearchingInformation.CurrentFurnitureToSearch, monster.coordinates, monster.mapWhereIsLocated.twoDArray);
            if (lastTime)
            {

                LookForPlayer();
                monsterSearchingInformation.CurrentFurnitureToSearch = null;

            }
            else { movement.MoveCharacter(monster, directionToGo); }
        }
        void LookForPlayer()
        {
            if (player.mapWhereIsLocated == monster.mapWhereIsLocated && mapHelper.parsing.CoordinatesToTupple(monsterSearchingInformation.CurrentFurnitureToSearch) == mapHelper.parsing.CoordinatesToTupple(player.coordinates))
            {
                player.Die("the monster found you in the hideout");
            }
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
            PerformSearchingRooms();
        }
        void WhereToSearchAssigment()
        {
            monsterSearchingInformation.count++;
            monsterSearchingInformation.whereToSearch = DecideWhereToSearch();
            monsterSearchingInformation.visitedRooms.Contains(mapHelper.returnFunctions.GetDoorThere(monster.mapWhereIsLocated, monsterSearchingInformation.whereToSearch).destinationMap);
        }
        void PerformSearchingRooms()
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
                foreach (Coordinates coordinates in doorsCoordinates)
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
                    if (monsterSearchingInformation.visitedRooms.Contains(door.destinationMap) == false)
                    {
                        return coordinates;
                    }
                }
                //if it got this far, there is no unvisited map, then it may be random.
                return doorsCoordinates[random.Next(doorsCoordinates.Count)];
            }
        }
        static bool FiftyFifTychance(Random random)
        {
            int number = random.Next(0, 2);
            if (number == 0) { return false; }
            return true;
        }

    }
}