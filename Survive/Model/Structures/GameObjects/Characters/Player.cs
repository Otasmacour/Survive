﻿using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class Player : Character
    {
        public SoundsController soundsController;
        public bool visible = true;
        public MapHelper mapHelper;
        Monster monster;
        public Player(Informations info, Monster monster)
        {
            this.info = info;
            this.monster = monster;
            name = "Tyler";
            inventory = new Inventory(1);
        }
        public void VisibilityUpdate(Map mapToWhere, Map mapFromWhere, Coordinates newCoordinates, MapHelper mapHelper)
        {
            if(CanGoThere(mapToWhere.twoDArray, newCoordinates)) //When this wasn't here, player could walk to wall and lose his invisibility
            {
                if (mapHelper.boolFunctions.HideoutThere(mapToWhere.twoDArray, newCoordinates))
                {
                    if (mapWhereIsLocated != monster.mapWhereIsLocated)
                    {
                        this.visible = false;
                    }
                    else if(mapFromWhere != mapToWhere)
                    {
                        this.visible = false;
                    }
                    else { this.visible = true; }
                }
                else { this.visible = true; }
            }
        }
        public override bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Door && mapHelper.boolFunctions.MonsterSeesThePlayer())// In this case, the player goes through the door, but the monster is in the same room, so it starts chasing the player into the room where the door leads
                {
                    monster.monsterChasingInformation.whereThePlayerHasGone = coordinates;
                }
                if (obj is Wall)
                {
                    return false;
                }
                if(obj is Furniture)
                {
                    Furniture furniture = (Furniture)obj;
                    if(furniture.canHideThere == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public Sound GetSound(string soundFileName)
        {
            Sound sound = new Sound(soundsController.soundsFolderPath, soundFileName);
            sound.outputDevice.Init(sound.audioFileReader);
            var volumeEffect = new VolumeSampleProvider(sound.audioFileReader.ToSampleProvider());
            sound.outputDevice.Init(volumeEffect);
            sound.noiceLevel = 1;
            return sound;
        }
        public override void Die(string theWayHowPlayerDied, Alerts alerts)
        {
            info.win = false;
            info.theWayThePlayerDied = theWayHowPlayerDied;
            info.run = false;
            soundsController.soundsToPLay.Enqueue((mapWhereIsLocated, GetSound("GameOVer")));
        }
        public override char GetSymbol(Map map)
        {
            return this.name[0];
        }
        public override int GetPriorityNumber()
        {
            return 31;
        }
    }
}
