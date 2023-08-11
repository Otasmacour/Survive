using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class CollisionController
    {
        Characters characters;
        Player player;
        Monster monster;
        MapHelper mapHelper;
        public CollisionController(Characters characters, MapHelper mapHelper)
        {
            this.characters = characters;
            this.player = characters.player;
            this.monster = characters.monster;
            this.mapHelper = mapHelper;
        }

        public void MonsterAndPlayerCollision()
        {
            //Console.WriteLine("Monster's coordinates: "+ mapHelper.parsing.CoordinatesToTupple(monster.coordinates));
            //Console.WriteLine("Monster's adjacent coordinates:");
            //foreach(var adjacentCoordinates in mapHelper.returnFunctions.AdjacentCoordinates(monster.mapWhereIsLocated.twoDArray, monster.coordinates, 8))
            //{
            //    Console.WriteLine(adjacentCoordinates.Key.ToString() + " " + mapHelper.parsing.CoordinatesToTupple(adjacentCoordinates.Value));
            //}
            var adjacentCoordinatesOfMonster = mapHelper.returnFunctions.AdjacentCoordinates(monster.mapWhereIsLocated.twoDArray, monster.coordinates, 8);
            foreach(Coordinates adjacentCoordinates in adjacentCoordinatesOfMonster.Values)
            {
                if(mapHelper.parsing.CoordinatesToTupple(adjacentCoordinates) == mapHelper.parsing.CoordinatesToTupple(player.coordinates)) // The player is within range of the monster
                {
                    if(player.visible) //The player could be hidden in a hideout
                    {
                        player.Die();
                    }
                    //Console.WriteLine(mapHelper.parsing.CoordinatesToTupple(adjacentCoordinates));
                    Console.WriteLine("He would die");
                }
            }
        }
    }
}
