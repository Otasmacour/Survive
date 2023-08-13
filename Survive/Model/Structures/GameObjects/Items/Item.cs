﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Item : GameObject
    {
        public abstract int noiseScale { get; }
        public abstract void PickUp(Character character);
        public abstract void Drop(Character character);
        public abstract void Use(Character character);
        void pickUp(Character character)
        {
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Remove(this);
            character.mapWhereIsLocated.mapInformations.itemsOnMap.Remove(this);
        }
        void drop(Character character)
        {
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Add(this);
            character.mapWhereIsLocated.mapInformations.itemsOnMap.Add(this);
        }
    }
}