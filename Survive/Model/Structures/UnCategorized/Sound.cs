using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Sound
    {
        string fileName;
        public int noiceLevel;
        public Sound(string soundFileName, int noiseLevel)
        {
            this.fileName = soundFileName;
            this.noiceLevel = noiseLevel;
        }
    }
}
