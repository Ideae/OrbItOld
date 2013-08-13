using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrbIt.GameObjects;
using Microsoft.Xna.Framework;
using OrbIt.LevelObjects;

namespace OrbIt {
    public class Utils {

        

        public static bool checkCollision(MoveableObject o1, MoveableObject o2)
        {
            
            if (Vector2.DistanceSquared(o1.position, o2.position) <= (((o1.radius) + (o2.radius)) * ((o1.radius) + (o2.radius))) && o1.collidable && o2.collidable)
            {
                return true;
            }
            return false;
        }

        public static void resolveCollision(MoveableObject o1, MoveableObject o2)
        {
            
                /*Console.WriteLine("Collision Occured.");
                o1.IsActive = false;
                o2.IsActive = false;
                 */
                
                //Console.WriteLine(o1.mass + " " + o2.mass);

                //ELASTIC COLLISION RESOLUTION --- FUCK YEAH
                //float orbimass = 1, orbjmass = 1;
                //float orbRadius = 25.0f; //integrate this into the orb class
                float distanceOrbs = (float)Vector2.Distance(o1.position, o2.position);
                Vector2 normal = (o2.position - o1.position) / distanceOrbs;
                float pvalue = 2 * (o1.velocity.X * normal.X + o1.velocity.Y * normal.Y - o2.velocity.X * normal.X - o2.velocity.Y * normal.Y) / (o1.mass + o2.mass);
                o1.velocity.X = o1.velocity.X - pvalue * o2.mass * normal.X;
                o1.velocity.Y = o1.velocity.Y - pvalue * o2.mass * normal.Y;
                o2.velocity.X = o2.velocity.X + pvalue * o1.mass * normal.X;
                o2.velocity.Y = o2.velocity.Y + pvalue * o1.mass * normal.Y;
                //if (game1.fixCollisionOn)
                    fixCollision(o1, o2);
            
        
        }

        //make sure that if the orbs are stuck together, they are separated.
        public static void fixCollision(MoveableObject o1, MoveableObject o2)
        {
            //float orbRadius = 25.0f; //integrate this into the orb class
            //if the orbs are still within colliding distance after moving away (fix radius variables)
            if (Vector2.DistanceSquared(o1.position + o1.velocity, o2.position + o2.velocity) <= ((o1.radius * 2) * (o2.radius * 2)))
            {

                Vector2 difference = o1.position - o2.position; //get the vector between the two orbs
                float length = Vector2.Distance(o1.position, o2.position);//get the length of that vector
                difference = difference / length;//get the unit vector
                //fix the below statement to get the radius' from the orb objects
                length = (o1.radius + o2.radius) - length; //get the length that the two orbs must be moved away from eachother
                difference = difference * length; // produce the vector from the length and the unit vector
                o1.position += difference / 2;
                o2.position -= difference / 2;
            }
            else return;
        }

        public static Vector2 projection(Vector2 aa, Vector2 bb)
        {
            Vector2 proj = (Vector2.Dot(aa,bb)/bb.LengthSquared())*bb;
            return proj;
        }



        public static bool pointInRectangle(int px, int py, int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy)
        {
            // 0 ≤ AP·AB ≤ AB·AB and 0 ≤ AP·AD ≤ AD·AD
            float r1 = Vector2.Dot(new Vector2(px - ax, py - ay),new Vector2(bx - ax, by - ay));
            float r2 = Vector2.Dot(new Vector2(bx - ax, by - ay),new Vector2(bx - ax, by - ay));
            float r3 = Vector2.Dot(new Vector2(px - ax, py - ay), new Vector2(dx - ax, dy - ay));
            float r4 = Vector2.Dot(new Vector2(dx - ax, dy - ay), new Vector2(dx - ax, dy - ay));
            if (0 <= r1 && r1 <= r2 && 0 <= r3 && r3 <= r4)
            {
                return true;
            }
            return false;
        
        }

        public static bool pointInCircle(int px, int py, int rad, int ax, int ay)
        {
            if (Vector2.Distance(new Vector2(ax, ay), new Vector2(px, py)) <= rad)
            {
                return true;
            }
            return false;
        }

        public static bool intersectCircleRectCorners(int px, int py, int rad, int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy)
        {
            if (pointInCircle(px, py, rad, ax, ay)
                || pointInCircle(px, py, rad, bx, by)
                || pointInCircle(px, py, rad, cx, cy)
                || pointInCircle(px, py, rad, dx, dy))
                return true;//make circle bounce in weird way if this is the case
            return false;
        }

        public static bool intersectCircleRect(int px, int py, int rad, int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy)
        {
            //if (pointInRectangle(px, py, ax, ay, bx, by, cx, cy, dx, dy)) return true;

            if (intersectCircleLine(px, py, rad, ax, ay, bx, by)
                || intersectCircleLine(px, py, rad, bx, by, cx, cy)
                || intersectCircleLine(px, py, rad, cx, cy, dx, dy)
                || intersectCircleLine(px, py, rad, dx, dy, ax, ay)
                )
                return true;


            return false;
        }


        public static bool intersectCircleLine(int px, int py, int rad, int ax, int ay, int bx, int by)
        {
            // And intersectCircle is easy to implement too: one way would be to check if the 
            // foot of the perpendicular from P to the line is close enough and between the endpoints, 
            // and check the endpoints otherwise.

            Vector2 line = new Vector2(bx - ax, by - ay);
            Vector2 perp = new Vector2((by - ay), -(bx - ax));
            float dist = Vector2.Distance(new Vector2(0, 0), perp);
            Vector2 unit = perp / dist;
            //Console.WriteLine(Vector2.Distance(new Vector2(0, 0),unit));
            Vector2 newvect = unit * rad;
            Rectangle rect1 = new Rectangle(px, py, px + (int)newvect.X, py + (int)newvect.Y);
            //Console.WriteLine((int)newvect.X + " " + (int)newvect.Y);
            //Console.WriteLine(Vector2.Distance(new Vector2(0, 0), newvect));
            //Console.WriteLine(px + " " + py + " " + (px + newvect.X) + " " + (py + newvect.Y) + " " + ax + " " + ay + " " + bx + " " + by);
            Point p = checklinescollide(px,py,px+newvect.X,py+newvect.Y,ax,ay,bx,by);
            if (p.X <= 0 && p.Y <= 0)
            {
                
                return false;
            }
            //Console.WriteLine("Yep");
            return true;
                
        }

        public static Point checklinescollide(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            float A1 = y2 - y1;
            float B1 = x1 - x2;
            float C1 = A1 * x1 + B1 * y1;
            float A2 = y4 - y3;
            float B2 = x3 - x4;
            float C2 = A2 * x3 + B2 * y3;
            float det = A1 * B2 - A2 * B1;
            if (det != 0)
            {
                float x = (B2 * C1 - B1 * C2) / det;
                float y = (A1 * C2 - A2 * C1) / det;
                if (x >= Math.Min(x1, x2) && x <= Math.Max(x1, x2) && x >= Math.Min(x3, x4) && x <= Math.Max(x3, x4)
                                && y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2) && y >= Math.Min(y3, y4) && y <= Math.Max(y3, y4))
                    return new Point((int)x, (int)y);
            }

            return new Point(-10, -10);


        }
    }
}
