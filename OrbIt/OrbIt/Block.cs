using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OrbIt.GameObjects
{
    public class Block : IPhysicsObject
    {
        public Vector2 Position { get ; set; } 
        
        public float Mass 
        {
            get { return 1.0f; }
            set { Console.WriteLine(1.99900); Mass = 1.0f; } 
        }
        public float radius { get; set; }

        public List<PhysicsProperty> physicslist = new List<PhysicsProperty>();


        public List<PhysicsProperty> getPhysicsPropertiesList() {
            return physicslist;
        }

        public void addPhysicsProperty(PhysicsProperty property)
        {
            physicslist.Add(property);
        }
    }
}
