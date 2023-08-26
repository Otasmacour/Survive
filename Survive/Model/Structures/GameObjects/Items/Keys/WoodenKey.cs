using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WoodenKey : Key
    {
        public override string itemName => "Wooden key";
        public WoodenKey(SoundsController soundController) : base(soundController) 
        {

        }
        public override char GetSymbol(Map map)
        {
            return 'W';
        }
        public override void Use(Character character, MapHelper mapHelper, Alerts alerts)
        {
            if(mapHelper.boolFunctions.ChestThere(character.mapWhereIsLocated.twoDArray, character.coordinates) == false)
            {
                return;
            }
            Chest chest = mapHelper.returnFunctions.GetChestThere(character.mapWhereIsLocated, character.coordinates);
            {
                if(chest is WoodenChest)
                {
                    chest.Unlock(character.mapWhereIsLocated, character.coordinates);
                }
                else
                {
                    alerts.Add("Wrong key");
                }
            }
        }
    }
}
