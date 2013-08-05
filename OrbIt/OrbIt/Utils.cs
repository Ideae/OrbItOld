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
            
            if (Vector2.DistanceSquared(o1.position, o2.position) <= (((o1.radius) + (o2.radius)) * ((o1.radius) + (o2.radius))))
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

    }
}
