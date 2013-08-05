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

        Player player1;
        Camera camera;

        Texture2D plr1Texture;
        Texture2D gravTexture;
        Texture2D orbTexture;
        Texture2D chainballTexture;
        Texture2D repelTexture;
        Texture2D slowTexture;
        Texture2D transferTexture;
        Texture2D grassTexture;
        Texture2D whiteTexture;
        Texture2D whiteCircle;
        Texture2D whiteLaser;

        public Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();

        public int screenWidth;
        public int screenHeight;

        //Laser laser1 = new Laser();

        public float gravMult, gravRad, plrVelMult, orbVelMult;
        public float repelMult, repelRad;
        public float permSlowMult, tempSlowMult, slowRad;
        public float transferRad;
        public float initialOrbRadius;
        public bool wallBounce, gravityFreeze, tempSlow, collisionOn, mapOn,discoOn,fixCollisionOn, bulletsOn;
        public bool fullLightOn, smallLightsOn;

        Random rand;
       
        public ChainBall cb1 = new ChainBall();
        public List<RepelNode> rnodes = new List<RepelNode>();
        public List<Orb> orbs = new List<Orb>();
        public List<SlowNode> snodes = new List<SlowNode>();
        public List<GravityNode> gnodes = new List<GravityNode>();
        public List<TransferNode> tnodes = new List<TransferNode>();
        public List<Enemy> enemies = new List<Enemy>();
        public List<Orb> bullets = new List<Orb>();
        public List<Laser> lasers = new List<Laser>();
        public List<Orb> beamorbs = new List<Orb>();

        public LightSource lightsource;
        
        
        public List<LightSource> lights = new List<LightSource>();

        public IPhysicsObject block = new Block();

        public Level level1;
        public Tileset tileset1;

        public GameTime gametime = new GameTime();
        TimeSpan timespan = new TimeSpan();

        public int respawnTimer,totalMilliseconds,totalMicroSeconds;

        private MouseState oldState;
        int weaponNumber;

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

            //Console.WriteLine(block);
            camera = new Camera();
            camera.position = new Vector2(0,0);
            
            block.Position = new Vector2(100, 200);
            block.Position = new Vector2(100, block.Position.Y);
            
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

            wallBounce = true;      // causes orbs to bounce off walls
            gravityFreeze = false;  // causes orbs to freeze when there are no gravitynodes spawned
            tempSlow = true;
            mapOn = true;
            collisionOn = true;
            discoOn = false;
            fixCollisionOn = true;
            rand = new Random();
            targetOrb = null;
            fullLightOn = false;
            smallLightsOn = false;
            bulletsOn = false;

            player1 = new Player();
            player1.radius = 25.0f;
            player1.position = new Vector2(screenWidth / 2, screenHeight / 2);
            player1.VelMultiplier = plrVelMult;

            plr1Texture = Content.Load<Texture2D>("bluesphere");
            gravTexture = Content.Load<Texture2D>("greensphere");
            orbTexture = Content.Load<Texture2D>("orangesphere");
            chainballTexture = Content.Load<Texture2D>("redsphere");
            repelTexture = Content.Load<Texture2D>("purplesphere");
            slowTexture = Content.Load<Texture2D>("yellowsphere");
            transferTexture = Content.Load<Texture2D>("pinksphere");
            grassTexture = Content.Load<Texture2D>("grass");
            whiteTexture = Content.Load<Texture2D>("whitetile");
            whiteCircle = Content.Load<Texture2D>("whitecircle");
            whiteLaser = Content.Load<Texture2D>("whitelaser");
            //enemyTexture = Content.Load<Texture2D>("enemy1");

            textureDict.Add("bluesphere", Content.Load<Texture2D>("bluesphere"));
            textureDict.Add("greensphere", Content.Load<Texture2D>("greensphere"));
            textureDict.Add("orangesphere", Content.Load<Texture2D>("orangesphere"));
            textureDict.Add("redsphere", Content.Load<Texture2D>("redsphere"));
            textureDict.Add("purplesphere", Content.Load<Texture2D>("purplesphere"));
            textureDict.Add("yellowsphere", Content.Load<Texture2D>("yellowsphere"));
            textureDict.Add("pinksphere", Content.Load<Texture2D>("pinksphere"));
            textureDict.Add("grass", Content.Load<Texture2D>("grass"));
            textureDict.Add("whitetile", Content.Load<Texture2D>("whitetile"));
            textureDict.Add("whitecircle", Content.Load<Texture2D>("whitecircle"));
            textureDict.Add("whitelaser", Content.Load<Texture2D>("whitelaser"));
            textureDict.Add("enemy1", Content.Load<Texture2D>("enemy1"));

            //level1, tileset1
            List<Texture2D> txs = new List<Texture2D>();
            txs.Add(grassTexture);
            txs.Add(orbTexture);
            String txcodes = "A B";
            tileset1 = new Tileset();
            tileset1.createTileset(txcodes, txs);

            String[] mapTileCodes = new String[24];
            mapTileCodes[0] = "A A A B A B A B A B A B A B A B A B A B";
            mapTileCodes[1] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[2] = "A B A B A B A B A B A B A B A B A B A B";
            mapTileCodes[3] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[4] = "A B A B A B A B A B A B A B A B A B A B";
            mapTileCodes[5] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[6] = "A B A B A B A B A B A B A B A B A B A B";
            mapTileCodes[7] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[8] = "A B A B A B A B A B A B A B A B A B A B";
            mapTileCodes[9] = "B A B A B A B A B A B A B A B A B A B A";
            mapTileCodes[10] = "A B A B A B A B A B A B A B A B A B A B";
            mapTileCodes[11] = "B A B A B A B A B A B A B A B A B A B A";
            for (int i = 0; i < mapTileCodes.Length/2; i++)
            {
                mapTileCodes[i] = mapTileCodes[i] + " " + mapTileCodes[i];
            }
            for (int i = 12; i < mapTileCodes.Length ; i++)
            {
                mapTileCodes[i] = mapTileCodes[i-12];
            }
            level1 = new Level();
            level1.readTiles(mapTileCodes);
            level1.tileset = tileset1;
            //level1 = new Level("Zack'sLevel",new Vector3(50, 50, 20),new Vector3(20, 12, 0), 

            player1.playerLight = new LightSource(0.5f, 0.5f, 0.5f, 0.6f, 0.0f, 1.0f, 0.0f, 100, whiteCircle);
            lightsource = new LightSource(0.5f, 0.5f, 0.5f, 0.6f, 0.0f, 300.0f, 1.0f, 100, whiteCircle);

            lightsource.position = new Vector2(500, 400);
            for (int i = 0; i < 300; i++)
            {
                LightSource light = new LightSource(0.5f, 0.5f, 0.5f, 0.9f, 0.0f, 3.0f, 1.0f, 100, whiteCircle);
                float randscale = ((float)rand.Next(100) / (float)100.0f) * (light.scaleRange * 2) -light.scaleRange;
                Console.WriteLine(randscale);
                light.scale += randscale;
                light.position = new Vector2(rand.Next(level1.levelwidth), rand.Next(level1.levelheight));
                light.scycle = true;
                light.setIncrements(0.1f, 0.1f, 0.1f, 0.1f, 0.1f);
                lights.Add(light);
            }
            
            cb1.position = player1.position;
            cb1.isActive = false;
            //Circle circle1 = (Circle)cb1.shape;
            //Console.WriteLine("RADIUS: " + circle1.radius);
            cb1.radius = 30.0f;
            cb1.ImageRadius = 10.0f;
            cb1.SpeedMultiplier = -2.0f;
            cb1.IsMoving = false;

            weaponNumber = 1;

            //gnodes.Add(new GravityNode(gravMult, gravRad, new Vector2(400, 400), true));
            //tnodes.Add(new TransferNode(10.0f, transferRad, new Vector2(500, 400), true));
            // TODO: use this.Content to load your game content here
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

            
            totalMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            
            //if (TimeSpan.FromSeconds(1) <= timespan) { level1.colorBool = true; timespan = new TimeSpan(); }
            if (level1.colorMode.Equals("disco"))
            {
                timespan += gameTime.ElapsedGameTime;
                if (timespan.Milliseconds > 10) { level1.colorBool = true; timespan = new TimeSpan(); }
            }
            if (totalMilliseconds > respawnTimer && totalMilliseconds != 0) 
            {
                timespan = new TimeSpan();
                Enemy newEnemy = new Enemy();
                
                newEnemy.Initialize(this);
                enemies.Add(newEnemy);
                //Console.WriteLine("rT: " + respawnTimer);
                respawnTimer -= (int)((respawnTimer-respawnTimer*0.2) * 0.05);
                totalMilliseconds = 0;
                //aConsole.WriteLine(respawnTimer);
            }
            if (shootTimer > 200 && totalMilliseconds != 0 && bulletsOn)
            {
                MouseState mouseState = Mouse.GetState();
                float mouseX = mouseState.X + camera.position.X;
                float mouseY = mouseState.Y + camera.position.Y;
                float playerCenterX = player1.position.X + (plr1Texture.Width) / 2;
                float playerCenterY = player1.position.Y + (plr1Texture.Height) / 2;
                Orb newBullet = new Orb();
                newBullet.setOrbValues(10.0f, 0.0f, 0.0f);
                Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                newBullet.InitOrb(angle2, player1.position);
                newBullet.setRadius(10.0f);
                bullets.Add(newBullet);
                shootTimer = 0;
                
            }

