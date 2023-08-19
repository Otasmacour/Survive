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
        public Dictionary<Sound, Map> sounds = new Dictionary<Sound, Map>();
        public GameInformations(MapHelper mapHelper, Characters characters)
        {
            this.mapHelper = mapHelper;
            this.characters = characters;
        }
        public void SoundsUpdate()
        {
            sounds.Clear();
        }
        public int GetMonsterDistance()
        {
            return mapHelper.returnFunctions.GetDistanceOfTwoMaps(characters.player.mapWhereIsLocated, characters.monster.mapWhereIsLocated);
        }
        public List<Item> GetItemsAroundPlayer()
        {
            List<Item> items = new List<Item>();
            var adjacentCoordinates = mapHelper.returnFunctions.GetAdjacentCoordinates(characters.player.mapWhereIsLocated.twoDArray, characters.player.coordinates, 8);
            foreach ( var item in adjacentCoordinates )
            {
                if(mapHelper.boolFunctions.ItemThere(characters.player.mapWhereIsLocated.twoDArray, item.Value))
                {
                    items.Add(mapHelper.returnFunctions.GetItemThere(characters.player.mapWhereIsLocated, item.Value));
                }
            }
            return items;
        }
    }
}