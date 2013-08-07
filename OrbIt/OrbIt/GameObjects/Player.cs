using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class Player : MoveableObject
    {
        //public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public float Power;
        public float VelMultiplier;
        public int hitpoints;
        public int score;
        public float radius;
        public LightSource playerLight;

        public Player(Room room) : base(room)
        {
            shape = new Circle(25.0f);
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            position += velocity;

            wallBounce();
            /*
            if (position.X + radius > room.level.levelwidth)
                position.X = room.level.levelwidth - radius;
            if (position.X - radius < 0)
                position.X = radius;
            if (position.Y + radius > room.level.levelheight)
                position.Y = room.level.levelheight - radius;
            if (position.Y - radius < 0)
                position.Y = radius;
            */
            
        }
    }
}
