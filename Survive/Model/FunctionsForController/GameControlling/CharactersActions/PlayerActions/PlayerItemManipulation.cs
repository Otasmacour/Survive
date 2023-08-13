using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Survive
{
    class PlayerItemManipulation
    {
        Player player;
        Inventory inventory;
        MapHelper mapHelper;
        public PlayerItemManipulation(Player player, MapHelper mapHelper)
        {
            this.player = player;
            this.inventory = player.inventory;
            this.mapHelper = mapHelper;
        }
        public void DropItem(Player player)
        {
            if (inventory.currentlyHeldItem != null && mapHelper.boolFunctions.JustFloorThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
            {
                inventory.currentlyHeldItem.Drop();
            }
            else
            {
                Console.WriteLine("You have no item to DROP");
                Thread.Sleep(1000);
            }
        }
        public void PickUpItem(Player player)
        {
            if(inventory.list.Count < inventory.inventorySize && mapHelper.boolFunctions.ItemThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
            {
                Item item = mapHelper.returnFunctions.GetItemThere(player.mapWhereIsLocated, player.coordinates);
                item.PickUp(player);
                Console.WriteLine("you have enough space in your inventory and there is an item to pick up");
                Thread.Sleep(1000);
                
            }
            else { Console.WriteLine("you have no enough space in your inventory or there is not any item to PICK UP"); Thread.Sleep(1000); }
        }
        public void UseItem(Player player)
        {
            if(player.inventory.currentlyHeldItem != null)
            {
                player.inventory.currentlyHeldItem.Use();
            }
            else
            {
                Console.WriteLine("You don't hold any item to USE");
                Thread.Sleep(1000);
            }
        }
    }
}
