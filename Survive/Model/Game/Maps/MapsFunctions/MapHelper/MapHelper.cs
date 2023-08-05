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
        public BoolFunctions boolFunctions = new BoolFunctions();
        public Parsing parsing = new Parsing();
        public ReturnFunctions returnFunctions;
        public MapHelper()
        {
            this.returnFunctions = new ReturnFunctions(boolFunctions);
            this.twoDArrayFunctions = new TwoDArrayFunctions(this.parsing, this.returnFunctions, this.boolFunctions);
        }
    }
}