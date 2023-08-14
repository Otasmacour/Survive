using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class NullItem : Item
    {
        public override string itemName => throw new NotImplementedException();
        public override int noiseLevel => throw new NotImplementedException();
        public override char symbol => throw new NotImplementedException();
        public override int GetPriorityNumber()
        {
            throw new NotImplementedException();
        }
        public override void PickUp(Character character)
        {
            throw new NotImplementedException();
        }
        public override void Drop(Character character)
        {
            throw new NotImplementedException();
        }
        public override void Use(Character character)
        {
            throw new NotImplementedException();
        }
    }
}