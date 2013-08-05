using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OrbIt.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt.GameObjects {
    public class Circle : Shape{
        public float radius;


        public Circle()
        {
            // TODO: Complete member initialization
        }
        public Circle(float radius)
        {
            this.radius = radius;
        }

        



        /*
        public override void Initialize() {
            radius = 0;
            //mass = 1;
        }

        public void Initialize(float radius) { }

        public override void Update(GameTime gametime) { }

        public override void Draw(SpriteBatch spritebatch) { }
        */
    }
}
