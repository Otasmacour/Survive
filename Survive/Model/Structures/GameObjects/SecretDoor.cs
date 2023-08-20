using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class SecretDoor : GameObject
    {
        public Map destinationMap;
        public Coordinates transitionPointCoordinates;
        public override char symbol => 's';
        public override int GetPriorityNumber()
        {
            return 90;
        }    
    }
}