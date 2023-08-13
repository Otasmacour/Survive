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
            if(inventory.currentlyHeldItem != null && mapHelper.boolFunctions.JustFloorThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
            {
                inventory.currentlyHeldItem.Drop();
            }
            else
            {
                Console.WriteLine("You have no item to drop");
                Thread.Sleep(1000);
            }
        }
        public void PickUpItem(Player player)
        {
            if(inventory.list.Count < inventory.inventorySize)
            {
                Console.WriteLine("you have enough space in your inventory");
                Thread.Sleep(1000);
                //if(pl)
            }
            //itemManipulation.PickUpItem(player);
        }
        public void UseItem(Player player)
        {
            //if(player.item != null)
            //{
            //    itemManipulation.UseItem(player, player.item);
            //}
        }
    }
}
