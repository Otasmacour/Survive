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
        public List<Character> characters;
        public CharactersFunctions charactersFunctions;
        public Characters()
        {
            charactersFunctions = new CharactersFunctions(this);
            charactersFunctions.CharactersInitializations(); //this is not complete, return and complete that when other characters than player will be needed
        }
    }
}
