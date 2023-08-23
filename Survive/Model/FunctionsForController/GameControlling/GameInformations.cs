using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class GameInformations
    {
        MapHelper mapHelper;
        Characters characters;
        public GameInformations(MapHelper mapHelper, Characters characters)
        {
            this.mapHelper = mapHelper;
            this.characters = characters;
        }
        public int GetMonsterDistance()
        {
            return mapHelper.returnFunctions.GetDistanceOfTwoMaps(characters.player.mapWhereIsLocated, characters.monster.mapWhereIsLocated);
        }
        public List<Item> GetItemsWithinPlayersReach()
        {
            List<Item> items = new List<Item>();
            var adjacentCoordinates = mapHelper.returnFunctions.GetAdjacentCoordinates(characters.player.mapWhereIsLocated.twoDArray, characters.player.coordinates, 8);
            foreach ( var item in adjacentCoordinates )//Items around player
            {
                if(mapHelper.boolFunctions.ItemThere(characters.player.mapWhereIsLocated.twoDArray, item.Value))
                {
                    items.Add(mapHelper.returnFunctions.GetItemThere(characters.player.mapWhereIsLocated, item.Value));
                }
            }
            //Item in the same place where the player is standing
            if(mapHelper.boolFunctions.ItemThere(characters.player.mapWhereIsLocated.twoDArray, characters.player.coordinates))
            {
                items.Add(mapHelper.returnFunctions.GetItemThere(characters.player.mapWhereIsLocated, characters.player.coordinates));
            }
            return items;
        }
    }
}