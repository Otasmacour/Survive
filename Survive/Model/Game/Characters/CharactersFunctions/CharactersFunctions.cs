using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class CharactersFunctions
    {
        Characters characters;
        public CharactersFunctions(Characters characters)
        {
            this.characters = characters;
        }
        public void CharactersInitializations()
        {
            NullCharacter character = new NullCharacter();
            character.name = "Tyler";
            character.symbol = 'T';
            characters.player = new Player(character);
            characters.monster = new Monster();
        }
    }
}