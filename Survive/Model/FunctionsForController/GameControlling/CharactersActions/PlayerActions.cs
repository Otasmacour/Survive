using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class PlayerActions
    {
        Characters characters;
        Movement movement;
        ItemManipulation itemManipulation;
        DataIOManager dataIOManager;
        Player player;
        public PlayerActions(Characters characters, Movement movement, ItemManipulation itemManipulation, DataIOManager dataIOManager, Player player)
        {
            this.characters = characters;
            this.movement = movement;
            this.dataIOManager = dataIOManager;
            this.player = player;
            this.itemManipulation = itemManipulation;
        }
        public void Action()
        {
            if(player.living == false)
            {
                return;
            }
            char c = Console.ReadKey().KeyChar;
            UserIntents userIntents = dataIOManager.enumFunctions.GetUserIntents(c);
            switch(userIntents)
            {
                case UserIntents.Move:
                    PlayerMovement(player, movement, dataIOManager.enumFunctions.GetDirectionByChar(c)); return;
                case UserIntents.Drop:
                    PlayerDropItem(player, itemManipulation); return;
                case UserIntents.PickUp:
                    PlayerPickUpItem(player, itemManipulation); return;
                case UserIntents.Use:
                    PlayerUseItem(player, itemManipulation); return;
            }
        }
        static void PlayerMovement(Player player, Movement movement, Direction movementDirection)
        {
            movement.MoveCharacter(player, movementDirection);
        }
        static void PlayerDropItem(Player player, ItemManipulation itemManipulation)
        {
            if(player.item != null)
            {
                itemManipulation.DropItem(player, player.item);
            }
        }
        static void PlayerPickUpItem(Player player, ItemManipulation itemManipulation)
        {
            itemManipulation.PickUpItem(player);
        }
        static void PlayerUseItem(Player player, ItemManipulation itemManipulation)
        {
            if(player.item != null)
            {
                itemManipulation.UseItem(player, player.item);
            }
        }
    }
}