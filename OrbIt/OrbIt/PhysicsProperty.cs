using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrbIt
{
    public interface PhysicsProperty
    {
        //public int x { get; set; }
        //public int y { get; set; }

        float getMultiplier();

        String getEffectName();
        
    }
}
