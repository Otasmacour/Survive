using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    enum Direction { Left, Right, Up, Down, TopLeft, TopRight, BottomLeft, BottomRight, Null }
    enum UserIntent { Move, Drop, PickUp, Use, SwitchItem, Null}
    enum MapType { Stairs, Garden, Null, Abnormal }
}