using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Monster : Character
    {
        public Monster()
        {
            this.symbol = 'M';
            this.name = "Monster";
        }
        public MonsterWalkingInformations monsterWalkingInformations = new MonsterWalkingInformations();
        public MonsterChasingInformations monsterChasingInformations = new MonsterChasingInformations();
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
        public override void Die()
        {
            die();
            info.win = true;
            info.run = false;
        }
        public override int GetPriorityNumber()
        {
            return 30;
        }
    }
}