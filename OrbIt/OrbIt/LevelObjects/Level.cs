using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt.LevelObjects
{
    public class Level
    {
        String levelname;
        public Vector3 tileLength;
        public Vector3 tileAmount;
        //public Vector3 cameraPosition;
        public Tile[,,] tile;
        public Tileset tileset;
        public bool colorBool;
        public String colorMode;
        public int levelwidth;
        public int levelheight;

        public Level()
        {
            levelname = "default";
            tileLength = new Vector3(50, 50, 20);
            tileAmount = new Vector3(20, 12, 1);
            levelwidth = (int)(tileLength.X * tileAmount.X);
            levelheight = (int)(tileLength.Y * tileAmount.Y);
            tile = new Tile[(int)tileAmount.X,(int)tileAmount.Y,(int)tileAmount.Z];
            tileset = null;
            colorBool = false;
            colorMode = "none";
        }
        public Level(String levelname, Vector3 tileLength, Vector3 tileAmount, Tileset tileset)
        {
            this.levelname = levelname;
            this.tileLength = tileLength;
            this.tileAmount = tileAmount;
            levelwidth = (int)(tileLength.X * tileAmount.X);
            levelheight = (int)(tileLength.Y * tileAmount.Y);
            tile = new Tile[(int)tileAmount.X, (int)tileAmount.Y, (int)tileAmount.Z];
            this.tileset = tileset;
            colorBool = false;
            colorMode = "none";
        
        }

        public void readTiles(String[] codes)
        {
            String[] codeLine = codes[0].Split(' ');
            tileAmount = new Vector3(codeLine.Length, codes.Length, 1);
            levelwidth = (int)(tileLength.X * tileAmount.X);
            levelheight = (int)(tileLength.Y * tileAmount.Y);
            tile = new Tile[(int)tileAmount.X, (int)tileAmount.Y, (int)tileAmount.Z];

            for (int i = 0; i < codes.Length; i++)
            {
                codeLine = codes[i].Split(' ');
                for (int j = 0; j < codeLine.Length; j++)
                {
                    tile[j, i, 0] = new Tile();
                    //tile[j, i, 0].color = Color.White;
                    tile[j,i,0].tilecode = codeLine[j];
                }

            }
        
        }

        public void Draw(SpriteBatch spriteBatch,Camera camera)
        {
            if (levelname == null || tileLength == null || tileAmount == null || tile == null || tileset == null)
            { Console.Write("Cannot draw Level"); return; }
            Random rand = new Random();
            //Color c;//= new Color(rand.Next(255), rand.Next(255), rand.Next(255));
            
            for (int x = 0; x < tileAmount.X; x++)
            {
                for (int y = 0; y < tileAmount.Y; y++)
                {
                    for (int z = 0; z < tileAmount.Z; z++)
                    {
                        Texture2D texture = tileset.getTexture(tile[x, y, z].tilecode);
                        if (colorMode.Equals("disco"))
                        {
                            if (colorBool)
                            {
                                tile[x, y, z].color = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
                            }
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y)-camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else if (colorMode.Equals("wave")) 
                        {
                            //tile[x, y, z].color.R = ((tile[x, y, z].color.R + (x + 1) * 10) % 255);
                            //Console.WriteLine(tile[x, y, z].color.R);
                            tile[x, y, z].color = new Color(((tile[x, y, z].color.R + (x + 1) * 10) % 255), ((tile[x, y, z].color.G + (y + 1) * 10) % 255), ((tile[x, y, z].color.B + (z + 1) * 10) % 255));
                            //tile[x, y, z].color = new Color(((tile[x, y, z].color.R + 2) % 255), ((tile[x, y, z].color.G + 2) % 255), ((tile[x, y, z].color.B + 2) % 255));
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else if (colorMode.Equals("wave2"))
                        {
                            //tile[x, y, z].color.R = ((tile[x, y, z].color.R + (x + 1) * 10) % 255);
                            tile[x, y, z].color = new Color(((tile[x, y, z].color.R + 2) % 255), ((tile[x, y, z].color.G + 2) % 255), ((tile[x, y, z].color.B + 2) % 255));
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else if (colorMode.Equals("wave3"))
                        {
                            tile[x, y, z].color = new Color(((tile[x, y, z].color.R + (x + 1) * 10) % 255), ((tile[x, y, z].color.G + (x + 1) * 10) % 255), ((tile[x, y, z].color.B + (x + 1) * 10) % 255));
                            //tile[x, y, z].color = new Color(((tile[x, y, z].color.R + 2) % 255), ((tile[x, y, z].color.G + 2) % 255), ((tile[x, y, z].color.B + 2) % 255));
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else if (colorMode.Equals("wave4"))
                        {
                            //tile[x, y, z].color = new Color(((tile[x, y, z].color.R + ((30 + x + y + 1) / 2) * 10) % 255), ((tile[x, y, z].color.G + ((30 + x + y + 1) / 2) * 10) % 255), ((tile[x, y, z].color.B + ((30 + x + y + 1) / 2) * 10) % 255));
                            tile[x, y, z].color = new Color((tile[x, y, z].color.R + (int)(y / 2)) % 255, (tile[x, y, z].color.G + (int)(y / 2)) % 255, (tile[x, y, z].color.B + (int)(y / 2)) % 255);
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else if (colorMode.Equals("wave5"))
                        {
                            //tile[x, y, z].color = new Color((tile[x, y, z].color.R + 1) % 255, (tile[x, y, z].color.G + 1) % 255, (tile[x, y, z].color.B + 1) % 255);
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, tile[x, y, z].color, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }
                        else {
                            spriteBatch.Draw(texture, new Vector2(x * tileLength.X + tileLength.X, y * tileLength.Y + tileLength.Y) - camera.position, null, Color.White, 0, new Vector2(tileLength.X, tileLength.Y), 1, SpriteEffects.None, 0);
                        }

                    }
                }
            }
            colorBool = false;
        
        }

    }
}
