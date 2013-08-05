using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


using OrbIt.LevelObjects;

namespace OrbIt.GameObjects {
    public class GameObject {

        


        //public abstract void Initialize();

        public virtual void Update(GameTime gametime) { }

        public virtual void Draw(SpriteBatch spritebatch) { }

    }
}
