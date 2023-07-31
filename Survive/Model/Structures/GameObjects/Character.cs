using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Character : GameObject
    {
        public string name;
        public Map mapWhereIsLocated;
        public Coordinates coordinates;
        public bool living = true;
        public void RoleConstructor(Character character)
        {
            this.symbol = character.symbol; //This is about Character as a gameobject
            this.name = character.name;
        }
    }
}