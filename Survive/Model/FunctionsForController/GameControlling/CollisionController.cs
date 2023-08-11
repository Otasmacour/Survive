using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class CollisionController
    {
        Characters characters;
        Player player;
        Monster monster;
        public CollisionController(Characters characters, Player player, Monster monster)
        {
            this.characters = characters;
            this.player = player;
            this.monster = monster;
        }

        public void CollisionCheck()
        {
            
        }
    }
}
