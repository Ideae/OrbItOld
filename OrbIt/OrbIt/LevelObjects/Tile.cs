using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OrbIt.LevelObjects
{
    public class Tile
    {
        public String tilecode;
        public Color color;
        public String collisioncode;
        //public int tileSize;
        //public Texture2D texture;

        public Tile()
        {
            tilecode = "0";
            collisioncode = "0";
            color = Color.White;
            //tileSize = 50;
            //texture = null;
        }


        public Tile(String tilecode, String collisioncode)
        {
            this.tilecode = tilecode;
            color = Color.White;
            this.collisioncode = collisioncode;
        }

        public void setValues(String tilecode, String collisioncode)
        {
            this.tilecode = tilecode;
            this.collisioncode = collisioncode;
            //this.tileSize = tileSize;
            //this.texture = texture;
        }

    }
}
