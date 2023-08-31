using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Chain : Chest
    {
        public Chain(SoundsController soundsController, Item content) : base(soundsController, content)
        {
            this.soundsController = soundsController;
        }
        public override string typeOfAssociatedKey => "Hammer";
        public override (int floorNumber, int roomNumber) locationMap => (1, 1);
        public override (int y, int x) locationCoordinates => (5, 5);
        public override string useSoundFileName => "HammerHittingChain";
        public override string getItemName()
        {
            return content.getItemName()+ " chained to wall, maybe a hammer would help...";
        }
        public override char GetSymbol(Map map)
        {
            return content.GetSymbol(map);
        }

    }
}