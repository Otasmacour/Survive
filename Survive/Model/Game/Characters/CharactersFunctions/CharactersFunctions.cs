﻿using System;
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
        Informations info;
        public CharactersFunctions(Characters characters, Informations info)
        {
            this.characters = characters;
            this.info = info;
        }
        public void CharactersInitializations()
        {
            characters.monster = new Monster(info);
            NullCharacter character = new NullCharacter();
            characters.player = new Player(info, characters.monster); 
        }
        public void Constructor(MapHelper mapHelper)
        {
            characters.player.mapHelper = mapHelper;
        }
    }
}