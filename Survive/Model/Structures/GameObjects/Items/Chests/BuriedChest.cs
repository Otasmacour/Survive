using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class BuriedChest : Chest
    {
        public BuriedChest(SoundsController soundsController, Item content) : base(soundsController, content)
        {
            this.soundsController = soundsController;
            this.visible = false;
        }
        public override string typeOfAssociatedKey => "MysteriousKey";
        public override int floorNumberWhereItemSpawns => -1;
        public override string getItemName()
        {
            return "Buried Chest";
        }
        public override char GetSymbol(Map map)
        {
            if (visible) { return 'B'; }
            else { return '.'; }
        }
    }
}