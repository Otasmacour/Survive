using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Character : GameObject
    {
        public Informations info;
        public string name;
        public Map mapWhereIsLocated;
        public Coordinates coordinates;
        public bool living = true;
        public void RoleConstructor(Character character, Informations info)
        {
            this.symbol = character.symbol; //This is about Character as a gameobject
            this.name = character.name;
            this.info = info;
        }
        public abstract bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates);
        public abstract void Die();
        public void die()
        {
            living = false;
            mapWhereIsLocated.mapInformations.charactersOnMap.Remove(this);
            mapWhereIsLocated.twoDArray[coordinates.y,coordinates.x].Remove(this);
        }
    }
}