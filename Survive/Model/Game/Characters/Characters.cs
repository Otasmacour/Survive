using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Characters
    {
        public Player player;
        public Monster monster;
        public int t;
        public CharactersFunctions charactersFunctions;
        public Characters(Informations info)
        {
            charactersFunctions = new CharactersFunctions(this, info);
            charactersFunctions.CharactersInitializations(); //this is not complete, return and complete that when other characters than player will be needed
        }
    }
}
