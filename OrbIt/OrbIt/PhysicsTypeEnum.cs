using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbIt
{
    [Flags]
    enum PhysicsTypeEnum
    {
        NONE = 1 << 0,
        GRAVITY = 1 << 1,
        REPEL = 1 << 2,
        SLOW = 1 << 3,
        TRANSFER = 1 << 4,
        ALL = NONE | GRAVITY | REPEL | SLOW | TRANSFER 
    }
}
/*

none    0000
graivty 0001
repel   0010
slow    0100
transfer1000
all     1111


graivty | repel
0011
*/