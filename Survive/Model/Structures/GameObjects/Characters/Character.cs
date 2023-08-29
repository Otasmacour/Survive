using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Character : GameObject
    {
        public Inventory inventory;
        public Informations info;
        public string name;
        public Map mapWhereIsLocated;
        public Coordinates coordinates;
        public bool living = true;
        public abstract bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates);
        public abstract void Die(string theWayHowCharacterDied, Alerts alerts);
        public void die()
        {
            living = false;
            mapWhereIsLocated.mapInformations.charactersOnMap.Remove(this);
            mapWhereIsLocated.twoDArray[coordinates.y,coordinates.x].Remove(this);
        }
    }
}