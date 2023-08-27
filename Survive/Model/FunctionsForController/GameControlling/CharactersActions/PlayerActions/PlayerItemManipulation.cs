using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Survive
{
    class PlayerItemManipulation
    {
        Player player;
        Inventory inventory;
        MapHelper mapHelper;
        Monster monster;
        MonsterActions monsterActions;
        Alerts alerts;
        public PlayerItemManipulation(Player player, MapHelper mapHelper, Monster monster, MonsterActions monsterActions, Alerts alerts)
        {
            this.player = player;
            this.inventory = player.inventory;
            this.mapHelper = mapHelper;
            this.monster = monster;
            this.monsterActions = monsterActions;
            this.alerts = alerts;
            this.alerts = alerts;
        }
        public void DropItem(Player player)
        {
            if (inventory.currentlyHeldItem != null && mapHelper.boolFunctions.JustFloorAndCharacterThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
            {
                inventory.currentlyHeldItem.Drop(player);
            }
            else
            {
                if(inventory.currentlyHeldItem == null) { alerts.Add("You have no item to drop"); }
                else { alerts.Add("You cannot drop item here"); }
            }
        }
        public void PickUpItem(Player player)
        {
            if(mapHelper.boolFunctions.ItemThere(player.mapWhereIsLocated.twoDArray, player.coordinates) == false) { return; }
            Item item = mapHelper.returnFunctions.GetItemThere(player.mapWhereIsLocated, player.coordinates);
            if (inventory.items.Count < inventory.inventorySize || item.takesUpSpaceInTheInventory == false)
            {
                item.PickUp(player);
            }
            else 
            {
                item.DropThenPickUp(player, player.inventory.currentlyHeldItem, item);
            }
        }
        public void UseItem(Player player)
        {
            if(player.inventory.currentlyHeldItem != null)
            {
                player.inventory.currentlyHeldItem.Use(player, mapHelper, alerts);
            }
        }
        public void SwitchItem(Player player)
        {
            if(player.inventory.items.Count >= 2)
            {
                if(player.inventory.items.IndexOf(player.inventory.currentlyHeldItem) == player.inventory.items.Count - 1) //Then the currentlyHeldItem is the last item of that list.
                {
                    player.inventory.currentlyHeldItem = player.inventory.items[0]; //There is no other item after the currentlyHeldItem item in that list, so the new currentlyHeldItem item is set from the beginning.
                }
                else //CurrentlyHeldItem is not the last item of that list, so the next item after current currentlyHeldItem in that list, can be set as new currentlyHeldItem.
                {
                    player.inventory.currentlyHeldItem = player.inventory.items[player.inventory.items.IndexOf(player.inventory.currentlyHeldItem) + 1];
                }
            }
        }
    }
}