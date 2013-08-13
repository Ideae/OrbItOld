using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using OrbIt.GameObjects;
using OrbIt.LevelObjects;

//using System.Drawing.Drawing2D;


namespace OrbIt
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        Orb targetOrb;

        public Room room1;
        public Camera camera;

        
        //texture names
        public enum tn {bluesphere,greensphere,orangesphere,redsphere,purplesphere,yellowsphere,pinksphere,grass,whitetile,whitecircle,whitelaser,enemy1};

        public Dictionary<tn, Texture2D> textureDict = new Dictionary<tn, Texture2D>();

        public int screenWidth;
        public int screenHeight;

        public float gravMult, gravRad, plrVelMult, orbVelMult;
        public float repelMult, repelRad;
        public float permSlowMult, tempSlowMult, slowRad;
        public float transferRad;
        public float initialOrbRadius;

        public bool mouseMovementOn;

        Random rand;
       
        public ChainBall cb1 = new ChainBall();

        public LightSource lightsource;
        public List<LightSource> lights = new List<LightSource>();


        public GameTime gametime = new GameTime();
        TimeSpan timespan = new TimeSpan();

        public int respawnTimer,totalMilliseconds,totalMicroSeconds;

        private MouseState oldState;
        int weaponNumber;

        Rectangle rect1;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Zack's OrbIt";
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            


            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(0, 0);

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            gravMult = 1000.0f;  // Gravity Multiplier
            gravRad = 400.0f;    // Gravity Radius
            repelMult = 1000.0f; // Repel Multiplier
            repelRad = 400.0f;   // Repel Radius
            permSlowMult = 0.01f;    // Slow Multiplier
            tempSlowMult = 0.6f;
            slowRad = 75.0f;    // Slow Radius
            plrVelMult = 5.0f;  // Player Velocity Multiplier
            orbVelMult = 3.0f;   // Orb Velocity Multiplier
            transferRad = 50.0f; //transfer radius
            initialOrbRadius = 25.0f; //initial orb radius when firing orbs

            respawnTimer = 1500;

            rand = new Random();
            targetOrb = null;

            room1 = new Room(this);

            rect1 = new Rectangle(100, 100, 200, 200);
            



            

            camera = new Camera();
            camera.position = new Vector2(0, 0);

            mouseMovementOn = false;


            //room1.player1 = new Player(room1);
            room1.player1.radius = 25.0f;
            room1.player1.position = new Vector2(screenWidth / 2, screenHeight / 2);
            room1.player1.VelMultiplier = plrVelMult;


            textureDict.Add(tn.bluesphere, Content.Load<Texture2D>("bluesphere"));
            textureDict.Add(tn.greensphere, Content.Load<Texture2D>("greensphere"));
            textureDict.Add(tn.orangesphere, Content.Load<Texture2D>("orangesphere"));
            textureDict.Add(tn.redsphere, Content.Load<Texture2D>("redsphere"));
            textureDict.Add(tn.purplesphere, Content.Load<Texture2D>("purplesphere"));
            textureDict.Add(tn.yellowsphere, Content.Load<Texture2D>("yellowsphere"));
            textureDict.Add(tn.pinksphere, Content.Load<Texture2D>("pinksphere"));
            textureDict.Add(tn.grass, Content.Load<Texture2D>("grass"));
            textureDict.Add(tn.whitetile, Content.Load<Texture2D>("whitetile"));
            textureDict.Add(tn.whitecircle, Content.Load<Texture2D>("whitecircle"));
            textureDict.Add(tn.whitelaser, Content.Load<Texture2D>("whitelaser"));
            textureDict.Add(tn.enemy1, Content.Load<Texture2D>("enemy1"));

            //level1, tileset1
            List<Texture2D> txs = new List<Texture2D>();
            txs.Add(textureDict[tn.whitetile]);
            txs.Add(textureDict[tn.whitecircle]);
            String txcodes = "A B";
            String clcodes = "0 1";
            room1.tileset = new Tileset();
            room1.tileset.createTileset(txcodes, clcodes, txs);

            String[] mapTileCodes = new String[24];
            //mapTileCodes[0] = "A B A B A B A B A B A B A B A B A B A B";
            //mapTileCodes[1] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[0] = "A A B A A A A A A A A A A A A A A A A A";
            mapTileCodes[1] = "A A B A A A A A B A A A A A A A A A A A";
            mapTileCodes[2] = "A A B A A A A A A A A A A A A A A A A A";
            mapTileCodes[3] = "A A B A A A A A A A A A A A A B A A A A";
            mapTileCodes[4] = "A A B A A A A A A A A A A A A A A A A A";
            mapTileCodes[5] = "A A B A A A A A A A A A A A A A A A A A";
            mapTileCodes[6] = "A A A A A B A A A A A A A A A A A A A A";
            mapTileCodes[7] = "A A A A A A A A A A A A A A A A A A A A";
            mapTileCodes[8] = "A A A A A A A A A A A A A A A A A A A A";
            mapTileCodes[9] = "A A A A A A A A A B A A A A A A A A A A";
            mapTileCodes[10] = "A A A A A A A A A A A A A A A A A A A A";
            mapTileCodes[11] = "A A A A A A A A A A A A A A A A A A A A";
            

            for (int i = 12; i < mapTileCodes.Length; i++)
            {
                mapTileCodes[i] = mapTileCodes[i-12];
                /*
                if (i % 2 == 0)
                    mapTileCodes[i] = mapTileCodes[0];
                else
                    mapTileCodes[i] = mapTileCodes[1];
                */
            }
            for (int i = 0; i < mapTileCodes.Length; i++)
            {
                //mapTileCodes[i] = mapTileCodes[i] + " " + mapTileCodes[i];

            }

            room1.level = new Level();
            room1.level.tileLength = new Vector3(50, 50, 20);
            room1.level.tileset = room1.tileset;
            room1.level.readTiles(mapTileCodes);
            
            //level1 = new Level("Zack'sLevel",new Vector3(50, 50, 20),new Vector3(20, 12, 0), 




            room1.player1.playerLight = new LightSource(0.5f, 0.5f, 0.5f, 0.6f, 0.0f, 1.0f, 0.0f, 100, textureDict[tn.whitecircle]);
            lightsource = new LightSource(0.5f, 0.5f, 0.5f, 0.6f, 0.0f, 300.0f, 1.0f, 100, textureDict[tn.whitecircle]);
            lightsource.colorMode = "randomColors";
            lightsource.position = new Vector2(500, 400);
            for (int i = 0; i < 300; i++)
            {
                LightSource light = new LightSource(0.5f, 0.5f, 0.5f, 0.9f, 0.0f, 3.0f, 1.0f, 100, textureDict[tn.whitecircle]);
                float randscale = ((float)rand.Next(100) / (float)100.0f) * (light.scaleRange * 2) -light.scaleRange;
                //Console.WriteLine(randscale);
                light.scale += randscale;
                light.position = new Vector2(rand.Next(room1.level.levelwidth), rand.Next(room1.level.levelheight));
                light.scycle = true;
                light.setIncrements(0.1f, 0.1f, 0.1f, 0.1f, 0.1f);
                lights.Add(light);
            }
            /*
            cb1.position = player1.position;
            cb1.isActive = false;
            cb1.radius = 30.0f;
            cb1.ImageRadius = 10.0f;
            cb1.SpeedMultiplier = -2.0f;
            cb1.IsMoving = false;
            */
            weaponNumber = 1;

           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        public int shootTimer = 0;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) //xupdate
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            room1.Update(gameTime);
            room1.player1.Update(gameTime);

            totalMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            
            //if (TimeSpan.FromSeconds(1) <= timespan) { level1.colorBool = true; timespan = new TimeSpan(); }
            if (room1.level.colorMode.Equals("disco"))
            {
                timespan += gameTime.ElapsedGameTime;
                if (timespan.Milliseconds > 10) { room1.level.colorBool = true; timespan = new TimeSpan(); }
            }
            if (totalMilliseconds > respawnTimer && totalMilliseconds != 0) 
            {
                timespan = new TimeSpan();
                Enemy newEnemy = new Enemy(room1);
                
                newEnemy.Initialize(this);
                room1.GameObjectDict["enemies"].Add(newEnemy);
                //Console.WriteLine("rT: " + respawnTimer);
                respawnTimer -= (int)((respawnTimer-respawnTimer*0.2) * 0.05);
                totalMilliseconds = 0;
                //aConsole.WriteLine(respawnTimer);
            }
            if (shootTimer > 200 && totalMilliseconds != 0 && room1.PropertiesDict["enemiesOn"])
            {
                MouseState mouseState = Mouse.GetState();
                float mouseX = mouseState.X + camera.position.X;
                float mouseY = mouseState.Y + camera.position.Y;
                float playerCenterX = room1.player1.position.X + (textureDict[tn.bluesphere].Width) / 2;
                float playerCenterY = room1.player1.position.Y + (textureDict[tn.bluesphere].Height) / 2;
                Orb newBullet = new Orb(room1);
                newBullet.setOrbValues(10.0f, 0.0f, 0.0f);
                Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                newBullet.InitOrb(angle2, room1.player1.position);
                newBullet.setRadius(10.0f);
                room1.GameObjectDict["bullets"].Add(newBullet);
                shootTimer = 0;
                
            }

