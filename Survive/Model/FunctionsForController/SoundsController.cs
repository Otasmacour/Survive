using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Survive
{
    class SoundsController
    {
        public string soundsFolderPath;
        Sound heartBeat;
        long heartBeatDuration;
        Stopwatch stopwatchHeartBeat = new Stopwatch();
        public Queue<(Map map, Sound sound)> soundsToPLay = new Queue<(Map map, Sound sound)>();
        public HashSet<(Map map, Sound sound)> unHeardByAMonster = new HashSet<(Map map, Sound sound)>();
        public SoundsController(string mainFolderPath) 
        {
            this.soundsFolderPath = mainFolderPath+@"Sounds\";
            HeartBeatInitialization();
        }
        public void PlaySounds(int distanceOfMonster)
        {
            HeartBeat(distanceOfMonster);
            while(soundsToPLay.Count > 0)
            {
                var item = soundsToPLay.Dequeue();
                unHeardByAMonster.Add(item);
                item.sound.outputDevice.Play();
            }
        }
        void HeartBeatInitialization()
        {
            stopwatchHeartBeat.Start();
            heartBeat = new Sound(soundsFolderPath, "HeartBeat0");
            heartBeat.outputDevice.Init(heartBeat.audioFileReader);
            heartBeatDuration = (long)heartBeat.audioFileReader.TotalTime.TotalMilliseconds;
        }
        void HeartBeat(int distanceOfMonster)
        {
            if (stopwatchHeartBeat.ElapsedMilliseconds < heartBeatDuration || distanceOfMonster > 3)
            {
                return;
            }
            PerformHeartBeat(distanceOfMonster);
            heartBeatDuration = (long)heartBeat.audioFileReader.TotalTime.TotalMilliseconds;
            stopwatchHeartBeat.Restart();
        }
        void PerformHeartBeat(int distanceOfMonster)
        {
            heartBeat = new Sound(soundsFolderPath, "HeartBeat"+distanceOfMonster.ToString());
            //heartBeat = new Sound(soundsFolderPath, "HeartBeat0");
            var volumeEffect = new VolumeSampleProvider(heartBeat.audioFileReader.ToSampleProvider());
            volumeEffect.Volume = 4.0f - distanceOfMonster*0.7f;
            heartBeat.outputDevice.Init(volumeEffect);
            heartBeat.outputDevice.Play();
        }
    }
}