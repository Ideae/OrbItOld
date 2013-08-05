using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OrbIt.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrbIt.LevelObjects {
    public class Room {


        /*
        public List<List<GameObject>> GameObjectLists = new List<List<GameObject>>();
        public List<Orb> orbs = new List<Orb>();
        public List<GravityNode> gnodes = new List<GravityNode>();
        public List<RepelNode> rnodes = new List<RepelNode>();
        public List<SlowNode> snodes = new List<SlowNode>();
        public List<TransferNode> tnodes = new List<TransferNode>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Orb> bullets = new List<Orb>();
        public List<Laser> lasers = new List<Laser>();
        public List<Orb> beamorbs = new List<Orb>();
        */
        //Test
        public Game1 game1;
        public Camera camera;
        public Level level;
        public Tileset tileset;
        public Player player1;
        

        public Dictionary<string, List<GameObject>> GameObjectDict = new Dictionary<string, List<GameObject>>();
        
        
        public Dictionary<string, bool> PropertiesDict = new Dictionary<string, bool>();
        
        

        public Room()
        {
            setDefaultLists();
            setDefaultProperties();
            camera = new Camera();
        
        }

        public Room(Game1 game)
        {
            setDefaultLists();
            setDefaultProperties();
            camera = new Camera();

        }

        public void addList(string name, List<GameObject> list)
        {
            GameObjectDict.Add(name, list);
        }

        public void setDefaultLists()
        {

            addList("orbs", new List<GameObject>());
            addList("gnodes", new List<GameObject>());
            addList("rnodes", new List<GameObject>());
            addList("snodes", new List<GameObject>());
            addList("tnodes", new List<GameObject>());
            //List<GameObject> enemies = new List<GameObject>(); addList("enemies", enemies);
            //List<GameObject> bullets = new List<GameObject>(); addList("bullets", bullets);
            //List<GameObject> lasers = new List<GameObject>(); addList("lasers", lasers);
            //List<GameObject> beamorbs = new List<GameObject>(); addList("beamorbs", beamorbs);
        }

        public void setDefaultProperties()
        {
            PropertiesDict.Add("wallBounce", true);
            PropertiesDict.Add("gravityFreeze", false);
            PropertiesDict.Add("tempSlow", true);
            PropertiesDict.Add("mapOn", true);
            PropertiesDict.Add("collisionOn", true);
            PropertiesDict.Add("discoOn", false);
            PropertiesDict.Add("fixCollisionOn", true);
            PropertiesDict.Add("fullLightOn", false);
            PropertiesDict.Add("smallLightsOn", false);
            PropertiesDict.Add("bulletsOn", false);
        }


        public void Update(GameTime gametime)
        {
            foreach(KeyValuePair<string,List<GameObject>> entry in GameObjectDict) //use entry.key or entry.value
            {
                foreach (GameObject gameobject in entry.Value)
                {
                    gameobject.Update(gametime);
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (KeyValuePair<string, List<GameObject>> entry in GameObjectDict) //use entry.key or entry.value
            {
                foreach (GameObject gameobject in entry.Value)
                {
                    gameobject.Draw(spritebatch);
                }
            }
        
        }
    }
}
