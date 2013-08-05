using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class RepelNode : Node
    {
        public RepelNode(Room room) : base(room) { texture = room.game1.textureDict[Game1.tn.purplesphere]; }

        public RepelNode(float Multiplier, float rangeRadius, float radius, Room room) : base(Multiplier, rangeRadius, radius, room) 
        { texture = room.game1.textureDict[Game1.tn.purplesphere]; }

        public RepelNode(float Multiplier, float rangeRadius, Vector2 Pos, bool isAct, float radius, Room room)
            : base(Multiplier, rangeRadius, Pos, isAct, radius, room)
        { texture = room.game1.textureDict[Game1.tn.purplesphere]; }


        public override void ApplyEffect(MoveableObject obj)
        {
            if (obj.isActive)
            {
                float distVects = Vector2.Distance(obj.position, position);
                if (distVects < rangeRadius)
                {
                    double angle = Math.Atan2((position.Y - obj.position.Y), (position.X - obj.position.X));
                    float counterforce = 100 / distVects;
                    //float counterforce = 1;
                    float gravForce = Multiplier / (distVects * distVects * counterforce);
                    //float gravForce = gnode1.GravMultiplier;
                    float velX = (float)Math.Cos(angle) * gravForce;
                    float velY = (float)Math.Sin(angle) * gravForce;
                    obj.velocity.X -= velX;
                    obj.velocity.Y -= velY;

                }
            }
        }

    }
}
