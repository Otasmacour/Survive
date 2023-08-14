using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Item : GameObject
    {
        public abstract string itemName { get; }
        public abstract bool takesUpSpaceInTheInventory { get; }
        public abstract int noiseLevel { get; }
        public abstract void PickUp(Character character);
        public abstract void Drop(Character character);
        public abstract void Use(Character character);
        public void pickUp(Character character)
        {
            if (this.takesUpSpaceInTheInventory) { character.inventory.items.Add(this); }
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Remove(this);
            character.mapWhereIsLocated.mapInformations.itemsOnMap.Remove(this);
            character.inventory.InventoryUpdate();
        }
        public void drop(Character character)
        {
            character.inventory.items.Remove(this);
            character.inventory.currentlyHeldItem = null;
            character.mapWhereIsLocated.twoDArray[character.coordinates.y, character.coordinates.x].Add(this);
            character.mapWhereIsLocated.mapInformations.itemsOnMap.Add(this);
            character.inventory.InventoryUpdate();
        }
    }
}