//lightsource.Cycle();
            if (fullLightOn)
            {
                lightsource.CycleRandColors(rand.Next(1000));
                //lightsource.CycleFlashing(rand.Next(1000));
            }
            if (smallLightsOn)
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].CycleRandColors(rand.Next(1000));
                    //lights[i].CycleFlashing(rand.Next(1000));
                }
            }
            player1.playerLight.CycleRandColors(rand.Next(1000));

            // TODO: Add your update logic here
            ProcessKeyboard();
            ProcessMouse();
            if (collisionOn)
            {
                collisionDetection();
            }

            if (player1.position.X + player1.radius > level1.levelwidth)
                player1.position.X = level1.levelwidth - player1.radius;
            if (player1.position.X - player1.radius < 0)
                player1.position.X = player1.radius;
            if (player1.position.Y + player1.radius > level1.levelheight)
                player1.position.Y = level1.levelheight - player1.radius;
            if (player1.position.Y - player1.radius < 0)
                player1.position.Y = player1.radius;


            //Console.WriteLine("OK");
            for (int i = 0; i < orbs.Count; i++)
            {
                if (orbs[i].isActive)
                {
                    //Apply gravity node effect
                    for (int j = 0; j < gnodes.Count; j++)
                    {
                        //orbs[i].ApplyGrav(gnodes[j]);
                        gnodes[j].ApplyEffect(orbs[i]);
                        if (gravityFreeze)
                        {
                            orbs[i].UpdateOrb();
                        }
                    }
                   
                    /*
                    funciton m () { return new Vector2(as *base, as*BadImageFormatException);
                    }

                  

                  position2222 =  velocity23.multiply(IsFixedTimeStep) 
                    */
                    
                    //Apply repel node effect
                    for (int j = 0; j < rnodes.Count; j++)
                    {
                        //orbs[i].ApplyRepel(rnodes[j]);
                        rnodes[j].ApplyEffect(orbs[i]);
                        if (gravityFreeze)
                        {
                            orbs[i].UpdateOrb();
                        }

                    }
                    for (int j = 0; j < snodes.Count; j++)
                    {
                        //orbs[i].ApplySlow(snodes[j]);
                        snodes[j].ApplyEffect(orbs[i]);
                        if (gravityFreeze)
                        {
                            orbs[i].UpdateOrb();
                        }

                    }
                    for (int j = 0; j < tnodes.Count; j++)
                    {
                        //orbs[i].ApplyTransfer(tnodes[j]);
                        tnodes[j].ApplyEffect(orbs[i]);
                        if (gravityFreeze)
                        {
                            orbs[i].UpdateOrb();
                        }

                    }
                    
                    if (!gravityFreeze)
                    {
                        orbs[i].UpdateOrb();
                    }
                    

                    if (wallBounce)
                    {
                        float halfWidth = orbTexture.Width / 2;
                        float halfHeight = orbTexture.Height / 2;

                        if (orbs[i].position.X >= (level1.levelwidth-orbs[i].radius)) 
                        {
                            orbs[i].position.X = level1.levelwidth - orbs[i].radius;
                            orbs[i].velocity.X *= -1;
                            orbs[i].Acceleration.X *= -1;
                        }
                        else if (orbs[i].position.X < orbs[i].radius)
                        {
                            orbs[i].position.X = orbs[i].radius;
                            orbs[i].velocity.X *= -1;
                            orbs[i].Acceleration.X *= -1;
                        }
                        else if (orbs[i].position.Y >= (level1.levelheight - orbs[i].radius)) 
                        {
                            orbs[i].position.Y = level1.levelheight - orbs[i].radius;
                            orbs[i].velocity.Y *= -1;
                            orbs[i].Acceleration.Y *= -1;
                        }
                        else if (orbs[i].position.Y < orbs[i].radius)
                        {
                            orbs[i].position.Y = orbs[i].radius;
                            orbs[i].velocity.Y *= -1;
                            orbs[i].Acceleration.Y *= -1;
                        }

                    }
                    else
                    {
                        if (orbs[i].position.X >= level1.levelwidth || orbs[i].position.X < 0 || orbs[i].position.Y >= level1.levelheight || orbs[i].position.Y < 0)
                        {
                            orbs[i].isActive = false;
                        }  
                    }


                    
                }
                else //if (!orbs[i].IsActive)
                {
                    orbs.RemoveAt(i);
                
                }

            }

            for (int i = 0; i < bullets.Count; i++ )
            {
                bullets[i].UpdateOrb();
                if (bullets[i].position.X >= level1.levelwidth || bullets[i].position.X < 0 || bullets[i].position.Y >= level1.levelheight || bullets[i].position.Y < 0)
                {
                    bullets[i].isActive = false;
                    bullets.RemoveAt(i);
                }

            }

            foreach (Enemy en  in enemies)
            {
                if (en.position.X < player1.position.X) { en.position.X += en.VelMultiplier; }
                else if (en.position.X > player1.position.X) { en.position.X -= en.VelMultiplier; }
                if (en.position.Y > player1.position.Y) { en.position.Y -= en.VelMultiplier; }
                else if (en.position.Y < player1.position.Y) { en.position.Y += en.VelMultiplier; }
            
            }

            for (int i = 0; i < lasers.Count; i++)
            {
                lasers[i].Update();
            }

            base.Update(gameTime);
        }

        public void collisionDetection()
        {
            for (int i = 0; i < orbs.Count - 1; i++)
            {
                for (int j = i+1; j < orbs.Count; j++)
                {
                    if (checkCollision(orbs[i], orbs[j]))
                    {
                        /*Console.WriteLine("Collision Occured.");
                        orbs[i].IsActive = false;
                        orbs[j].IsActive = false;
                         */
                        
                        //Console.WriteLine(orbs[i].mass + " " + orbs[j].mass);

                        //ELASTIC COLLISION RESOLUTION --- FUCK YEAH
                        //float orbimass = 1, orbjmass = 1;
                        //float orbRadius = 25.0f; //integrate this into the orb class
                        float distanceOrbs = (float)DistanceVectors(orbs[i].position, orbs[j].position);
                        Vector2 normal = (orbs[j].position - orbs[i].position) / distanceOrbs;
                        float pvalue = 2 * (orbs[i].velocity.X * normal.X + orbs[i].velocity.Y * normal.Y - orbs[j].velocity.X * normal.X - orbs[j].velocity.Y * normal.Y) / (orbs[i].mass + orbs[j].mass);
                        orbs[i].velocity.X = orbs[i].velocity.X - pvalue * orbs[j].mass * normal.X;
                        orbs[i].velocity.Y = orbs[i].velocity.Y - pvalue * orbs[j].mass * normal.Y;
                        orbs[j].velocity.X = orbs[j].velocity.X + pvalue * orbs[i].mass * normal.X;
                        orbs[j].velocity.Y = orbs[j].velocity.Y + pvalue * orbs[i].mass * normal.Y;
                        if (fixCollisionOn)
                            fixCollision(orbs[i], orbs[j]);
                        
                    }
                }
                for (int j = 0; j < gnodes.Count ; j++)
                {
                    if (checkCollisionOrbGrav(orbs[i],gnodes[j]))
                    {
                        //collision code

                    
                    }
                }
            }
            //check collision between bullets and enemies
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (checkCollisionOrbEnemy(bullets[i], enemies[j])) {
                        //bullets[i].isActive = false; 
                        enemies[j].hitpoints -= 1;
                        if (enemies[j].hitpoints <= 0)
                        {
                            enemies[j].IsAlive = false; player1.score += 1;
                        }
                    }
                }
            
            }

            for (int i = 0; i < bullets.Count; i++) if (bullets[i].isActive==false)bullets.RemoveAt(i);
            for (int j = 0; j < enemies.Count; j++) if (enemies[j].IsAlive == false) enemies.RemoveAt(j);
        
        }
        //make sure that if the orbs are stuck together, they are separated.
        public void fixCollision(Orb o1, Orb o2)
        {
            //float orbRadius = 25.0f; //integrate this into the orb class
            //if the orbs are still within colliding distance after moving away (fix radius variables)
            if (DistanceVectorsSquared(o1.position + o1.velocity, o2.position + o2.velocity) <= ((o1.radius * 2) * (o2.radius * 2)))
            {
                
                Vector2 difference = o1.position - o2.position; //get the vector between the two orbs
                float length = DistanceVectors(o1.position, o2.position);//get the length of that vector
                difference = difference / length;//get the unit vector
                //fix the below statement to get the radius' from the orb objects
                length = (o1.radius + o2.radius) - length; //get the length that the two orbs must be moved away from eachother
                difference = difference * length; // produce the vector from the length and the unit vector
                o1.position += difference / 2;
                o2.position -= difference / 2;
            }
            else return;
        }


        public bool checkCollision(Orb o1, Orb o2)
        {
            //float orbRadius = 25.0f;
            if (DistanceVectorsSquared(o1.position, o2.position) <= (((o1.radius) + (o2.radius)) * ((o1.radius) + (o2.radius))))
                return true;
            return false;
        
        }
        public bool checkCollisionOrbEnemy(Orb o1, Enemy en)
        {
            //float orbRadius = 25.0f;
            if (DistanceVectorsSquared(o1.position, en.position) <= (((o1.radius) + (en.radius)) * ((o1.radius) + (en.radius))))
                return true;
            return false;

        }

        public bool checkCollisionOrbGrav(Orb o1, GravityNode grav1)
        {
            float orbRadius = 25.0f; // grav radius is the same (for now)
            if (DistanceVectorsSquared(o1.position, grav1.position) <= ((orbRadius * 2) * (orbRadius * 2)))
                return true;
            return false;

        }
        //find projection of a onto b
        public Vector2 projection(Vector2 aa, Vector2 bb)
        {
            Vector2 proj = (Vector2.Dot(aa,bb)/bb.LengthSquared())*bb;
            return proj;
        
        }

        


        private void ProcessKeyboard()
        {
            float deltaX = 0.0f;
            float deltaY = 0.0f;
            KeyboardState keybState = Keyboard.GetState();
            //if () return;
            
            if (keybState.IsKeyDown(Keys.A))
                deltaX -= player1.VelMultiplier;   //player1.position.X -= player1.VelMultiplier;

            if (keybState.IsKeyDown(Keys.D))
                deltaX += player1.VelMultiplier; //player1.position.X += player1.VelMultiplier; 

            if (keybState.IsKeyDown(Keys.S))
                deltaY += player1.VelMultiplier; //player1.position.Y += player1.VelMultiplier; 

            if (keybState.IsKeyDown(Keys.W))
                deltaY -= player1.VelMultiplier; //player1.position.Y -= player1.VelMultiplier;
            //ensure that diagonal movement is the same speed as straight 'single key' movement
            if ((deltaX == player1.VelMultiplier || deltaX == -player1.VelMultiplier) && (deltaY == player1.VelMultiplier || deltaY == -player1.VelMultiplier))
            {
                float xneg = 1, yneg = 1;
                if (deltaX < 0) xneg = -1;
                if (deltaY < 0) yneg = -1;
                float tempdeltaX = (float)(Math.Cos(45) * player1.VelMultiplier) * xneg;
                float tempdeltaY = (float)(Math.Sin(45) * player1.VelMultiplier) * yneg;
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


            float xch = player1.position.X;
            float ych = player1.position.Y;
            player1.position.X += (int)deltaX;
            player1.position.Y += (int)deltaY;
            xch = player1.position.X - xch;
            ych = player1.position.Y - ych;
            int dx = (int)deltaX;
            int dy = (int)deltaY;
            //Console.WriteLine("xch: " + xch);
            // CAMERA                                  CAMERA MOVEMENT
            //if the player moved, move camera
            if (dx != 0 || dy != 0)
            {
                if (dx > 0 && player1.position.X > screenWidth / 2 && camera.position.X < (level1.levelwidth - screenWidth))
                {
                    camera.position.X += xch; //Console.WriteLine("position dx: " + deltaX); 
                }
                else if (dx < 0 && camera.position.X > 0 && player1.position.X < (level1.levelwidth - screenWidth/2))
                {
                    camera.position.X += xch; //Console.WriteLine("Neg dx: " + deltaX); 
                } 
                if (dy > 0 && player1.position.Y > screenHeight/2 && camera.position.Y < (level1.levelheight - screenHeight))
                    camera.position.Y += ych;
                else if (dy < 0 && camera.position.Y > 0 && player1.position.Y < (level1.levelheight - screenHeight/2))
                    camera.position.Y += ych;

                if (camera.position.X < 0) camera.position.X = 0;
                if (camera.position.Y < 0) camera.position.Y = 0;
            }

            float linex = player1.position.X - cb1.position.X;
            float liney = player1.position.Y - cb1.position.Y;

            //Processing ChainBall code:
            if (cb1.isActive)
            {
                if (DistanceVectors(player1.position, cb1.position) >= cb1.radius)
                {
                    float difference = DistanceVectors(player1.position, cb1.position) - cb1.radius;
                    cb1.IsMoving = true;
                    player1.VelMultiplier = 4.0f;
                    //Console.WriteLine("It happened.");

                }
                else if (DistanceVectors(player1.position, cb1.position) < cb1.radius)
                {
                    cb1.IsMoving = false;
                    player1.VelMultiplier = 6.0f;
                }
                if (cb1.IsMoving)
                {
                    //cb1.position.X += (int)deltaX;
                    //cb1.position.Y += (int)deltaY;
                    //float difference = DistanceVectors(player1.position, cb1.position) - cb1.radius;
                    cb1.position.X += (3 * linex) / cb1.radius;
                    cb1.position.Y += (3 * liney) / cb1.radius;
                }
            }

        }

        private void ProcessMouse()
        {
            MouseState mouseState = Mouse.GetState();
            float mouseX = mouseState.X + camera.position.X;
            float mouseY = mouseState.Y + camera.position.Y;
            float playerCenterX = player1.position.X + (plr1Texture.Width) / 2;
            float playerCenterY = player1.position.Y + (plr1Texture.Height) / 2;

            if (mouseState.X > graphics.PreferredBackBufferWidth) return;
            if (mouseState.Y > graphics.PreferredBackBufferHeight) return;

            if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                Double angleDelta = 0;
                
                if (weaponNumber == 1)
                {
                    Orb neworb1 = new Orb();
                    neworb1.setOrbValues(orbVelMult, 0.0f, 0.0f);
                    Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                    neworb1.InitOrb(angle2, player1.position);
                    //neworb1.radius = initialOrbRadius;
                    neworb1.setRadius(initialOrbRadius);
                    orbs.Add(neworb1);
                    //Console.WriteLine("WeaponNumber is 1. " + angle2);
                }
                else if (weaponNumber == 2)
                {


                    float distClick = DistanceVectors(player1.position, new Vector2(mouseX, mouseY));
                        Double distMultiplier = 200.0 - distClick;
                        angleDelta = ((distMultiplier / 200.0) / 5.0) * 3.14;
                        Orb neworb1 = new Orb();
                        neworb1.setOrbValues(5.0f, 0.0f, 0.0f);
                        Double angle2 = Math.Atan2((mouseY - player1.position.Y), (mouseX - player1.position.X)) - angleDelta;
                        neworb1.InitOrb(angle2, player1.position);
                        neworb1.setRadius(initialOrbRadius);
                        orbs.Add(neworb1);

                        Double angle3 = Math.Atan2((mouseY - player1.position.Y), (mouseX - player1.position.X)) + angleDelta;
                        Orb neworb2 = new Orb();
                        neworb2.setOrbValues(5.0f, 0.0f, 0.0f);
                        neworb2.InitOrb(angle3, player1.position + new Vector2(initialOrbRadius, initialOrbRadius));
                        neworb2.setRadius(initialOrbRadius);
                        orbs.Add(neworb2);
                        //Console.WriteLine("WeaponNumber is 2. (A) " + angle2);
                    
                }
                else if (weaponNumber == 3)
                {
                    gnodes.Add(new GravityNode(gravMult, gravRad, new Vector2(mouseX, mouseY), true, 25));
                }
                else if (weaponNumber == 4)
                {
                    rnodes.Add(new RepelNode(repelMult, repelRad, new Vector2(mouseX, mouseY), true, 25));
                }
                else if (weaponNumber == 5)
                {
                    float slowmult = 0.0f;
                    if (tempSlow) slowmult = tempSlowMult;
                    else slowmult = permSlowMult;
                        
                    snodes.Add(new SlowNode(slowmult, slowRad, new Vector2(mouseX, mouseY), true, 25));
                    snodes[snodes.Count-1].temporarySlow = tempSlow;
                }
                else if (weaponNumber == 6)
                {
                    tnodes.Add(new TransferNode(10.0f, transferRad, new Vector2(mouseX, mouseY), true, 25));
                }
                else if (weaponNumber == 7)
                {
                    Laser las = new Laser();
                    las.Intialize(player1.position, new Vector2(mouseX, mouseY), 3.0f);
                    lasers.Add(las);
                }
                else if (weaponNumber == 8)
                { }
                else if (weaponNumber == 11)
                {
                    Orb newBullet = new Orb();
                    newBullet.setOrbValues(10.0f, 0.0f, 0.0f);
                    Double angle2 = Math.Atan2((mouseY - playerCenterY), (mouseX - playerCenterX));
                    newBullet.InitOrb(angle2, player1.position);
                    newBullet.setRadius(20.0f);
                    bullets.Add(newBullet);
                    //---------------------
                    
                }
                else if (weaponNumber == 12)
                {
                    bool found = false;
                    for (int i = 0; i < orbs.Count; i++)
                    {
                        if (DistanceVectorsSquared(orbs[i].position, new Vector2(mouseX, mouseY)) < orbs[i].radius * orbs[i].radius)
                        {
                            targetOrb = orbs[i];
                            found = true;
                        }
                    }
                    if (!found) targetOrb = null;
                }


            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (weaponNumber == 1 || weaponNumber == 2)
                {
                    int halfWidth = orbTexture.Width / 2;
                    int halfHeight = orbTexture.Height / 2;
                    for (int i = 0; i < orbs.Count; i++)
                    {
                        if (mouseX >= orbs[i].position.X - orbs[i].radius && mouseX <= (orbs[i].position.X + orbs[i].radius)
                            && mouseY >= orbs[i].position.Y - orbs[i].radius && mouseY <= (orbs[i].position.Y + orbs[i].radius))
                        {
                            orbs.RemoveAt(i);
                        }
                    }
                }

                if (weaponNumber == 3)
                {
                    int halfWidth = gravTexture.Width / 2;
                    int halfHeight = gravTexture.Height / 2;
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
                    int halfWidth = repelTexture.Width / 2;
                    int halfHeight = repelTexture.Height / 2;
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
                    int halfWidth = slowTexture.Width / 2;
                    int halfHeight = slowTexture.Height / 2;
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
                    int halfWidth = transferTexture.Width / 2;
                    int halfHeight = transferTexture.Height / 2;
                    for (int i = 0; i < tnodes.Count; i++)
                    {
                        if (mouseX >= tnodes[i].position.X - halfWidth && mouseX <= (tnodes[i].position.X + halfWidth)
                            && mouseY >= tnodes[i].position.Y - halfHeight && mouseY <= (tnodes[i].position.Y + halfHeight))
                        {
                            tnodes.RemoveAt(i);
                        }
                    }
                }
            }
            oldState = mouseState;
        }



        private void ApplyChainBall()
        {
            if (cb1.isActive)
            {
                if (DistanceVectors(player1.position, cb1.position) >= cb1.radius)
                {
                    cb1.IsMoving = true;
                    player1.VelMultiplier = 2.0f;
                    //Console.WriteLine("It happened.");

                }
                else if (DistanceVectors(player1.position, cb1.position) < cb1.radius)
                {
                    cb1.IsMoving = false;
                    player1.VelMultiplier = 6.0f;
                }

            }
        }

        private float DistanceVectors(Vector2 v1, Vector2 v2)
        {
            float dist = (float)Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
            return dist;
        }

        private float DistanceVectorsSquared(Vector2 v1, Vector2 v2)
        {
            return (float)((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            if (mapOn)
            level1.Draw(spriteBatch,camera);

            spriteBatch.Draw(plr1Texture, player1.position - camera.position, null, Color.White, 0, new Vector2(plr1Texture.Width / 2, plr1Texture.Height / 2), 1, SpriteEffects.None, 0);
            //draw the light sources
            if (smallLightsOn)
            {
                for (int i = 0; i < lights.Count; i++)
                {
                    lights[i].Draw(spriteBatch, lights[i].position - camera.position);
                }
            }
            if (targetOrb != null)
                spriteBatch.Draw(orbTexture, targetOrb.position - camera.position, null, Color.White, 0, new Vector2(orbTexture.Width / 2, orbTexture.Height / 2), 3.0f, SpriteEffects.None, 0);

            for (int i = 0; i < orbs.Count; i++)
            {
                if (orbs[i].isActive)
                {
                    if (orbs[i].radius != orbTexture.Width / 2)
                    {
                        float scale = orbs[i].radius / (orbTexture.Width / 2);
                        spriteBatch.Draw(orbTexture, orbs[i].position - camera.position, null, Color.White, 0, new Vector2(orbTexture.Width / 2, orbTexture.Height / 2), scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(orbTexture, orbs[i].position - camera.position, null, Color.White, 0, new Vector2(orbTexture.Width / 2, orbTexture.Height / 2), 1, SpriteEffects.None, 0);
                    }
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isActive)
                {
                    if (bullets[i].radius != orbTexture.Width / 2)
                    {
                        float scale = bullets[i].radius / (orbTexture.Width / 2);
                        spriteBatch.Draw(chainballTexture, bullets[i].position - camera.position, null, Color.White, 0, new Vector2(orbTexture.Width / 2, orbTexture.Height / 2), scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(chainballTexture, bullets[i].position - camera.position, null, Color.White, 0, new Vector2(orbTexture.Width / 2, orbTexture.Height / 2), 1, SpriteEffects.None, 0);
                    }
                }
 
            }

            for (int i = 0; i < gnodes.Count; i++)
            {
                spriteBatch.Draw(gravTexture, gnodes[i].position - camera.position, null, Color.White, 0, new Vector2(gravTexture.Width / 2, gravTexture.Height / 2), 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < rnodes.Count; i++)
            {
                spriteBatch.Draw(repelTexture, rnodes[i].position - camera.position, null, Color.White, 0, new Vector2(repelTexture.Width / 2, repelTexture.Height / 2), 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < snodes.Count; i++)
            {
                spriteBatch.Draw(slowTexture, snodes[i].position - camera.position, null, Color.White, 0, new Vector2(slowTexture.Width / 2, slowTexture.Height / 2), 1, SpriteEffects.None, 0);
            }
            for (int i = 0; i < tnodes.Count; i++)
            {
                spriteBatch.Draw(transferTexture, tnodes[i].position - camera.position, null, Color.White, 0, new Vector2(transferTexture.Width / 2, transferTexture.Height / 2), 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                spriteBatch.Draw(transferTexture, enemies[i].position - camera.position, null, Color.White, 0, new Vector2(transferTexture.Width / 2, transferTexture.Height / 2), 1, SpriteEffects.None, 0);
            }

            for (int i = 0; i < lasers.Count; i++)
            {
                lasers[i].Draw(spriteBatch, whiteLaser,camera);
            }

            if (cb1.isActive)
            {
                spriteBatch.Draw(chainballTexture, cb1.position - camera.position, null, Color.White, 0, new Vector2(chainballTexture.Width / 2, chainballTexture.Height / 2), 1, SpriteEffects.None, 0);
            }
            //player sprite
            
            //block, an interface testing object
            //spriteBatch.Draw(plr1Texture, block.position - camera.position, null, Color.White, 0, new Vector2(plr1Texture.Width / 2, plr1Texture.Height / 2), 1, SpriteEffects.None, 0);

            //DRAW THE INDICATOR ORBS IN THE CORNER OF THE SCREEN (so you know which weapon is selected)
            if (weaponNumber == 1) 
            { 
                spriteBatch.Draw(orbTexture, new Vector2(0, 0), Color.White); 
            }
            else if (weaponNumber == 2)
            {
                spriteBatch.Draw(orbTexture, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(orbTexture, new Vector2(orbTexture.Width, 0), Color.White);
            }
            else if (weaponNumber == 3)
            {
                spriteBatch.Draw(gravTexture, new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 4)
            {
                spriteBatch.Draw(repelTexture, new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 5)
            {
                spriteBatch.Draw(slowTexture, new Vector2(0, 0), Color.White);
            }
            else if (weaponNumber == 6)
            {
                spriteBatch.Draw(transferTexture, new Vector2(0, 0), Color.White);
            }

            //Color c1 = new Color(100.0f, 200.0f, 0.0f, 0.5f);
            //spriteBatch.Draw(whiteCircle, new Vector2(300,300), null, c1, 0, new Vector2(50,50), 1, SpriteEffects.None, 0);
            //if (lightsource.col.R 
            if (fullLightOn)
            {
                lightsource.Draw(spriteBatch, lightsource.position - camera.position);
            }
            player1.playerLight.Draw(spriteBatch, player1.position - camera.position);

            Rectangle rect = new Rectangle(200, 200, weaponNumber*10, 10);
            //spriteBatch.Draw(transferTexture, rect, Color.White);
            spriteBatch.Draw(whiteLaser, rect, null, Color.Red, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
