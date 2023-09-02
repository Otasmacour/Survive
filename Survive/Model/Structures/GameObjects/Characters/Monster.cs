using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Monster : Character
    {
        public MonsterMovementInformation monsterMovementInformations;
        public MonsterWalkingInformation monsterWalkingInformation = new MonsterWalkingInformation();
        public MonsterChasingInformation monsterChasingInformation = new MonsterChasingInformation();
        public MonsterSearchingInformation monsterSearchingInformation = new MonsterSearchingInformation();
        public Monster(Informations info)
        {
            this.info = info;
            this.monsterMovementInformations = new MonsterMovementInformation(this);
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
            alerts.Add("Monster is now gone for about 1 minute");
        }
        public override char GetSymbol(Map map)
        {
            return 'A';
        }
        public override int GetPriorityNumber()
        {
            return 30;
        }
    }
}