using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects {
    public class Laser {
        public Vector2 position;
        public Vector2 velocity;
        public double angle;

        public Laser() {
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            angle = 0;
        }

        public void Intialize(Vector2 Position, Vector2 MousePosition,float speed)
        {
            //Double angle = Math.Atan2((MousePosition.Y - Position.Y), (MousePosition.X - Position.X));

            position = Position;
            Vector2 unitvector = new Vector2((MousePosition.X - Position.X), (MousePosition.Y - Position.Y));
            unitvector.Normalize();
            unitvector *= speed;
            velocity = unitvector;
            angle = Math.Atan2(velocity.Y, velocity.X)+(Math.PI / 2);
            
        }

        public void Update() {
            position.X += velocity.X;
            position.Y += velocity.Y;
            //angle = Math.Atan2(velocity.Y, velocity.X);
        }


        public void Draw(SpriteBatch sb,Texture2D texture,Camera camera) {
            //double angle = Math.Atan2(velocity.Y, velocity.X);
            sb.Draw(texture, position - camera.position, null, Color.Red, (float)angle, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //spriteBatch.Draw(repelTexture, rnodes[i].Position, null, Color.White, 0, new Vector2(repelTexture.Width / 2, repelTexture.Height / 2), 1, SpriteEffects.None, 0);
        }

    }
}
