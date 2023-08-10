using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapHelper
    {
        public TwoDArrayFunctions twoDArrayFunctions;
        public BoolFunctions boolFunctions;
        public Parsing parsing = new Parsing();
        public ReturnFunctions returnFunctions;
        public MapHelper(Characters characters, DataIOManager dataIOManager)
        {
            this.boolFunctions = new BoolFunctions(characters.monster, characters.player);
            this.returnFunctions = new ReturnFunctions(boolFunctions);
            this.twoDArrayFunctions = new TwoDArrayFunctions(this.parsing, this.returnFunctions, this.boolFunctions, dataIOManager);
        }
    }
}