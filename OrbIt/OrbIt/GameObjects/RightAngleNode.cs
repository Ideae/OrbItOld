using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class RightAngleNode : Node
    {
        public RightAngleNode(Room room)
            : base(room)
        {
            texture = room.game1.textureDict[Game1.tn.whitecircle];
            collidable = false;
        }

        public RightAngleNode(float Multiplier, float rangeRadius, float radius, Room room)
            : base(Multiplier, rangeRadius, radius, room)
        {
            texture = room.game1.textureDict[Game1.tn.whitecircle];
            collidable = false;
        }

        public RightAngleNode(float Multiplier, float rangeRadius, Vector2 Pos, bool isAct, float radius, Room room)
            : base(Multiplier, rangeRadius, Pos, isAct, radius, room)
        {
            texture = room.game1.textureDict[Game1.tn.whitecircle];
            collidable = false;
        }

        public override void ApplyEffect(MoveableObject obj)
        {
            if (obj.isActive)
            {
                float distVects = Vector2.Distance(obj.position, position);
                if (distVects < rangeRadius)
                {
                    float step = 1.0f;
                    if (obj.velocity.X > velocity.X)
                        obj.velocity.X -= step;
                    else if (obj.velocity.X < velocity.X)
                        obj.velocity.X += step;
                    if (obj.velocity.Y > velocity.Y)
                        obj.velocity.Y -= step;
                    else if (obj.velocity.Y < velocity.Y)
                        obj.velocity.Y += step;

                }
            }
        }

        //public void Draw(SpriteBatch spritebatch) : base(spritebatch) {} ;
    }
}
