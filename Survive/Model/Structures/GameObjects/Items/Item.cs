﻿using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Item : GameObject
    {
        public SoundsController soundsController;
        public bool visible = true; //Some items could be hidden and if this property "visible" was not present, the GetItemsWithinPlayersReach method would return all items regardless of whether they are visible or not.
        public abstract string getItemName();
        public abstract bool takesUpSpaceInTheInventory { get; }
        public abstract int noiseLevel { get; }
        public abstract string useSoundFileName { get; }
        public abstract string dropSoundFileName { get; }
        public Item(SoundsController soundsController){ }
        public abstract void PickUp(Character character);
        public abstract void Drop(Character character);
        public abstract void Use(Character character, MapHelper mapHelper, Alerts alerts);
        public Sound GetSound(string soundFileName)
        {
            Sound sound = new Sound(soundsController.soundsFolderPath, soundFileName);
            sound.outputDevice.Init(sound.audioFileReader);
            var volumeEffect = new VolumeSampleProvider(sound.audioFileReader.ToSampleProvider());
            sound.outputDevice.Init(volumeEffect);
            sound.noiceLevel = noiseLevel;
            return sound;
        }
        public void pickUp(Character character)
        {
            if (this.takesUpSpaceInTheInventory) { character.inventory.items.Add(this); }
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Remove(this);
            character.inventory.InventoryUpdate();
        }
        public void drop(Character character)
        {
            character.inventory.items.Remove(this);
            character.inventory.currentlyHeldItem = null;
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Add(this);
            character.inventory.InventoryUpdate();
            this.soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(dropSoundFileName)));
        }
        public void DropThenPickUp(Character character, Item toDrop, Item toPickUp)
        {
            character.inventory.items.Remove(toDrop);
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Add(toDrop);
            toDrop.soundsController.soundsToPLay.Enqueue((character.mapWhereIsLocated, GetSound(toDrop.dropSoundFileName)));
            if (toPickUp.takesUpSpaceInTheInventory) { character.inventory.items.Add(toPickUp); }
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Remove(toPickUp);
            character.inventory.currentlyHeldItem = toPickUp;
        }
    }
}