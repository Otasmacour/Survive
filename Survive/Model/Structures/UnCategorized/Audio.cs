using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Survive
{
    class Audio
    {
        public AudioFileReader audioFileReader;
        public WaveOutEvent outputDevice;
        public Audio(string mainFolderPath, string fileName)
        {
            audioFileReader = new AudioFileReader(mainFolderPath+fileName+".mp3");
            outputDevice = new WaveOutEvent();
        }
    }
}
