using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class SecretDoor : GameObject
    {
        MapHelper mapHelper;
        public SecretDoor(MapHelper mapHelper)
        {
            this.mapHelper = mapHelper;
        }
        public Map destinationMap;
        public Coordinates transitionPointCoordinates;
        Coordinates SecretDoorCoordinates(Map map)
        {
            return map.mapInformations.mapLayout.secretDoorsCoordinates[this];
        }
        public override char GetSymbol(Map map)
        {
            if(map is Tunnel) { return 's'; }
            var adjacentCoordinates = mapHelper.returnFunctions.GetAdjacentCoordinates(map.twoDArray, SecretDoorCoordinates(map), 4);
            foreach(var item in adjacentCoordinates)
            {
                if(mapHelper.boolFunctions.PlayerThere(map.twoDArray, item.Value))
                {
                    return 's';
                }
            }
            return'x';
        }
        public override int GetPriorityNumber()
        {
            return 90;
        }    
    }
}