using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OrbIt.LevelObjects;


namespace OrbIt.GameObjects {
    public class MoveableObject : GameObject{

        public Vector2 position;
        public Vector2 velocity;
        public Shape shape;
        public bool isActive;
        public float radius;
        public float mass;
        public Room room;

        public MoveableObject(Room room)
        {
            this.room = room;
        }

        public override void Update(GameTime gametime){}

        public override void Draw(SpriteBatch spritebatch){}

        public void wallBounce()
        {
            if (room.PropertiesDict["wallBounce"])
            {

                if (position.X >= (room.level.levelwidth - radius))
                {
                    position.X = room.level.levelwidth - radius;
                    velocity.X *= -1;

                }
                else if (position.X < radius)
                {
                    position.X = radius;
                    velocity.X *= -1;

                }
                else if (position.Y >= (room.level.levelheight - radius))
                {
                    position.Y = room.level.levelheight - radius;
                    velocity.Y *= -1;

                }
                else if (position.Y < radius)
                {
                    position.Y = radius;
                    velocity.Y *= -1;

                }
            }
            else
            {
                if (position.X >= room.level.levelwidth || position.X < 0 || position.Y >= room.level.levelheight || position.Y < 0)
                {
                    isActive = false;
                }
            }
        }
    }
}
