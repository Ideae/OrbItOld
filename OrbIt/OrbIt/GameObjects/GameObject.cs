using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


using OrbIt.LevelObjects;

namespace OrbIt.GameObjects {
    public abstract class GameObject {

        


        //public abstract void Initialize();

        public abstract void Update(GameTime gametime);

        public abstract void Draw(SpriteBatch spritebatch);

    }
}
