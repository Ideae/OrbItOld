using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt.GameObjects {
    public class ChainBall : GameObject {

        //public Vector2 Pos;
        public float radius;
        //public Shape shape;
        public float ImageRadius;
        //public bool isActive;
        public float SpeedMultiplier;
        public bool IsMoving;

        public ChainBall()
        {
            //shape = new Circle(25.0f);
           
        }

        public override void Update(GameTime gametime) { }

        public override void Draw(SpriteBatch spritebatch) { }
    }
}


/* Old struct code in main game1 class
public struct ChainBall
    {
        public Vector2 position;
        public float Radius;
        public float ImageRadius;
        public bool IsActive;
        public float SpeedMultiplier;
        public bool IsMoving;
    }
*/