using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;
namespace OrbIt.GameObjects
{
    public class Orb : MoveableObject
    {
        
        //public Vector2 Position;
        //public Vector2 Velocity;
        public Vector2 Acceleration;
        public Vector2 Jerk;
        //public bool IsActive;
        public float VelMultiplier;
        public float AccMultiplier;
        public float JerkMultiplier;
        //public float radius;
        //public float mass;// { get { return 0; }
            //private set; }
        public bool inSlow;
        public int slowsActive;
        public int textureNum;

        public LightSource lightsource;
        public Texture2D texture;
        
        public Orb(Room room) : base(room){
            isActive = false;
            velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
            Jerk = new Vector2(0, 0);
            VelMultiplier = 0.0f;
            AccMultiplier = 0.0f;
            JerkMultiplier = 0.0f;
            position = new Vector2(0, 0);
            textureNum = 0;
            radius = 25;
            mass = 1;
            
            texture = room.game1.textureDict[Game1.tn.orangesphere];
        }

        public Orb(float vmult, float amult, float jmult, Room room) : base(room)
        {
            isActive = false;
            velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
            Jerk = new Vector2(0, 0);
            VelMultiplier = vmult;
            AccMultiplier = amult;
            JerkMultiplier = jmult;
            position = new Vector2(0, 0);
            textureNum = 0;
            radius = 25;
            mass = 1;
            texture = room.game1.textureDict[Game1.tn.orangesphere];
        }

        public void InitOrb(Double angle, Vector2 startPos)
        {
            Vector2 Direction = new Vector2((float)(Math.Cos(angle)), (float)(Math.Sin(angle)));
            velocity.X = Direction.X * VelMultiplier;
            velocity.Y = Direction.Y * VelMultiplier;

            float velRatioX = (Math.Abs(Direction.X) / (Math.Abs(Direction.X) + Math.Abs(Direction.Y)));
            float velRatioY = (Math.Abs(Direction.Y) / (Math.Abs(Direction.X) + Math.Abs(Direction.Y)));
            float accelX = velRatioX * AccMultiplier;
            float accelY = velRatioY * AccMultiplier;
            if (Direction.X <= 0) { accelX = accelX * -1; }
            if (Direction.Y <= 0) { accelY = accelY * -1; }

            float jerkX = velRatioX * JerkMultiplier;
            float jerkY = velRatioY * JerkMultiplier;
            if (Direction.X <= 0) { jerkX = jerkX * -1; }
            if (Direction.Y <= 0) { jerkY = jerkY * -1; }

            Acceleration = new Vector2(accelX, accelY);
            Jerk = new Vector2(jerkX, jerkY);
            position = new Vector2(startPos.X, startPos.Y);
            isActive = true;
            slowsActive = 0;

            
        }

        public void setOrbValues(float vmult, float amult, float jmult)
        {
            VelMultiplier = vmult;
            AccMultiplier = amult;
            JerkMultiplier = jmult;
        }
        //old update method
        public void UpdateOrb()
        {
            Acceleration.X += Jerk.X;
            Acceleration.Y += Jerk.Y;

            velocity.X += Acceleration.X;
            velocity.Y += Acceleration.Y;

            position.X += velocity.X;
            position.Y += velocity.Y;
        }

