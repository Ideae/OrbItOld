using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OrbIt
{
    public class GravNode
    {
        public Vector2 Position;
        public float GravMultiplier;
        public float Radius;
        public bool IsActive;

        public GravNode() 
        { 
            GravMultiplier = 0;
            Radius = 0;
            Position = new Vector2(0, 0);
            IsActive = false;
        }
        public GravNode(float GravMult, float rad)
        {
            GravMultiplier = GravMult;
            Radius = rad;
            Position = new Vector2(0, 0);
            IsActive = false;
        }
        public GravNode(float GravMult, float rad, Vector2 Pos, bool isAct) 
        {
            GravMultiplier = GravMult;
            Radius = rad;
            Position = Pos;
            IsActive = isAct;
        }

        public void initGravNode(Vector2 pos)
        {
            Position = pos;
            IsActive = true;
        }

        public void setGravNodeValues(float GravMult, float rad)
        {
            GravMultiplier = GravMult;
            Radius = rad;
        }

    }
}
