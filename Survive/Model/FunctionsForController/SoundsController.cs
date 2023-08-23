﻿using System;
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
        public string soundsFolderPath;
        Sound heartBeat;
        public Queue<(Map map, Sound sound)> soundsToPLay = new Queue<(Map map, Sound sound)>();
        public HashSet<(Map map, Sound sound)> unHeardByAMonster = new HashSet<(Map map, Sound sound)>();
        public SoundsController(string mainFolderPath) 
        {
            this.soundsFolderPath = mainFolderPath+@"Sounds\";
            SoundsInitialization();
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
        void SoundsInitialization()
        {
            heartBeat = new Sound(soundsFolderPath, "HeartBeat");
            SoundInitialization(heartBeat, 4);
        }
        void SoundInitialization(Sound audio, float volume)
        {
            audio.outputDevice.Init(audio.audioFileReader);
            var volumeEffect = new VolumeSampleProvider(audio.audioFileReader.ToSampleProvider());
            volumeEffect.Volume = volume;
            audio.outputDevice.Init(volumeEffect);
        }
        void HeartBeat(int distanceOfMonster)
        {
            heartBeat.outputDevice.Play();
        }
    }
}