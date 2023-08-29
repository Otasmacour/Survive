using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Monster : Character
    {
        
        public MonsterMovementInformations monsterMovementInformations;
        public MonsterWalkingInformations monsterWalkingInformations = new MonsterWalkingInformations();
        public MonsterChasingInformations monsterChasingInformations = new MonsterChasingInformations();
        public MonsterSearchingInformation monsterSearchingInformation = new MonsterSearchingInformation();
        public Monster(Informations info)
        {
            this.info = info;
            this.monsterMovementInformations = new MonsterMovementInformations(this);
        }
        public override bool CanGoThere(List<GameObject>[,] twoDArray, Coordinates coordinates)
        {
            List<GameObject> list = twoDArray[coordinates.y, coordinates.x];
            foreach (GameObject obj in list)
            {
                if (obj is Wall)
                {
                    return false;
                }
                else if (obj is Furniture)
                {
                    return false;
                }
            }
            return true;
        }
        public override void Die(string theWayHowMonsterDied, Alerts alerts)
        {
            die();
            alerts.Add("Granny is now gone for about 2 minutes");
        }
        public override char GetSymbol(Map map)
        {
            return 'M';
        }
        public override int GetPriorityNumber()
        {
            return 30;
        }
    }
}