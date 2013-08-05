using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt.LevelObjects
{
    public class Tileset
    {
        List<String> tilecodes;
        List<Texture2D> textures;
        Texture2D defaulttexture;

        public Tileset()
        {
            tilecodes = new List<String>();
            textures = new List<Texture2D>();
            //defaulttexture = new Texture2D
        }
        public Tileset(List<String> tilecodes, List<Texture2D> textures)
        {
            this.tilecodes = tilecodes;
            this.textures = textures;
        }

        public void createTileset(String codes, List<Texture2D> textures)
        {
            this.textures = textures;

            String[] codesarray = codes.Split(' ');
            tilecodes = new List<String>();
            for (int i = 0; i < codesarray.Length; i++)
            { 
                tilecodes.Add(codesarray[i]);
            }
        }

        public void setValues(List<String> tilecodes, List<Texture2D> textures)
        {
            this.tilecodes = tilecodes;
            this.textures = textures;
        }
        public void addTexture(String code, Texture2D texture)
        {
            //add code: if code already mapped, disallow
            tilecodes.Add(code);
            textures.Add(texture);
        }

        public Texture2D getTexture(String code)
        {
            for (int i = 0; i < tilecodes.Count; i++)
            {
                if (code.Equals(tilecodes[i]))
                    return textures[i];
            }
            return null;
            //return defaulttexture;
        }

        

    }
}
