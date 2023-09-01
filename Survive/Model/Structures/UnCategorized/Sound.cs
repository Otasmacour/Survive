using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Sound
    {
        public AudioFileReader audioFileReader;
        public WaveOutEvent outputDevice;
        public int noiceLevel;
        public Sound(string soundsFolderPath, string fileName)
        {
            audioFileReader = new AudioFileReader(soundsFolderPath + fileName+".mp3");
            outputDevice = new WaveOutEvent();
        }
    }
}
