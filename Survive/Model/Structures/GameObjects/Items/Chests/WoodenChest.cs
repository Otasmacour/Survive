using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class WoodenChest : Chest
    {
        public WoodenChest(SoundsController soundsController, Item content): base(soundsController, content)
        {
            this.soundsController = soundsController;
        }
        public override string itemName => "Wooden chest";
        public override char GetSymbol(Map map)
        {
            return 'w';
        }
    }
}
