using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class Node : MoveableObject
    {
        //public Vector2 Position;
        //public Vector2 Velocity;
        public float VelMultiplier;
        public float Multiplier;
        public float rangeRadius;
        //public bool IsActive;
        //public float radius;
        //public float mass;
        public Texture2D texture;


        public Node(Room room) : base(room)
        {
            Multiplier = 0;
            rangeRadius = 0;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            isActive = false;
            radius = 25;
            mass = 1;
            
        }
        public Node(float Multiplier, float rangeRadius, float radius, Room room) : base(room)
        {
            this.Multiplier = Multiplier;
            this.rangeRadius = rangeRadius;
            this.radius = radius;
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            isActive = false;
            mass = 1;
            
        }
        public Node(float Multiplier, float rangeRadius, Vector2 Pos, bool isAct, float radius, Room room)
            : base(room)
        {
            this.Multiplier = Multiplier;
            this.rangeRadius = rangeRadius;
            this.radius = radius;
            position = Pos;
            isActive = isAct;
            mass = 1;
            
        }

        public void initNode(Vector2 pos)
        {
            position = pos;
            isActive = true;
        }

        public void setNodeValues(float Multiplier, float rangeRadius,float radius)
        {
            this.Multiplier = Multiplier;
            this.rangeRadius = rangeRadius;
            this.radius = radius;
        }

        public void fireNode(Double angle, Vector2 startPos)
        {
            velocity.X = (float)(Math.Cos(angle)) * VelMultiplier;
            velocity.Y = (float)(Math.Sin(angle)) * VelMultiplier;
            position = startPos;
            isActive = true;

        }
        

        public void setRadius(float newRadius)
        {
            //get the new mass of the node, based on the ratio between the new and old radius (the area of the circles)
            mass = (3.14f * newRadius * newRadius) / (3.14f * radius * radius);
            radius = newRadius;

        }

        public float DistanceVectors(Vector2 v1, Vector2 v2)
        {
            float dist = (float)Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
            return dist;
        }

        public virtual void ApplyEffect(MoveableObject obj)
        {
            
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (isActive)
            {
                foreach (KeyValuePair<string, List<GameObject>> entry in room.GameObjectDict) //use entry.key or entry.value
                {
                    foreach (GameObject gameobject in entry.Value)
                    {
                        if (!(gameobject == this) && gameobject is MoveableObject)
                        {
                            MoveableObject moveableobject = (MoveableObject)gameobject;
                            ApplyEffect(moveableobject);
                            if (Utils.checkCollision(this, moveableobject))
                                Utils.resolveCollision(this, moveableobject);
                        }
                    }
                }
                position += velocity;
                
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (isActive)
            {
                spritebatch.Draw(texture, position - room.game1.camera.position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 0);
            }
        }

        //old method
        public void updateNode()
        {
            position += velocity;
        }
    }
}