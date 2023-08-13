using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class NullItem : Item
    {
        public override int GetPriorityNumber()
        {
            throw new NotImplementedException();
        }
        public override void PickUp(Character character)
        {
            throw new NotImplementedException();
        }
        public override void Drop()
        {
            throw new NotImplementedException();
        }
        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}