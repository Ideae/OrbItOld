using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using OrbIt.GameObjects;

namespace OrbIt {
    public class LightSource : GameObject {
        public Color col;
        public Vector2 position;
        public float r, g, b, rinitial, ginitial, binitial;
        public float a,ainitial;
        public float scale,sinitial;
        public float scaleRange;
        public float alphaRange;
        public int period;
        public Texture2D texture;
        public float rinc, ginc, binc, ainc, sinc;
        public bool acycle, scycle,ccycle;
        public Random rand;
        public float rrand,grand,brand;
        public string colorMode;
        

        public LightSource(Texture2D Texture) {
            r = g = b = rinitial = ginitial = binitial = 255;
            a = ainitial = 0.5f;
            col = new Color(1f, 1f, 1f, 0.5f);
            scale = sinitial = 1.0f;
            scaleRange = 0.5f;
            alphaRange = 0.2f;
            period = 200;
            texture = Texture;
            rinc = ginc = binc = 1.0f;
            ainc = 0.01f;
            sinc = 0.1f;
            acycle = scycle = ccycle = true;
            rand = new Random();
            rrand = grand = brand = -1f;
            colorMode = "normal";

        }
        public LightSource(float red, float green, float blue, float alpha, float alphaRange, float scale, float scaleRange,  int period, Texture2D Texture) {
            r = rinitial = red ; g = ginitial = green ; b = binitial = blue; a = ainitial = alpha ;
            col = new Color(r, g, b, a);
            this.scale = sinitial = scale;
            this.scaleRange = scaleRange;
            this.alphaRange = alphaRange;
            this.period = period;
            texture = Texture;
            rinc = ginc = binc = 0.01f;
            //rinc = ginc = binc = 0f;
            ainc = 0.01f;
            sinc = 0.01f;
            acycle = scycle = ccycle = true;
            acycle = false;
            rand = new Random();
            rrand = grand = brand = -1f;
            colorMode = "normal";
        }

        public void setIncrements(float r, float g, float b, float a, float s) {
            rinc = r; ginc = g; binc = b; ainc = a; sinc = s;
        }

        public override void Update(GameTime gametime)
        { 
            if (colorMode.Equals("normal"))
                Cycle();
            else if (colorMode.Equals("randomColors"))
                CycleRandColors(rand.Next(1000));
            else if (colorMode.Equals("flashing"))
                CycleFlashing(rand.Next(1000));
            else
                Cycle();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //uhhh
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 vect)
        {
            spriteBatch.Draw(texture, vect, null, new Color(r, g, b, a), 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(
        }

        public void Cycle() {
            if (acycle)
            {
                if (a >= (ainitial + alphaRange) || a <= (ainitial - alphaRange)) ainc *= -1;
                a = a + ainc;
            }
            if (scycle)
            {
                if (scale >= (sinitial + scaleRange) || scale <= (sinitial - scaleRange)) sinc *= -1;
                scale = scale + sinc;
            }
            if (ccycle)
            {
                //if (
                if ((r + rinc) >= 1.0f || (r + rinc) <= 0.0f) rinc *= -1; 
                r = (r + rinc);
                if ((g + ginc) >= 1.0f || (g + ginc) <= 0.0f) ginc *= -1;
                g = (g + rinc);
                if ((b + binc) >= 1.0f || (b + binc) <= 0.0f) binc *= -1;
                b = (b + binc);
                Console.WriteLine(r + "|" + g + "|" + b + "|" + a);
            }   
        }

        public void CycleRandColors(int seed) {
            if (acycle)
            {
                if (a >= (ainitial + alphaRange) || a <= (ainitial - alphaRange)) ainc *= -1;
                a = a + ainc;
            }
            if (scycle)
            {
                if (scale >= (sinitial + scaleRange) || scale <= (sinitial - scaleRange)) sinc *= -1;
                scale = scale + sinc;
            }
            if (ccycle)
            {
                Random random = new Random(seed);
                if (rrand < 0 || grand < 0 || brand < 0)
                {
                    rrand = (float)random.Next(255) / (float)255;
                    grand = (float)random.Next(255) / (float)255;
                    brand = (float)random.Next(255) / (float)255;
                    if (rinc < 0) rinc *= -1;
                    if (ginc < 0) ginc *= -1;
                    if (binc < 0) binc *= -1;
                }
                //when it reaches the color that it is approaching, it will reset the random color destinations
                if ((Math.Abs(r - rrand) <= rinc) && (Math.Abs(g - grand) <= ginc) && (Math.Abs(b - brand) <= binc))
                {
                    //Console.WriteLine((r-rrand) + " R:" + rinc + "   " + (g-grand) + " G:" + ginc);
                    rrand = (float)random.Next(255) / (float)255;
                    grand = (float)random.Next(255) / (float)255;
                    brand = (float)random.Next(255) / (float)255;
                    
                }
                else
                {
                    if (r < rrand) r += rinc; else if (r > rrand) r -= rinc;
                    if (g < grand) g += ginc; else if (g > grand) g -= ginc;
                    if (b < brand) b += binc; else if (b > brand) b -= binc;


                }
                //Console.WriteLine(r + "|" + g + "|" + b + "|" + a);
                //Console.WriteLine(rrand + "|" + grand + "|" + brand + "|" + a + "RANDS");
            }

        }

        public void CycleFlashing(int seed)
        {
            if (acycle)
            {
                if (a >= (ainitial + alphaRange) || a <= (ainitial - alphaRange)) ainc *= -1;
                a = a + ainc;
            }
            if (scycle)
            {
                if (scale >= (sinitial + scaleRange) || scale <= (sinitial - scaleRange)) sinc *= -1;
                scale = scale + sinc;
            }
            if (ccycle)
            {
                Random random = new Random(seed);
                if (rrand < 0 || grand < 0 || brand < 0)
                {
                    rrand = (float)random.Next(255) / (float)255;
                    grand = (float)random.Next(255) / (float)255;
                    brand = (float)random.Next(255) / (float)255;
                    if (rinc < 0) rinc *= -1;
                    if (ginc < 0) ginc *= -1;
                    if (binc < 0) binc *= -1;
                }
                //when it reaches the color that it is approaching, it will reset the random color destinations
                if ((Math.Abs(r - rrand) <= rinc) && (Math.Abs(g - grand) <= ginc) && (Math.Abs(b - brand) <= binc))
                {
                    //Console.WriteLine((r-rrand) + " R:" + rinc + "   " + (g-grand) + " G:" + ginc);
                    rrand = (float)random.Next(255) / (float)255;
                    grand = (float)random.Next(255) / (float)255;
                    brand = (float)random.Next(255) / (float)255;

                }
                else
                {
                    r = rrand; 
                    g = grand;
                    b = brand;


                }
                //Console.WriteLine(r + "|" + g + "|" + b + "|" + a);
                //Console.WriteLine(rrand + "|" + grand + "|" + brand + "|" + a + "RANDS");
            }
        
        }

    }
}
