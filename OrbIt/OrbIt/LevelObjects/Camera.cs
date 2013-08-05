using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OrbIt.LevelObjects {
    public class Camera {
        public Vector2 position;
        public float z;

        public Camera()
        {
            position = new Vector2(0, 0);
        }
    }
}
