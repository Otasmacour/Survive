using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    abstract class Item : GameObject
    {
        public abstract void PickUp(Character character);
        public abstract void Drop();
        public abstract void Use();
    }
}