//lightsource.Cycle();
            if (room1.PropertiesDict["fullLightOn"])
            //if (true)
            {
                lightsource.CycleRandColors(rand.Next(1000));
                //lightsource.CycleFlashing(rand.Next(1000));
            }
            if (room1.PropertiesDict["smallLightsOn"])
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].CycleRandColors(rand.Next(1000));
                    //lights[i].CycleFlashing(rand.Next(1000));
                }
            }
            room1.player1.playerLight.CycleRandColors(rand.Next(1000));

            // TODO: Add your update logic here
            ProcessKeyboard();
            ProcessMouse();

            base.Update(gameTime);
        }

        private void ProcessKeyboard()
        {
            float deltaX = 0.0f;
            float deltaY = 0.0f;
            KeyboardState keybState = Keyboard.GetState();
            //if () return;
            
            if (keybState.IsKeyDown(Keys.A))
                deltaX -= room1.player1.VelMultiplier;   //room1.player1.position.X -= room1.player1.VelMultiplier;

            if (keybState.IsKeyDown(Keys.D))
                deltaX += room1.player1.VelMultiplier; //room1.player1.position.X += room1.player1.VelMultiplier; 

            if (keybState.IsKeyDown(Keys.S))
                deltaY += room1.player1.VelMultiplier; //room1.player1.position.Y += room1.player1.VelMultiplier; 

            if (keybState.IsKeyDown(Keys.W))
                deltaY -= room1.player1.VelMultiplier; //room1.player1.position.Y -= room1.player1.VelMultiplier;
            //ensure that diagonal movement is the same speed as straight 'single key' movement
            if ((deltaX == room1.player1.VelMultiplier || deltaX == -room1.player1.VelMultiplier) && (deltaY == room1.player1.VelMultiplier || deltaY == -room1.player1.VelMultiplier))
            {
                float xneg = 1, yneg = 1;
                if (deltaX < 0) xneg = -1;
                if (deltaY < 0) yneg = -1;
                float tempdeltaX = (float)(Math.Cos(45) * room1.player1.VelMultiplier) * xneg;
                float tempdeltaY = (float)(Math.Sin(45) * room1.player1.VelMultiplier) * yneg;
                deltaX = tempdeltaX;
                deltaY = tempdeltaY;
            
            }

            if (keybState.IsKeyDown(Keys.D1))
            {
                weaponNumber = 1;
            }
            if (keybState.IsKeyDown(Keys.D2))
            {
                weaponNumber = 2;
            }
            if (keybState.IsKeyDown(Keys.D3))
            {
                weaponNumber = 3;
            }
            if (keybState.IsKeyDown(Keys.D4))
            {
                weaponNumber = 4;
            }
            if (keybState.IsKeyDown(Keys.D5))
            {
                weaponNumber = 5;
            }
            if (keybState.IsKeyDown(Keys.D6))
            {
                weaponNumber = 6;
            }
            if (keybState.IsKeyDown(Keys.D7))
            {
                weaponNumber = 7;
            }
            if (keybState.IsKeyDown(Keys.D8))
            {
                weaponNumber = 8;
            }
            if (keybState.IsKeyDown(Keys.D9))
            {
                weaponNumber = 9;
            }
            if (keybState.IsKeyDown(Keys.Q))
            {
                weaponNumber = 11;
            }
            if (keybState.IsKeyDown(Keys.T))
            {
                weaponNumber = 12;
            }
            if (keybState.IsKeyDown(Keys.Enter) && weaponNumber == 12 && targetOrb != null)
            {
                targetOrb.velocity = targetOrb.velocity / 2;
            }
            if (keybState.IsKeyDown(Keys.Space))
            {
                mouseMovementOn = true;
            }
            else
            {
                mouseMovementOn = false;
            }


            float xch = room1.player1.position.X;
            float ych = room1.player1.position.Y;
            room1.player1.position.X += (int)deltaX;
            room1.player1.position.Y += (int)deltaY;
            xch = room1.player1.position.X - xch;
            ych = room1.player1.position.Y - ych;
            int dx = (int)deltaX;
            int dy = (int)deltaY;
            //Console.WriteLine("xch: " + xch);
            // CAMERA                                  CAMERA MOVEMENT
            //if the player moved, move camera
            if (dx != 0 || dy != 0)
            {
                if (dx > 0 && room1.player1.position.X > screenWidth / 2 && camera.position.X < (room1.level.levelwidth - screenWidth))
                {
                    camera.position.X += xch; //Console.WriteLine("position dx: " + deltaX); 
                }
                else if (dx < 0 && camera.position.X > 0 && room1.player1.position.X < (room1.level.levelwidth - screenWidth/2))
                {
                    camera.position.X += xch; //Console.WriteLine("Neg dx: " + deltaX); 
                } 
                if (dy > 0 && room1.player1.position.Y > screenHeight/2 && camera.position.Y < (room1.level.levelheight - screenHeight))
                    camera.position.Y += ych;
                else if (dy < 0 && camera.position.Y > 0 && room1.player1.position.Y < (room1.level.levelheight - screenHeight/2))
                    camera.position.Y += ych;

                if (camera.position.X < 0) camera.position.X = 0;
                if (camera.position.Y < 0) camera.position.Y = 0;
            }

            
            

            //Processing ChainBall code:
            /*
             
            float linex = room1.player1.position.X - cb1.position.X;
            float liney = room1.player1.position.Y - cb1.position.Y;
            if (cb1.isActive)
            {
                if (DistanceVectors(room1.player1.position, cb1.position) >= cb1.radius)
                {
                    float difference = DistanceVectors(room1.player1.position, cb1.position) - cb1.radius;
                    cb1.IsMoving = true;
                    room1.player1.VelMultiplier = 4.0f;
                    //Console.WriteLine("It happened.");

                }
                else if (DistanceVectors(room1.player1.position, cb1.position) < cb1.radius)
                {
                    cb1.IsMoving = false;
                    room1.player1.VelMultiplier = 6.0f;
                }
                if (cb1.IsMoving)
                {
                    //cb1.position.X += (int)deltaX;
                    //cb1.position.Y += (int)deltaY;
                    //float difference = DistanceVectors(room1.player1.position, cb1.position) - cb1.radius;
                    cb1.position.X += (3 * linex) / cb1.radius;
                    cb1.position.Y += (3 * liney) / cb1.radius;
                }
            }
            */
        }

        private void ProcessMouse()
        {
            MouseState mouseState = Mouse.GetState();
            float mouseX = mouseState.X + camera.position.X;
            float mouseY = mouseState.Y + camera.position.Y;
            float playerCenterX = room1.player1.position.X + (textureDict[tn.bluesphere].Width) / 2;
            float playerCenterY = room1.player1.position.Y + (textureDict[tn.bluesphere].Height) / 2;

            


            if (mouseMovementOn)
            {
                Vector2 mousePos = new Vector2(mouseX, mouseY);
                Vector2 playerPos = new Vector2(playerCenterX, playerCenterY);
                float distv = Vector2.Distance(mousePos, playerPos);
                if (distv >= 150) distv = 150;
                if (distv <= 20) distv = 20;
                float nvx = (mouseX - playerCenterX) * 0.001f * distv;
                float nvy = (mouseY - playerCenterY) * 0.001f * distv;
                room1.player1.velocity.X = nvx;
                room1.player1.velocity.Y = nvy;
                //Console.WriteLine(distv);
            
            }

            if (mouseState.X > graphics.PreferredBackBufferWidth) return;
            if (mouseState.Y > graphics.PreferredBackBufferHeight) return;

            if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                Double angleDelta = 0;

                //Console.WriteLine("#Orbs: " + room1.GameObjectDict["orbs"].Count);

                if (weaponNumber == 1)
                {
                    /*
                    Orb neworb1 = new Orb(room1);
                    neworb1.setOrbValues(orbVelMult, 0.0f, 0.0f);
                    Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                    neworb1.InitOrb(angle2, room1.player1.position);
                    //neworb1.radius = initialOrbRadius;
                    neworb1.setRadius(initialOrbRadius);
                    room1.GameObjectDict["orbs"].Add(neworb1);
                    //Console.WriteLine("WeaponNumber is 1. " + angle2);
                    */
                    
                    PhaseOrb neworb1 = new PhaseOrb(room1,rand.Next(10000));
                    neworb1.setOrbValues(orbVelMult, 0.0f, 0.0f);
                    Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                    neworb1.InitOrb(angle2, room1.player1.position);
                    //neworb1.radius = initialOrbRadius;
                    neworb1.setRadius(initialOrbRadius);
                    room1.GameObjectDict["orbs"].Add(neworb1);
                    //Console.WriteLine("WeaponNumber is 1. " + angle2);
                }
                else if (weaponNumber == 2)
                {


                    float distClick = Vector2.Distance(room1.player1.position, new Vector2(mouseX, mouseY));
                        Double distMultiplier = 200.0 - distClick;
                        angleDelta = ((distMultiplier / 200.0) / 5.0) * 3.14;
                        Orb neworb1 = new Orb(room1);
                        neworb1.setOrbValues(5.0f, 0.0f, 0.0f);
                        Double angle2 = Math.Atan2((mouseY - room1.player1.position.Y), (mouseX - room1.player1.position.X)) - angleDelta;
                        neworb1.InitOrb(angle2, room1.player1.position);
                        neworb1.setRadius(initialOrbRadius);
                        room1.GameObjectDict["orbs"].Add(neworb1);

                        Double angle3 = Math.Atan2((mouseY - room1.player1.position.Y), (mouseX - room1.player1.position.X)) + angleDelta;
                        Orb neworb2 = new Orb(room1);
                        neworb2.setOrbValues(5.0f, 0.0f, 0.0f);
                        neworb2.InitOrb(angle3, room1.player1.position + new Vector2(initialOrbRadius, initialOrbRadius));
                        neworb2.setRadius(initialOrbRadius);
                        room1.GameObjectDict["orbs"].Add(neworb2);
                        //Console.WriteLine("WeaponNumber is 2. (A) " + angle2);
                    
                }
                else if (weaponNumber == 3)
                {
                    room1.GameObjectDict["gnodes"].Add(new GravityNode(gravMult, gravRad, new Vector2(mouseX, mouseY), true, 25,room1));
                }
                else if (weaponNumber == 4)
                {
                    room1.GameObjectDict["rnodes"].Add(new RepelNode(repelMult, repelRad, new Vector2(mouseX, mouseY), true, 25, room1));
                }
                else if (weaponNumber == 5)
                {
                    float slowmult = 0.0f;
                    if (room1.PropertiesDict["tempSlow"]) slowmult = tempSlowMult;
                    else slowmult = permSlowMult;

                    room1.GameObjectDict["snodes"].Add(new SlowNode(slowmult, slowRad, new Vector2(mouseX, mouseY), true, 25, room1));
                    if (room1.GameObjectDict["snodes"][room1.GameObjectDict["snodes"].Count - 1] is SlowNode)
                    {
                        SlowNode snode = (SlowNode)room1.GameObjectDict["snodes"][room1.GameObjectDict["snodes"].Count - 1];
                        snode.temporarySlow = room1.PropertiesDict["tempSlow"];
                    }
                }
                else if (weaponNumber == 6)
                {
                    room1.GameObjectDict["tnodes"].Add(new TransferNode(10.0f, transferRad, new Vector2(mouseX, mouseY), true, 25, room1));
                }
                else if (weaponNumber == 7)
                {
                    /*
                    Laser las = new Laser();
                    las.Intialize(room1.player1.position, new Vector2(mouseX, mouseY), 3.0f);
                    lasers.Add(las);
                    */
                }
                else if (weaponNumber == 8)
                {
                    room1.GameObjectDict["ranodes"].Add(new RightAngleNode(gravMult, gravRad, new Vector2(mouseX, mouseY), true, 25, room1));
                }
                else if (weaponNumber == 11)
                {
                    Orb newBullet = new Orb(room1);
                    newBullet.setOrbValues(10.0f, 0.0f, 0.0f);
                    Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                    newBullet.InitOrb(angle2, room1.player1.position);
                    newBullet.setRadius(20.0f);
                    room1.GameObjectDict["bullets"].Add(newBullet);
                    //---------------------
                    
                }

                //not sure if this target code will work after changing system (and casting as a Orb)
                else if (weaponNumber == 12)
                {
                    bool found = false;
                    for (int i = 0; i < room1.GameObjectDict["orbs"].Count; i++)
                    {
                        if (room1.GameObjectDict["orbs"][i] is Orb)
                        {
                            Orb morb = (Orb)room1.GameObjectDict["orbs"][i];
                            if (Vector2.DistanceSquared(morb.position, new Vector2(mouseX, mouseY)) < morb.radius * morb.radius)
                            {
                                targetOrb = morb;
                                found = true;
                            }
                        }
                    }
                    if (!found) targetOrb = null;
                }


            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (weaponNumber == 1 || weaponNumber == 2)
                {
                    int halfWidth = textureDict[tn.orangesphere].Width / 2;
                    int halfHeight = textureDict[tn.orangesphere].Height / 2;
                    for (int i = 0; i < room1.GameObjectDict["orbs"].Count; i++)
                    {
                        if (room1.GameObjectDict["orbs"][i] is Orb)
                        {
                            Orb morb = (Orb)room1.GameObjectDict["orbs"][i];

                            if (mouseX >= morb.position.X - morb.radius && mouseX <= (morb.position.X + morb.radius)
                                && mouseY >= morb.position.Y - morb.radius && mouseY <= (morb.position.Y + morb.radius))
                            {
                                room1.GameObjectDict["orbs"].RemoveAt(i);
                            }
                        }
                    }
                }


                //Going to disable right-click removal of objects for now.
                /*
                if (weaponNumber == 3)
                {
                    int halfWidth = textureDict[tn.greensphere].Width / 2;
                    int halfHeight = textureDict[tn.greensphere].Height / 2;
                    for (int i = 0; i < gnodes.Count; i++)
                    {
                        if (mouseX >= gnodes[i].position.X-halfWidth && mouseX <= (gnodes[i].position.X + halfWidth)
                            && mouseY >= gnodes[i].position.Y-halfHeight && mouseY <= (gnodes[i].position.Y + halfHeight))
                        {
                            gnodes.RemoveAt(i);
                        }
                    }
                }
                if (weaponNumber == 4)
                {
                    int halfWidth = textureDict[tn.purplesphere].Width / 2;
                    int halfHeight = textureDict[tn.purplesphere].Height / 2;
                    for (int i = 0; i < rnodes.Count; i++)
                    {
                        if (mouseX >= rnodes[i].position.X - halfWidth && mouseX <= (rnodes[i].position.X + halfWidth)
                            && mouseY >= rnodes[i].position.Y - halfHeight && mouseY <= (rnodes[i].position.Y + halfHeight))
                        {
                            rnodes.RemoveAt(i);
                        }
                    }
                }
                if (weaponNumber == 5)
                {
                    int halfWidth = textureDict[tn.yellowsphere].Width / 2;
                    int halfHeight = textureDict[tn.yellowsphere].Height / 2;
                    for (int i = 0; i < snodes.Count; i++)
                    {
                        if (mouseX >= snodes[i].position.X - halfWidth && mouseX <= (snodes[i].position.X + halfWidth)
                            && mouseY >= snodes[i].position.Y - halfHeight && mouseY <= (snodes[i].position.Y + halfHeight))
                        {
                            snodes.RemoveAt(i);
                        }
                    }
                }
                if (weaponNumber == 6)
                {
                    int halfWidth = textureDict[tn.pinksphere].Width / 2;
                    int halfHeight = textureDict[tn.pinksphere].Height / 2;
                    for (int i = 0; i < tnodes.Count; i++)
                    {
                        if (mouseX >= tnodes[i].position.X - halfWidth && mouseX <= (tnodes[i].position.X + halfWidth)
                            && mouseY >= tnodes[i].position.Y - halfHeight && mouseY <= (tnodes[i].position.Y + halfHeight))
                        {
                            tnodes.RemoveAt(i);
                        }
                    }
                }
                */
            }
            oldState = mouseState;
        }


        /*
        private void ApplyChainBall()
        {
            if (cb1.isActive)
            {
                if (DistanceVectors(room1.player1.position, cb1.position) >= cb1.radius)
                {
                    cb1.IsMoving = true;
                    room1.player1.VelMultiplier = 2.0f;
                    //Console.WriteLine("It happened.");

                }
                else if (DistanceVectors(room1.player1.position, cb1.position) < cb1.radius)
                {
                    cb1.IsMoving = false;
                    room1.player1.VelMultiplier = 6.0f;
                }

            }
        }
        */

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            

            if (room1.PropertiesDict["mapOn"])
            room1.level.Draw(spriteBatch,camera);

            


            
            //draw the light sources
            if (room1.PropertiesDict["smallLightsOn"])
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].Draw(spriteBatch, lights[i].position - camera.position);
                }
            }
            if (targetOrb != null)
                spriteBatch.Draw(textureDict[tn.orangesphere], targetOrb.position - camera.position, null, Color.White, 0, new Vector2(textureDict[tn.orangesphere].Width / 2, textureDict[tn.orangesphere].Height / 2), 3.0f, SpriteEffects.None, 0);


            room1.Draw(spriteBatch);
            spriteBatch.Draw(textureDict[tn.whitecircle], room1.player1.position - camera.position, null, Color.White, 0, new Vector2(textureDict[tn.bluesphere].Width / 2, textureDict[tn.bluesphere].Height / 2), 1, SpriteEffects.None, 0);
            /*
            if (cb1.isActive)
            {
                spriteBatch.Draw(chainballTexture, cb1.position - camera.position, null, Color.White, 0, new Vector2(chainballTexture.Width / 2, chainballTexture.Height / 2), 1, SpriteEffects.None, 0);
            }
            */
            //player sprite
            
            //block, an interface testing object
            //spriteBatch.Draw(plr1Texture, block.position - camera.position, null, Color.White, 0, new Vector2(plr1Texture.Width / 2, plr1Texture.Height / 2), 1, SpriteEffects.None, 0);

            //DRAW THE INDICATOR ORBS IN THE CORNER OF THE SCREEN (so you know which weapon is selected)
            if (weaponNumber == 1) 
            {
                spriteBatch.Draw(textureDict[tn.orangesphere], new Vector2(0, 0), Color.White); 
            }
            else if (weaponNumber == 2)
            {
                spriteBatch.Draw(textureDict[tn.orangesphere], new Vector2(0, 0), Color.White);
                spriteBatch.Draw(textureDict[tn.orangesphere], new Vector2(textureDict[tn.orangesphere].Width, 0), Color.White);
            }
            else if (weaponNumber == 3)
            {
                spriteBatch.Draw(textureDict[tn.greensphere], new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 4)
            {
                spriteBatch.Draw(textureDict[tn.purplesphere], new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 5)
            {
                spriteBatch.Draw(textureDict[tn.yellowsphere], new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 6)
            {
                spriteBatch.Draw(textureDict[tn.pinksphere], new Vector2(0, 0), Color.White);
            }

            //Color c1 = new Color(100.0f, 200.0f, 0.0f, 0.5f);
            //spriteBatch.Draw(whiteCircle, new Vector2(300,300), null, c1, 0, new Vector2(50,50), 1, SpriteEffects.None, 0);
            //if (lightsource.col.R 
            /*
            spriteBatch.Draw(textureDict[tn.whitetile], rect1, Color.Red);

            //if (Utils.pointInRectangle((int)room1.player1.position.X, (int)room1.player1.position.Y, rect1.X, rect1.Y, rect1.X + rect1.Width, rect1.Y, rect1.X + rect1.Width, rect1.Y + rect1.Height, rect1.X, rect1.Y + rect1.Width))
            if (Utils.pointInRectangle((int)room1.player1.position.X, (int)room1.player1.position.Y, rect1.Left, rect1.Bottom, rect1.Right, rect1.Bottom, rect1.Right, rect1.Top, rect1.Left, rect1.Top))
            {
                spriteBatch.Draw(textureDict[tn.whitetile], new Rectangle(400,400,20,20), Color.Blue);
            }
            if (Utils.intersectCircleRectCorners(room1.player1, rect1.Left, rect1.Bottom, rect1.Right, rect1.Bottom, rect1.Right, rect1.Top, rect1.Left, rect1.Top))
            {
                spriteBatch.Draw(textureDict[tn.whitetile], new Rectangle(450, 450, 20, 20), Color.Orange);
            }
            if (Utils.intersectCircleRect(room1.player1, rect1.Left, rect1.Bottom, rect1.Right, rect1.Bottom, rect1.Right, rect1.Top, rect1.Left, rect1.Top))
            {
                spriteBatch.Draw(textureDict[tn.whitetile], new Rectangle(500, 500, 20, 20), Color.Yellow);
            }
            */


            if (room1.PropertiesDict["fullLightOn"])
            {
                lightsource.Draw(spriteBatch, lightsource.position - camera.position);
            }
            room1.player1.playerLight.Draw(spriteBatch, room1.player1.position - camera.position);

            

            Rectangle rect = new Rectangle(200, 200, weaponNumber*10, 10);
            //spriteBatch.Draw(transferTexture, rect, Color.White);
            //spriteBatch.Draw(whiteLaser, rect, null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
