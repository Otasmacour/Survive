using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class PlayerItemManipulation
    {
        Player player;
        Inventory inventory;
        public PlayerItemManipulation(Player player)
        {
            this.inventory = player.inventory;
            this.player = player;
        }
        public void DropItem(Player player)
        {
            //if(player.item != null)
            //{
            //    itemManipulation.DropItem(player, player.item);
            //}
        }
        public void PickUpItem(Player player)
        {
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
