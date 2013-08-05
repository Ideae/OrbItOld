using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace OrbIt
{
    class PhysicsEngine : List<IPhysicsObject>
    {

        public void update(float timestep)
        {
            for (int i = 0; i < this.Count; i++)
            {
                /*
                IPhysicsObject p1 = this[i];
                for (int j = 0; j < this.Count; j++)
                {
                PhysicsObject p2 = this[j];
                /
                if (p2.type & PhysicsTypeEnum.GRAVITY == PhysicsTypeEnum.GRAVITY)
                {
                    float dist = Vector2.Distance(p1.Position, gnode.Position);
                    if (dist < gnode.rangeRadius)
                    {
                        double angle = Math.Atan2((gnode.Position.Y - Position.Y), (gnode.Position.X - Position.X));
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
                */
            }
        }
    }
}
