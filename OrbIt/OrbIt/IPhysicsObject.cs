using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OrbIt
{
    public interface IPhysicsObject
    {
        Vector2 Position { get; set; }
        float radius { get; set; }
        float Mass { get; set; }

        //PhysicsTypeEnum type;

        List<PhysicsProperty> getPhysicsPropertiesList();

        void addPhysicsProperty(PhysicsProperty property);
    }
}
