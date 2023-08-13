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
    }
}
