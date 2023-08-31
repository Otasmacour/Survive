using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WeaponChest : Chest
    {
        public WeaponChest(SoundsController soundsController, Item content) : base(soundsController, content)
        {
            this.soundsController = soundsController;
        }
        public override string typeOfAssociatedKey => "WeaponKey";
        public override (int floorNumber, int roomNumber) locationMap => (1, 4);
        public override (int y, int x) locationCoordinates => (1, 8);
        public override string getItemName()
        {
            return "Weapon chest";
        }
        public override char GetSymbol(Map map)
        {
            return 'w';
        }
    }
}
