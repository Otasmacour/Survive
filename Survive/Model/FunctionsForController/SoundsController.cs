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
        MapHelper mapHelper;
        Characters characters;
        public string soundsFolderPath;
        Sound heartBeat;
        long heartBeatDuration;
        int pauseBetweenHeartbeats = 1200;
        Stopwatch stopwatchHeartBeat = new Stopwatch();
        public Queue<(Map map, Sound sound)> soundsToPLay = new Queue<(Map map, Sound sound)>();
        public HashSet<(Map map, Sound sound)> unHeardByAMonster = new HashSet<(Map map, Sound sound)>();
        public SoundsController(string mainFolderPath, MapHelper mapHelper, Characters characters) 
        {
            this.mapHelper = mapHelper;
            this.characters = characters;
            this.soundsFolderPath = mainFolderPath+@"Sounds\";
            HeartBeatInitialization();
        }
        public void PlaySounds()
        {
            if (characters.monster.living) { HeartBeat(); }
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
            heartBeat = new Sound(soundsFolderPath, "HeartBeat");
            heartBeat.outputDevice.Init(heartBeat.audioFileReader);
            heartBeatDuration = (long)heartBeat.audioFileReader.TotalTime.TotalMilliseconds;
        }
        void HeartBeat()
        {
            if (stopwatchHeartBeat.ElapsedMilliseconds < heartBeatDuration + pauseBetweenHeartbeats || mapHelper.returnFunctions.GetDistanceOfTwoMaps(characters.monster.mapWhereIsLocated, characters.player.mapWhereIsLocated) > 3) 
            {
                return;
            }
            PerformHeartBeat();
            stopwatchHeartBeat.Restart();
        }
        void PerformHeartBeat()
        {
            int distanceByFields = mapHelper.twoDArrayFunctions.DistanceOfMonster(characters.monster, characters.player);
            heartBeat = new Sound(soundsFolderPath, "HeartBeat");
            var volumeEffect = new VolumeSampleProvider(heartBeat.audioFileReader.ToSampleProvider());
            volumeEffect.Volume = 4.0f - 0.1f*distanceByFields;
            heartBeat.outputDevice.Init(volumeEffect);
            heartBeat.outputDevice.Play();
            pauseBetweenHeartbeats = 30 * distanceByFields;
        }
    }
}