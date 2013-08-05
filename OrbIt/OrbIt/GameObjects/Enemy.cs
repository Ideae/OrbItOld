using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class Enemy : MoveableObject
    {
        //public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public float Power;
        public float VelMultiplier;
        public Texture2D texture;
        public float radius;
        public int hitpoints;
        public int totalHitpoints;
        //public float mass;

        public Enemy(Room room) : base(room)
        {
            position = new Vector2(0,0);
            IsAlive = false;
            Color = Color.White;
            Power = 1;
            VelMultiplier = 1.0f;
            radius = 20;
            hitpoints = 1;
            mass = 1;
        }

        public void Initialize(Game1 game)
        {
            IsAlive = true;
            Random rand = new Random();
            int nextrand = rand.Next(5);
            if (nextrand == 0) { position = new Vector2(0, rand.Next(game.screenHeight)); }
            else if (nextrand == 1) { position = new Vector2(game.screenWidth, rand.Next(game.screenHeight)); }
            else if (nextrand == 3) { position = new Vector2(rand.Next(game.screenWidth), 0); }
            else if (nextrand == 4) { position = new Vector2(rand.Next(game.screenWidth), game.screenHeight); }

            VelMultiplier = 1.0f;
        }

        public override void Update(GameTime gametime)
        {
            if (position.X < room.player1.position.X) { position.X += VelMultiplier; }
            else if (position.X > room.player1.position.X) { position.X -= VelMultiplier; }
            if (position.Y > room.player1.position.Y) { position.Y -= VelMultiplier; }
            else if (position.Y < room.player1.position.Y) { position.Y += VelMultiplier; }

            foreach (Orb bullet in room.GameObjectDict["bullets"])
            {
                if (Utils.checkCollision(this, bullet))
                {
                    bullet.isActive = false;
                    //room.GameObjectDict["bullets"].Remove(bullet);

                    hitpoints -= 1;
                    if (hitpoints <= 0)
                    {
                        isActive = false;
                        //room.GameObjectDict["enemies"].Remove(this);
                    }
                }
            }
        }

    }

    


}
