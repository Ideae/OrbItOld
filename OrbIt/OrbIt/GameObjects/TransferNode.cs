using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class TransferNode : Node
    {
        public TransferNode(Room room) : base(room) { texture = room.game1.textureDict[Game1.tn.pinksphere]; }

        public TransferNode(float Multiplier, float rangeRadius, float radius, Room room) : base(Multiplier, rangeRadius, radius, room) {
            texture = room.game1.textureDict[Game1.tn.pinksphere];
        }

        public TransferNode(float Multiplier, float rangeRadius, Vector2 Pos, bool isAct, float radius, Room room)
            : base(Multiplier, rangeRadius, Pos, isAct, radius, room)
        {
            texture = room.game1.textureDict[Game1.tn.pinksphere];
        }

        public override void ApplyEffect(MoveableObject obj)
        {
            if (obj.isActive)
            {
                float distVects = Vector2.Distance(obj.position, position);
                if (distVects < rangeRadius)
                {
                    //double angle = Math.Atan2((transfernode.Position.Y - Position.Y), (transfernode.Position.X - Position.X));
                    //double newangle = angle + 3.14;
                    float newX = (position.X - obj.position.X) * 2.05f;
                    float newY = (position.Y - obj.position.Y) * 2.05f;
                    obj.position.X += newX;
                    obj.position.Y += newY;
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
        }
    }
}
