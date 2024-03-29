﻿using System;
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



        public enum texNames { orangecircle, redcircle };
        
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
            player1 = new Player(this);
            player1.radius = 25;
            game1 = game;

        }

        public void Update(GameTime gametime)
        {


            //Update every gameobject from every list in the GameObjectDict
            foreach(KeyValuePair<string,List<GameObject>> entry in GameObjectDict) //use entry.key or entry.value
            {
                foreach (GameObject gameobject in entry.Value)
                {
                    //if (PropertiesDict["collisionOn"])
                        checkLevelObjectCollisions(gameobject);
                    gameobject.Update(gametime);
                }
            }
            //remove inActive gameobjects
            foreach (KeyValuePair<string, List<GameObject>> entry in GameObjectDict) //use entry.key or entry.value
            {
                List<GameObject> toRemove = new List<GameObject>();
                foreach (GameObject gameobject in entry.Value)
                {
                    if (gameobject is MoveableObject)
                    {
                        MoveableObject mo = (MoveableObject)gameobject;
                        if (!mo.isActive)
                        {
                            toRemove.Add(gameobject);
                            //entry.Value.Remove(mo);
                        }
                    }
                }
                foreach (GameObject gameobject in toRemove)
                {
                    entry.Value.Remove(gameobject);
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

            for (int i = 0; i < level.tileAmount.X; i++)
            {
                for (int j = 0; j < level.tileAmount.Y; j++)
                {
                    //if the collision code is "1" (collidable). 
                    //add more cases later for different terrain
                    if (level.tile[i, j, 0].collisioncode.Equals("1"))
                    {
                        //Console.WriteLine(i + "  " + j);
                        int x = i * (int)level.tileLength.X;
                        int y = j * (int)level.tileLength.Y;
                        //bounding box for tile
                        Rectangle box = new Rectangle(x, y, (int)level.tileLength.X, (int)level.tileLength.Y);

                        //spritebatch.Draw(game1.textureDict[OrbIt.Game1.tn.whitetile], box, Color.Blue);
                        
                    }
                }
            }

        
        }
        //Check if any game objects collide with any collidable tiles on the map(level)
        public void checkLevelObjectCollisions(GameObject gameobject)
        {
            if (gameobject is MoveableObject)
            {
                MoveableObject mo = (MoveableObject)gameobject;
                //for all the tiles
                for (int i = 0; i < level.tileAmount.X; i++)
                {
                    for (int j = 0; j < level.tileAmount.Y; j++)
                    {
                        //if the collision code is "1" (collidable). 
                        //add more cases later for different terrain
                        if (level.tile[i, j, 0].collisioncode.Equals("1"))
                        {
                            int x = i * (int)level.tileLength.X;
                            int y = j * (int)level.tileLength.Y;
                            //bounding box for tile
                            Rectangle box = new Rectangle(x, y,(int)level.tileLength.X, (int)level.tileLength.Y);


                            if (Utils.intersectCircleRect(mo, box.Left, box.Bottom, box.Right, box.Bottom, box.Right, box.Top, box.Left, box.Top))
                            {
                                Console.WriteLine("Side");
                            }
                            if (Utils.intersectCircleRectCorners(mo, box.Left, box.Bottom, box.Right, box.Bottom, box.Right, box.Top, box.Left, box.Top))
                            {
                                Console.WriteLine("Corner");
                            }
                        }
                    }
                }
            
            }
        }

        public void setDefaultLists()
        {
            addList("orbs", new List<GameObject>());
            addList("gnodes", new List<GameObject>());
            addList("rnodes", new List<GameObject>());
            addList("snodes", new List<GameObject>());
            addList("tnodes", new List<GameObject>());
            addList("ranodes", new List<GameObject>());
            addList("enemies", new List<GameObject>());
            addList("bullets", new List<GameObject>());
            //addList("lasers", new List<GameObject>());
            //addList("beamorbs", new List<GameObject>());
        }

        public void addList(string name, List<GameObject> list)
        {
            GameObjectDict.Add(name, list);
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
            PropertiesDict.Add("enemiesOn", false);
            PropertiesDict.Add("friction", false);
        }
    }
}
