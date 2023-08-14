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
        Monster monster;
        MonsterActions monsterActions;
        public PlayerItemManipulation(Player player, MapHelper mapHelper, Monster monster, MonsterActions monsterActions)
        {
            this.player = player;
            this.inventory = player.inventory;
            this.mapHelper = mapHelper;
            this.monster = monster;
            this.monsterActions = monsterActions;
        }
        public void DropItem(Player player)
        {
            if (inventory.currentlyHeldItem != null && mapHelper.boolFunctions.JustFloorAndCharacterThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
            {
                Console.WriteLine("Level of that noise was: " + inventory.currentlyHeldItem.noiseLevel.ToString());
                Thread.Sleep(1000);
                if(inventory.currentlyHeldItem.noiseLevel >= mapHelper.returnFunctions.GetDistanceOfTwoMaps(player.mapWhereIsLocated, monster.mapWhereIsLocated))//That noise level is high enough for monster to hear it
                {
                    if(mapHelper.boolFunctions.MonsterSeesThePlayer() == false) //If monster can see player, it won't pay attention to dropped items
                    {
                        monsterActions.monsterMovement.monsterWalking.whereTheMonsterShouldGoForAWalk(player.mapWhereIsLocated);
                        Console.WriteLine("The monster heard that");
                        Thread.Sleep(1000);
                    }
                }
                inventory.currentlyHeldItem.Drop(player);
            }
            else
            {
                Console.WriteLine("You have no item to DROP");
                Thread.Sleep(1000);
            }
        }
        public void PickUpItem(Player player)
        {
            if(inventory.items.Count < inventory.inventorySize && mapHelper.boolFunctions.ItemThere(player.mapWhereIsLocated.twoDArray, player.coordinates))
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
                player.inventory.currentlyHeldItem.Use(player);
            }
            else
            {
                Console.WriteLine("You don't hold any item to USE");
                Thread.Sleep(1000);
            }
        }
    }
}