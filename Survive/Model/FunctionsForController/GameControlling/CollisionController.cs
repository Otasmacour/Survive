using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class CollisionController
    {
        Characters characters;
        Player player;
        Monster monster;
        MapHelper mapHelper;
        Alerts alerts;
        public CollisionController(Characters characters, MapHelper mapHelper, Alerts alerts)
        {
            this.characters = characters;
            this.player = characters.player;
            this.monster = characters.monster;
            this.mapHelper = mapHelper;
            this.alerts = alerts;
        }
        public void MonsterAndPlayerCollision()
        {
            if(monster.living == false) { return; }
            if(monster.mapWhereIsLocated != player.mapWhereIsLocated || player.visible == false)
            {
                //They are not on the same map
                //Or the player is not visible, could be hidden in a hideout
                return;
            }
            if(mapHelper.boolFunctions.IsPlayerWithinRangeOfMonster())
            {
                player.Die("this is not a speed race, you'd better play stealth", alerts);
            }
        }
    }
}