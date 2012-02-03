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

namespace CodenameHorror
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private KeyboardState keyState;
        private KeyboardState prevKeyState;
        private MouseState mState;
        private MouseState prevMState;
        public static Rectangle viewPort;
        private int screenWidth = 1280;
        private int screenHeight = 720;

        public TileMap currentMap = TileMap.LoadMap(7, 2,TileMap.Dimension.Overworld);

        GameState curGameState = 0;
        private enum GameState
        {
            Splash = 0,
            Instructions0 = 1,
            Instructions1 = 2,
            Instructions2 = 3,
            Game = 4,
            Died = 5
        }


        Player player;
        List<Entity> full_entity_list = new List<Entity>();
        public List<Corpse> corpse_list = new List<Corpse>();
        List<Sparker> sparker_list = new List<Sparker>();
        List<Rune> rune_list = new List<Rune>();
        List<Soul> soul_list = new List<Soul>();

        public List<Decal> blood_splat_list = new List<Decal>();
        
        List<MonsterSpawner> spawner_list = new List<MonsterSpawner>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ChangeResolution(screenWidth, screenHeight);
        }

        public void ChangeResolution(int width, int height)
        {
            int oldWidth = screenWidth;
            int oldHeight = screenHeight;
            this.screenWidth = width;
            this.screenHeight = height;
            TileMap.ChangeResolution(screenWidth, screenHeight);
            screenWidth -= screenWidth % TileMap.tileWidth;
            screenHeight -= screenHeight % TileMap.tileHeight;
            this.graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.PreferredBackBufferWidth = screenWidth;
            foreach (Entity e in full_entity_list)
                e.setPos(new Vector2((e.getPos().X / oldWidth) * screenWidth,
                              (e.getPos().Y / oldHeight) * screenHeight));
            viewPort = new Rectangle(0, 0, screenWidth, screenHeight);
            sparker_list.Clear();
            GenerateLavaSparkers();
            spawner_list.Clear();
            GenerateSpawners();
            this.graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected void loadTextures(ContentManager cmgr)
        {

            AssetManager.Player_Anim_Idle = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Idle"), 1, new Vector2(32, 32));
            AssetManager.Player_Anim_Walk = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Walk"), 10, new Vector2(32, 32));
            AssetManager.Player_Anim_Sweep = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Sweep"), 10, new Vector2(64, 32));
            AssetManager.Player_Anim_Stab = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Stab"), 7, new Vector2(32, 32));
            AssetManager.Player_Anim_Throw = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Throw"), 13, new Vector2(32, 32));
            AssetManager.Player_Anim_Draw = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Draw"), 17, new Vector2(32, 32));
            AssetManager.Player_Anim_Suicide = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Suicide"), 18, new Vector2(32, 64));
            AssetManager.Player_Anim_Death = new AnimSprite(cmgr.Load<Texture2D>("Player_Anim_Death"), 12, new Vector2(32, 32));


            AssetManager.Crawler_Anim_Idle = new AnimSprite(cmgr.Load<Texture2D>("Crawler_Anim_Idle"), 1, new Vector2(32, 32));
            AssetManager.Crawler_Anim_Walk = new AnimSprite(cmgr.Load<Texture2D>("Crawler_Anim_Walk"), 10, new Vector2(32, 32));
            AssetManager.Crawler_Anim_Attack = new AnimSprite(cmgr.Load<Texture2D>("Crawler_Anim_Attack"), 6, new Vector2(32, 32));
            AssetManager.Crawler_Anim_Death = new AnimSprite(cmgr.Load<Texture2D>("Crawler_Anim_Death"), 4, new Vector2(32, 32));


            AssetManager.Praet_Anim_Idle = new AnimSprite(cmgr.Load<Texture2D>("Praet_Anim_Idle"), 1, new Vector2(50, 50));
            AssetManager.Praet_Anim_Walk = new AnimSprite(cmgr.Load<Texture2D>("Praet_Anim_Walk"), 10, new Vector2(50, 50));
            AssetManager.Praet_Anim_Attack = new AnimSprite(cmgr.Load<Texture2D>("Praet_Anim_Attack"), 7, new Vector2(50, 50));
            AssetManager.Praet_Anim_Death = new AnimSprite(cmgr.Load<Texture2D>("Praet_Anim_Death"), 7, new Vector2(50, 50));

            AssetManager.Cherub_Anim_Idle = new AnimSprite(cmgr.Load<Texture2D>("Cherub_Anim_Idle"), 1, new Vector2(50, 50));
            AssetManager.Cherub_Anim_Walk = new AnimSprite(cmgr.Load<Texture2D>("Cherub_Anim_Walk"), 2, new Vector2(50, 50));
            AssetManager.Cherub_Anim_Attack = new AnimSprite(cmgr.Load<Texture2D>("Cherub_Anim_Attack"), 1, new Vector2(50, 50));
            AssetManager.Cherub_Anim_Death = new AnimSprite(cmgr.Load<Texture2D>("Cherub_Anim_Death"), 8, new Vector2(50, 50));

            AssetManager.Divider_Anim_Idle = new AnimSprite(cmgr.Load<Texture2D>("Divider_Anim_Idle"), 1, new Vector2(60, 60));
            AssetManager.Divider_Anim_Walk = new AnimSprite(cmgr.Load<Texture2D>("Divider_Anim_Walk"), 10, new Vector2(60, 60));
            //AssetManager.Divider_Anim_Attack = new AnimSprite(cmgr.Load<Texture2D>("Divider_Anim_Attack"), 7, new Vector2(60, 60));

            
            AssetManager.Praet_Texture_Corpse = cmgr.Load<Texture2D>("Praet_Texture_Corpse");
            AssetManager.Player_Texture_Corpse = cmgr.Load<Texture2D>("Player_Texture_Corpse");
            AssetManager.Crawler_Texture_Corpse = cmgr.Load<Texture2D>("Crawler_Texture_Corpse");
            //AssetManager.Cherub_Texture_Corpse = cmgr.Load<Texture2D>("Cherub_Texture_Corpse");


            AssetManager.InventoryBackground = cmgr.Load<Texture2D>("Area");
            
            
            AssetManager.Health_Bar_Empty = cmgr.Load<Texture2D>("Health_Bar_Empty");
            AssetManager.Health_Bar_Fill = cmgr.Load<Texture2D>("Health_Bar_Fill");

            TileMap.spriteSheet = cmgr.Load<Texture2D>("tilesheet");
            Sparker.lineTex = cmgr.Load<Texture2D>("line");

            AssetManager.Rune_Texture_Anchor = cmgr.Load<Texture2D>("Runes\\Soul\\Anchor");
            AssetManager.Rune_Texture_Link = cmgr.Load<Texture2D>("Runes\\Soul\\SoulLinkRune");
            AssetManager.Rune_Texture_Summon_Demon = cmgr.Load<Texture2D>("Runes\\Soul\\SummonDemonRune");
            AssetManager.Rune_Texture_Summon_Legion = cmgr.Load<Texture2D>("Runes\\Soul\\SummonLegionRune");
            AssetManager.Rune_Texture_Summon_Victims = cmgr.Load<Texture2D>("Runes\\Soul\\SummonVictimsRune");
            AssetManager.Rune_Texture_Swarm = cmgr.Load<Texture2D>("Runes\\Combat\\SwarmRune");
            AssetManager.Rune_Texture_Blood = cmgr.Load<Texture2D>("Runes\\Combat\\BloodRune");
            AssetManager.Rune_Texture_Rend = cmgr.Load<Texture2D>("Runes\\Combat\\RendRune");
            AssetManager.Rune_Texture_Vortex = cmgr.Load<Texture2D>("Runes\\Combat\\VortexRune");
            AssetManager.Rune_Texture_Freeze = cmgr.Load<Texture2D>("Runes\\Combat\\FreezeRune");
            AssetManager.Rune_Texture_Teleport = cmgr.Load<Texture2D>("Runes\\Teleport\\Teleport");
            AssetManager.Rune_Texture_Mark = cmgr.Load<Texture2D>("Runes\\Teleport\\Mark");

            AssetManager.Blood_Splat_01 = cmgr.Load<Texture2D>("Splatters\\BloodSplat01");
            AssetManager.Blood_Splat_02 = cmgr.Load<Texture2D>("Splatters\\BloodSplat02");
            AssetManager.Blood_Splat_03 = cmgr.Load<Texture2D>("Splatters\\BloodSplat03");

            AssetManager.Gib_01 = cmgr.Load<Texture2D>("Gibs\\Gib_01");
            AssetManager.Gib_02 = cmgr.Load<Texture2D>("Gibs\\Gib_02");
            AssetManager.Gib_03 = cmgr.Load<Texture2D>("Gibs\\Gib_03");
            AssetManager.Gib_04 = cmgr.Load<Texture2D>("Gibs\\Gib_04");

            AssetManager.SoulTexture = cmgr.Load<Texture2D>("Soul");
            AssetManager.SplashScreen = cmgr.Load<Texture2D>("Splash");
            AssetManager.InstructionsScreen0 = cmgr.Load<Texture2D>("InstructionsScreen0");
            AssetManager.InstructionsScreen1 = cmgr.Load<Texture2D>("InstructionsScreen1");
            AssetManager.InstructionsScreen2 = cmgr.Load<Texture2D>("InstructionsScreen2");
            AssetManager.DeathScreen = cmgr.Load<Texture2D>("DeathScreen");

       

        }

        public void PlaceRune(Rune rune, Vector2 position, Color sparkColor)
        {
            for (int i = 0; i < rune_list.Count; i++)
            {
                if (rune.GetType() == rune_list[i].GetType())
                    return;
            }
            rune.setPos(position);
            rune_list.Add(rune);
            Sparker sp = new Sparker(100, new Vector2(position.X + 32, position.Y + 32));
            sp.SetGradient(sparkColor, Color.Multiply(sparkColor, 0.5f));
            sp.Fire();
            Living.gameParent.GetSparkerList().Add(sp);
        }


        public static AnimSprite[] playerAnimSet;
        public static AnimSprite[] crawlerAnimSet;
        public static AnimSprite[] praetAnimSet;
        public static AnimSprite[] dividerAnimSet;
        public static AnimSprite[] cherubAnimSet;

        protected void initializeEverything(ContentManager cmgr)
        {
            playerAnimSet = new AnimSprite[]{ AssetManager.Player_Anim_Idle,
                                             AssetManager.Player_Anim_Walk, 
                                             AssetManager.Player_Anim_Sweep, 
                                             AssetManager.Player_Anim_Stab, 
                                             AssetManager.Player_Anim_Throw,
                                             AssetManager.Player_Anim_Draw,
                                             AssetManager.Player_Anim_Suicide,
                                             AssetManager.Player_Anim_Death };

            crawlerAnimSet = new AnimSprite[]{ AssetManager.Crawler_Anim_Idle,
                                              AssetManager.Crawler_Anim_Walk,
                                              AssetManager.Crawler_Anim_Attack,
                                              AssetManager.Crawler_Anim_Death };


            praetAnimSet = new AnimSprite[]{ AssetManager.Praet_Anim_Idle,
                                              AssetManager.Praet_Anim_Walk,
                                              AssetManager.Praet_Anim_Attack,
                                              AssetManager.Praet_Anim_Death };

            dividerAnimSet = new AnimSprite[]{ AssetManager.Divider_Anim_Idle,
                                              AssetManager.Divider_Anim_Walk,
                                              AssetManager.Divider_Anim_Attack };

            cherubAnimSet = new AnimSprite[]{ AssetManager.Cherub_Anim_Idle,
                                              AssetManager.Cherub_Anim_Walk,
                                              AssetManager.Cherub_Anim_Attack,
                                              AssetManager.Cherub_Anim_Death };


            player = new Player(new AnimManager(playerAnimSet), new Vector2(TileMap.tileWidth * TileMap.MapWidth / 2 + 32, TileMap.tileHeight * TileMap.MapHeight / 2), 28);
            Living.gameParent = this;
            full_entity_list.Add(player);
            Random rand = new Random();
            

            GenerateLavaSparkers();
            GenerateSpawners();

            Living.DamageFont = cmgr.Load<SpriteFont>("DamageFont");
        }

        private void GenerateLavaSparkers()
        {
            sparker_list.Clear();
            Random rand = new Random();
            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    for (int z = 0; z < TileMap.MapDepth; z++)
                    {
                        if (currentMap.data[x][y][z] >= 0x30 && currentMap.data[x][y][z] <= 0x3c)
                        {
                            if (rand.Next() % 8 == 0)
                            {
                                Sparker sp = new Sparker(20, new Vector2(x * TileMap.tileWidth, y * TileMap.tileHeight), true,
                                    100 + rand.Next() % 20, rand.Next() % 100);
                                sparker_list.Add(sp);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateSpawners()
        {
            spawner_list.Clear();
            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        if (currentMap.data[x][y][z + 1] == 0x71)
                        {
                            spawner_list.Add(new MonsterSpawner(new Vector2(x * TileMap.tileWidth, y * TileMap.tileHeight)));
                        }
                    }
                }
            }
        }

        public void AddMonster(Vector2 position)
        {
            position.X += 64;
            position.Y += 64;
            if((new Random().Next() % 10) > 3)
                full_entity_list.Add(new Crawler(new AnimManager(crawlerAnimSet), position));
            else if ((new Random().Next()%2 >0))
                full_entity_list.Add(new Cherub(new AnimManager(cherubAnimSet), position));
            else
                full_entity_list.Add(new Praetorian(new AnimManager(praetAnimSet), position));
        }

        public void ResetLevel()
        {
            corpse_list.Clear();
            Player p = null;
            for (int i = 0; i < full_entity_list.Count; i++)
            {
                if (full_entity_list[i] is Player)
                    p = (Player)full_entity_list[i];
            }
            full_entity_list.Clear();
            full_entity_list.Add(p);

            blood_splat_list.Clear();

            sparker_list.Clear();
            rune_list.Clear();
            GenerateLavaSparkers();
            GenerateSpawners();
            int idx = (int)((Math.Abs(p.getPos().X - 32)) / TileMap.tileWidth) - 1;
            if (idx < 0) idx = 0;
            int idy = (int)((p.getPos().Y) / TileMap.tileHeight);
            if (idy < 0) idy = 0;
            currentMap.data[idx][idy][2] = 0xff;
            currentMap.data[idx][idy][1] = 0xff;
        }

        public void ChangeRoom(Vector2 newPosition, int roomX, int roomY)
        {
            currentMap = TileMap.LoadMap(roomX, roomY, TileMap.Dimension.Overworld);
            ResetLevel();
            GenerateLavaSparkers();
        }

        public List<Entity> GetEntityList()
        {
            return full_entity_list;
        }

        public List<Rune> GetRuneList()
        {
            return rune_list;
        }

        public List<Sparker> GetSparkerList()
        {
            return sparker_list;
        }

        public Player getPlayer()
        {
            return player;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            loadTextures(this.Content);
            initializeEverything(this.Content);

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        private void advanceState()
        {
            if (keyState.IsKeyDown(Keys.Space) &&
                    !prevKeyState.IsKeyDown(Keys.Space))
            {
                curGameState++;
            }
        }
       
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            mState = Mouse.GetState();
            switch (curGameState)
            {
                case GameState.Splash:
                    advanceState();
                    break;
                case GameState.Instructions0:
                    advanceState();
                    break;
                case GameState.Instructions1:
                    advanceState();
                    break;
                case GameState.Instructions2:
                    advanceState();
                    break;

                case GameState.Died:
                    if (keyState.IsKeyDown(Keys.Space) &&
                    !prevKeyState.IsKeyDown(Keys.Space))
                    {
                        curGameState= GameState.Instructions0;
                    }
                    if (keyState.IsKeyDown(Keys.Enter) &&
                    !prevKeyState.IsKeyDown(Keys.Enter))
                    {
                        curGameState = GameState.Game;
                    }
                    if (keyState.IsKeyDown(Keys.Escape) &&
                    !prevKeyState.IsKeyDown(Keys.Escape))
                    {
                        base.Exit();
                    }
                    break;
                case GameState.Game:
                    for (int i = 0; i < full_entity_list.Count; i++)
                    {
                        //The update code of 0 means this is a routine update (the code is in case an update needs to be called out of this main update loop, such as by another entity or an environmental peice)
                        Entity e = full_entity_list[i];
                        int entStatus = e.update(0);
                        if (entStatus == 1)
                        {
                            Texture2D corpseTexture = null;
                            if (e is Player)
                            {
                                corpseTexture = AssetManager.Player_Texture_Corpse;
                            }
                            else if (e is Crawler)
                            {
                                corpseTexture = AssetManager.Crawler_Texture_Corpse;
                            }
                            else if (e is Praetorian)
                            {
                                corpseTexture = AssetManager.Praet_Texture_Corpse;
                            }
                            if (corpseTexture != null)
                            {
                                if (e is Living && (!(e is Player)))
                                {
                                    this.soul_list.Add(new Soul(((Living)e).getSoulPower(), null, e.getPos(), 100f));
                                   
                                }
                                this.corpse_list.Add(new Corpse(corpseTexture, e.getPos()));

                            }
                            if (!(e is Player))
                                full_entity_list.RemoveAt(i);
                         
                        }
                    }


                    for (int i = 0; i < rune_list.Count; i++)
                    {
                        int status = rune_list[i].update(0);
                        if (status == 1) rune_list.RemoveAt(i);

                    }

                    for (int i = 0; i < sparker_list.Count; i++)
                    {
                        sparker_list[i].Update();
                        if (!sparker_list[i].isAlive())
                            sparker_list.RemoveAt(i);
                    }

                    for (int i = 0; i < spawner_list.Count; i++)
                    {
                        spawner_list[i].Update();
                        if (!spawner_list[i].alive)
                            spawner_list.RemoveAt(i);
                    }

                    for (int i = 0; i < soul_list.Count; i++)
                    {
                        if (soul_list[i].update(0) == 1) soul_list.RemoveAt(i);

                    }

                    if (player.deathFlag)
                    {
                        curGameState = GameState.Died;
                    }


                    break;
            }

            prevKeyState = Keyboard.GetState();
            prevMState = Mouse.GetState();
            base.Update(gameTime);
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (curGameState)
            {
                case GameState.Instructions0:
                    spriteBatch.Draw(AssetManager.InstructionsScreen0, viewPort, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Instructions1:
                    spriteBatch.Draw(AssetManager.InstructionsScreen1, viewPort, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Instructions2:
                    spriteBatch.Draw(AssetManager.InstructionsScreen2, viewPort, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Splash:
                    spriteBatch.Draw(AssetManager.SplashScreen, viewPort, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Died:
                    spriteBatch.Draw(AssetManager.DeathScreen, viewPort, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);

                    break;
                case GameState.Game:
                    currentMap.Draw(spriteBatch);
                    HealthBar.Render(spriteBatch, ((Living)player).health);
                    foreach (Rune r in rune_list)
                        r.Render(spriteBatch);

                    foreach (Decal d in blood_splat_list)
                        d.draw(spriteBatch);

                    foreach (Soul s in soul_list)
                        s.Render(spriteBatch);
                    foreach (Corpse c in corpse_list)
                    {
                        c.render(spriteBatch);
                    }
                    foreach (Entity e in full_entity_list)
                    {
                        e.Render(spriteBatch);
                    }
                    foreach (Sparker s in sparker_list)
                        s.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
