using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Game
    {
        public Informations info { get; set; } = new Informations();
        public Characters characters { get; set; }
        public Maps maps { get; set; }
        public Game(DataIOManager dataIOManager) 
        {
            this.characters = new Characters(info);
            this.maps = new Maps(this.characters, dataIOManager);
            this.characters.charactersFunctions.Constructor(this.maps.mapsFunctions.mapHelper);
        }
    }
}
