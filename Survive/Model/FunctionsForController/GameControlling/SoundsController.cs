using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Survive
{
    class SoundsController
    {
        string mainFolderPath;
        Audio heartBeat;
        public List<Item> list = new List<Item>();
        public SoundsController(string mainFolderPath) 
        {
            this.mainFolderPath = mainFolderPath+@"Sounds\";
            SoundsInitialization();
        }
        public void PlaySounds(int distanceOfMonster)
        {
            HeartBeat(distanceOfMonster);
        }
        void SoundsInitialization()
        {
            heartBeat = new Audio(mainFolderPath, "HeartBeat");
            SoundInitialization(heartBeat, 4);
        }
        void SoundInitialization(Audio audio, float volume)
        {
            audio.outputDevice.Init(audio.audioFileReader);
            var volumeEffect = new VolumeSampleProvider(audio.audioFileReader.ToSampleProvider());
            volumeEffect.Volume = volume;
            audio.outputDevice.Init(volumeEffect);
        }
        void SoundPlay()
        {

        }
        void HeartBeat(int distanceOfMonster)
        {
            heartBeat.outputDevice.Play();
        }
    }
}