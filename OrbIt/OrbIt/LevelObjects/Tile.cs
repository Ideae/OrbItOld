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
        //public int tileSize;
        //public Texture2D texture;

        public Tile()
        {
            tilecode = "0";
            color = Color.White;
            //tileSize = 50;
            //texture = null;
        }

        public Tile(String tilecode)
        {
            this.tilecode = tilecode;
            color = Color.White;
            //this.tileSize = tileSize;
            //this.texture = texture;
        }

        public void setValues(String tilecode)
        {
            this.tilecode = tilecode;
            //this.tileSize = tileSize;
            //this.texture = texture;
        }

    }
}