        public override void Update(GameTime gametime)
        {
            if (isActive)
            {
                Acceleration.X += Jerk.X;
                Acceleration.Y += Jerk.Y;

                velocity.X += Acceleration.X;
                velocity.Y += Acceleration.Y;

                position.X += velocity.X;
                position.Y += velocity.Y;
                
                //collision
                foreach (KeyValuePair<string, List<GameObject>> entry in room.GameObjectDict) //use entry.key or entry.value
                {
                    foreach (GameObject gameobject in entry.Value)
                    {
                        if (!(gameobject == this) && gameobject is MoveableObject)
                        {
                            MoveableObject moveableobject = (MoveableObject)gameobject;
                            //ApplyEffect(moveableobject);
                            if (Utils.checkCollision(this,moveableobject))
                                Utils.resolveCollision(this, moveableobject);
                        }
                    }
                }
                wallBounce();
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            
            if (isActive)
            {
                
                if (radius != texture.Width / 2)
                {
                    float scale = radius / (texture.Width / 2);
                    spritebatch.Draw(texture, position - room.game1.camera.position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
                }
                else
                {
                    spritebatch.Draw(texture, position - room.game1.camera.position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 0);
                }
            }
        }


        public void setRadius(float newRadius)
        {
            //get the new mass of the orb, based on the ratio between the new and old radius (the area of the circles)
            mass = ((newRadius * newRadius) / ( radius * radius))*mass;
            radius = newRadius;
           
        }
        /*
        public void ApplyGrav(GravityNode gnode)
        {
            //float distVects = DistanceVectors(position, gnode.position);
            float distVects = Vector2.Distance(position, gnode.position);
            if (distVects < gnode.rangeRadius)
            {
                double angle = Math.Atan2((gnode.position.Y - position.Y), (gnode.position.X - position.X));
                float counterforce = 100 / distVects;
                //float counterforce = 1;
                float gravForce = gnode.Multiplier / (distVects * distVects * counterforce);
                //float gravForce = gnode1.GravMultiplier;
                float velX = (float)Math.Cos(angle) * gravForce;
                float velY = (float)Math.Sin(angle) * gravForce;
                Velocity.X += velX;
                Velocity.Y += velY;

            }
        }

        public void ApplyRepel(RepelNode rnode)
        {
            float distVects = Vector2.Distance(Position, rnode.Position);
            if (distVects < rnode.rangeRadius)
            {
                double angle = Math.Atan2((rnode.Position.Y - Position.Y), (rnode.Position.X - Position.X));
                float counterforce = 100 / distVects;
                //float counterforce = 1;
                float gravForce = rnode.Multiplier / (distVects * distVects * counterforce);
                //float gravForce = gnode1.GravMultiplier;
                float velX = (float)Math.Cos(angle) * gravForce;
                float velY = (float)Math.Sin(angle) * gravForce;
                Velocity.X -= velX;
                Velocity.Y -= velY;

            }
        }
        public void ApplySlow(SlowNode snode)
        {
            float distVects = Vector2.Distance(Position, snode.Position);
            if (distVects < snode.rangeRadius)
            {
                float velX = snode.Multiplier * Velocity.X;
                float velY = snode.Multiplier * Velocity.Y;
                if (!snode.temporarySlow)
                {
                    Velocity.X -= velX;
                    Velocity.Y -= velY;
                }
                else
                {
                    Position.X -= (Velocity.X * snode.Multiplier);
                    Position.Y -= (Velocity.Y * snode.Multiplier);
                }
            }
            
        }

        public void ApplyTransfer(TransferNode transfernode)
        {
            float distVects = Vector2.Distance(Position, transfernode.Position);
            if (distVects < transfernode.rangeRadius)
            {
                //double angle = Math.Atan2((transfernode.Position.Y - Position.Y), (transfernode.Position.X - Position.X));
                //double newangle = angle + 3.14;
                float newX = (transfernode.Position.X - Position.X) * 2.05f;
                float newY = (transfernode.Position.Y - Position.Y) * 2.05f;
                Position.X += newX;
                Position.Y += newY;
                //float counterforce = 100 / distVects;
                //float counterforce = 1;
                //float gravForce = gnode.Multiplier / (distVects * distVects * counterforce);
                //float gravForce = gnode1.GravMultiplier;
                //float velX = (float)Math.Cos(angle) * gravForce;
                //float velY = (float)Math.Sin(angle) * gravForce;
                //Velocity.X += velX;
                //Velocity.Y += velY;

            }
        }
        */

        /*
        private float DistanceVectors(Vector2 v1, Vector2 v2)
        {
            float dist = (float)Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
            return dist;
        }
        */
    }
}
