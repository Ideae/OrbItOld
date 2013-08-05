using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrbIt.LevelObjects;

namespace OrbIt.GameObjects
{
    public class SlowNode : Node
    {
        public bool temporarySlow;
        public SlowNode(Room room) : base(room) { temporarySlow = true;
        texture = room.game1.textureDict[Game1.tn.yellowsphere];
        }

        public SlowNode(float Multiplier, float rangeRadius, float radius, Room room) : base(Multiplier, rangeRadius, radius, room) { temporarySlow = true;
        texture = room.game1.textureDict[Game1.tn.yellowsphere];
        }

        public SlowNode(float Multiplier, float rangeRadius, Vector2 Pos, bool isAct, float radius, Room room)
            : base(Multiplier, rangeRadius, Pos, isAct, radius, room)
        { temporarySlow = true;
        texture = room.game1.textureDict[Game1.tn.yellowsphere];
        }

        public override void ApplyEffect(MoveableObject obj)
        {
            if (obj.isActive)
            {
                float distVects = Vector2.Distance(obj.position, position);
                if (distVects < rangeRadius)
                {
                    float velX = Multiplier * obj.velocity.X;
                    float velY = Multiplier * obj.velocity.Y;
                    if (!temporarySlow)
                    {
                        obj.velocity.X -= velX;
                        obj.velocity.Y -= velY;
                    }
                    else
                    {
                        obj.position.X -= (obj.velocity.X * Multiplier);
                        obj.position.Y -= (obj.velocity.Y * Multiplier);
                    }
                }
            }
        }

    }
}
