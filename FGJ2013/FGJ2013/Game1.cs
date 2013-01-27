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
using FuncWorks.XNA.XTiled;
using System.Diagnostics;
using System.Media;


namespace FGJ2013
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int width = 1280, height = 720;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState KeyboardInput;
        Rectangle mapView;
        Map map;
        Camera2d camera = new Camera2d();
        Player player;
        List<Enemy> enemies;
        Hitbox hitbox;
        SoundEffect heartbeat;
        SoundEffectInstance heartbeatInstance;
        bool keyPressed = false;
        SoundEffect MenuMusic;
        SoundEffect Musa1;
        SoundEffect Musa2;
        SoundEffect Musa3;
        SoundEffect Musa4;
        SoundEffect kirkuna;

        SoundEffectInstance instance;
        static int DrugsCount = 0;
        List<Rectangle> pillLocations;

        Texture2D PlayerTexture;
        Texture2D PlayerTextureDark;
        Texture2D DoctorTexture;
        Texture2D DoctorTextureDark;
        Texture2D NurseTexture;
        Texture2D NurseTextureDark;

        Texture2D WorldTileSheet;
        Texture2D WorldTileSheetDark;
        Texture2D PropsTileSheet;
        Texture2D PropsTileSheetDark;

        Rectangle StartButton;
        Rectangle ExitButton;
        Rectangle CreditsButton;
        Texture2D Menu;
        Texture2D Credits;

        Texture2D Start1;
        Texture2D Start2;
        Texture2D Start3;
        Texture2D Start4;

        Texture2D End1;
        Texture2D End2;
        Texture2D End3;


        List<int> start = new List<int> {0,0,0,0};
        int startnumber = 0;

        int endnumber = 0;


        //Texture2D maptexture;
        float shortestDistance;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Less Than Three";
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
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

            StartButton = new Rectangle(40, 530, 230, 50);
            ExitButton = new Rectangle(40, 600, 230, 50);
            CreditsButton = new Rectangle(40, 670, 230, 50);

            pillLocations = new List<Rectangle>
            {
                Rectangle.Empty,
                Rectangle.Empty,
                Rectangle.Empty,
                Rectangle.Empty,
                Rectangle.Empty
            };

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

            PlayerTexture = Content.Load<Texture2D>("Animations/AllCharacterAnimations");
            PlayerTextureDark = Content.Load<Texture2D>("Animations/AllCharacterAnimationsDark");
            DoctorTexture = Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctor");
            DoctorTextureDark = Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctorDark");
            NurseTexture = Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsu");
            NurseTextureDark = Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsuDark");

            WorldTileSheet = Content.Load<Texture2D>("Maps/worldsheet");
            WorldTileSheetDark = Content.Load<Texture2D>("Maps/hellworldsheet");
            PropsTileSheet = Content.Load<Texture2D>("Maps/props");
            PropsTileSheetDark = Content.Load<Texture2D>("Maps/hellprops");

            Menu = Content.Load<Texture2D>("titlescreenhearthand2");
            Credits = Content.Load<Texture2D>("Credits1");

            Start1 = Content.Load<Texture2D>("StartImages/start1");
            Start2 = Content.Load<Texture2D>("StartImages/start2");
            Start3 = Content.Load<Texture2D>("StartImages/start3");
            Start4 = Content.Load<Texture2D>("StartImages/start4");

            End1 = Content.Load<Texture2D>("StartImages/End1");
            End2 = Content.Load<Texture2D>("StartImages/End2");
            End3 = Content.Load<Texture2D>("StartImages/End3");

            MenuMusic = Content.Load<SoundEffect>("Music/1 musa v1");
            Musa1 = Content.Load<SoundEffect>("Music/1_musiikki");
            Musa2 = Content.Load<SoundEffect>("Music/2_musiikki");
            Musa3 = Content.Load<SoundEffect>("Music/3_musiikki");
            Musa4 = Content.Load<SoundEffect>("Music/4_musiikki");
            kirkuna = Content.Load<SoundEffect>("Music/kirkuna");

            instance = MenuMusic.CreateInstance();
            instance.IsLooped = true;
            instance.Play();

            player = new Player(PlayerTexture, new Vector2(55));

            enemies = new List<Enemy> 
            {
                new Enemy(NurseTexture, new Vector2(550, 255), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(2090, 933), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(1645, 735), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(1027, 1042), Enemy.EnemyType.Nurse),

                new Enemy(DoctorTexture, new Vector2(2133, 254), Enemy.EnemyType.Doctor),
                new Enemy(DoctorTexture, new Vector2(1197, 638), Enemy.EnemyType.Doctor),
            };
            Map.InitObjectDrawing(GraphicsDevice);
            map = Content.Load<Map>("Maps/Stage");
            mapView = map.Bounds;
            hitbox = new Hitbox(map);
            heartbeat = Content.Load<SoundEffect>("GGJ13_Theme");
            heartbeatInstance = heartbeat.CreateInstance();

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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardInput = Keyboard.GetState();

            switch (Data.GameState)
            {
                case State.None:
                    break;
                case State.Start:

                    if (KeyboardInput.IsKeyDown(Keys.Space) && !keyPressed)
                    {
                        startnumber++;
                        keyPressed = true;
                    }
                    else if (KeyboardInput.IsKeyUp(Keys.Space))
                    {
                        keyPressed = false;
                    }

                    if (startnumber > 3)
                    {
                        Data.GameState = State.Play;
                        instance.Stop(true);
                        instance = Musa1.CreateInstance();
                        instance.IsLooped = true;
                        instance.Play();
                        break;
                    }

                    start[startnumber] += 2;
                    if ((startnumber >0))
                    {
                        start[startnumber - 1] -= 3;
                    }


                    break;
                case State.Play:

                    player.Update(KeyboardInput, gameTime);

                    player.position += hitbox.MapHit(player.position);

                    foreach (var enemy in enemies)
                    {
                        if (player.SourceID == enemy.SourceID)
                        {
                            enemy.Update(gameTime, player.position);
                            enemy.position += hitbox.MapHit(enemy.position); 
                        }

                        if (hitbox.PlayerHit(new Rectangle((int)player.position.X + 10, (int)player.position.Y + 45, 35, 35),
                            new Rectangle((int)enemy.position.X + 10, (int)enemy.position.Y + 45, 35, 35))) //enemy hits player
                        {
                            Reset();
                            kirkuna.Play();
                        }
                    }

                    for (var i = 0; i < map.ObjectLayers[0].MapObjects.Count<MapObject>(); i++)
                    {
                        if (hitbox.PlayerHit(new Rectangle((int)player.position.X + 10, (int)player.position.Y + 45, 35, 35),
                            new Rectangle((int)map.ObjectLayers[0].MapObjects[i].Bounds.X, (int)map.ObjectLayers[0].MapObjects[i].Bounds.Y, 20, 20))) //player collects drug
                        {
                            CollectDrug();
                            pillLocations[i] = map.ObjectLayers[0].MapObjects[i].Bounds;
                            map.ObjectLayers[0].MapObjects[i].Bounds.Location = new Point(10000,10000);
                        }
                    }

                    if (KeyboardInput.IsKeyDown(Keys.W) && !keyPressed)
                    {
                        keyPressed = true;
                        hitbox.AtDoor(player); 
                    }
                    else if (KeyboardInput.IsKeyUp(Keys.W))
                    {
                        keyPressed = false;
                    }

                    if (heartbeatInstance.State != SoundState.Playing)
                    {
                        heartbeatInstance.IsLooped = true;
                        heartbeatInstance.Play();
                    }
                    shortestDistance = 10000;
                    foreach (var enemy in enemies)
                    {
                        if (shortestDistance > (enemy.position - player.position).Length())
                            shortestDistance = (enemy.position - player.position).Length();
                    }

                    if (shortestDistance < 3000)
                    {
                        heartbeatInstance.Pitch = (2000 - shortestDistance) / 2000; // -0.3f;
                    }
                    else
                    {
                        heartbeatInstance.Pitch = -0.5f;
                    }
                    heartbeatInstance.Volume = 1;// 0.2f + (3200 - shortestDistance) / 4000;

                    camera.Pos = player.position;

                    Debug.WriteLine(player.position);

                    break;
                case State.End:
                    endnumber += 1;
                    if (endnumber == 300)
                        kirkuna.Play();
                    if (KeyboardInput.IsKeyDown(Keys.Space))
                    {
                        Data.GameState = State.Menu;
                    }
                    break;
                case State.Menu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        var mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                        if (StartButton.Intersects(mouse))
                        {
                            Data.GameState = State.Start;
                            this.IsMouseVisible = false;
                        }
                        if (ExitButton.Intersects(mouse))
                        {
                            this.Exit();
                        }
                        if (CreditsButton.Intersects(mouse))
                        {
                            Data.GameState = State.Credits;
                            this.IsMouseVisible = false;
                        }
                    }
                    break;
                case State.Credits:
                    if (KeyboardInput.IsKeyDown(Keys.Escape))
                    {
                        Data.GameState = State.Menu;
                    }
                    break;
                default:
                    break;
            }


            

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
       {
            GraphicsDevice.Clear(Color.Black);
            var playerTileX = (int)Math.Floor(player.position.X / map.TileWidth);
            var playerTileY = (int)Math.Floor((player.position.Y + 45) / map.TileHeight);
            player.SourceID = 0;
            try
            {
                player.SourceID = map.TileLayers[map.TileLayers.Count - 1]
                        .Tiles[playerTileX][playerTileY].SourceID;
            }
            catch (NullReferenceException)
            {
                player.SourceID = 0;
                //throw;
            }
            
            
            // TODO: Add your drawing code here
            //map.Draw(spriteBatch, mapView);

            switch (Data.GameState)
            {
                case State.None:
                    break;
                case State.Start:

                    spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);

                    spriteBatch.Draw(Start4, Vector2.Zero, new Color(start[3], start[3], start[3], start[3]));
                    spriteBatch.Draw(Start3, Vector2.Zero, new Color(start[2], start[2], start[2], start[2]));
                    spriteBatch.Draw(Start2, Vector2.Zero, new Color(start[1], start[1], start[1], start[1]));
                    spriteBatch.Draw(Start1, Vector2.Zero, new Color(start[0], start[0], start[0], start[0]));

                    spriteBatch.End();

                    break;
                case State.Play:

                    spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));

                    DrawLayer(spriteBatch, map, 0, ref mapView, 0.1f, player.SourceID);
                    DrawLayer(spriteBatch, map, 1, ref mapView, 0.2f, player.SourceID);
                    DrawLayer(spriteBatch, map, 2, ref mapView, 0.3f, player.SourceID);
                    DrawLayer(spriteBatch, map, 3, ref mapView, 0.4f, player.SourceID);
                    for (var i = 0; i < map.ObjectLayers[0].MapObjects.Count<MapObject>(); i++ )
                    {
                        var pill = map.ObjectLayers[0].MapObjects[i];
                        var PillTileX = (int)Math.Floor((double)pill.Bounds.X / map.TileWidth);
                        var PillTileY = (int)Math.Floor((double)pill.Bounds.Y / map.TileHeight);
                        var PillSourceID = 0;
                        try
                        {
                            PillSourceID = map.TileLayers[map.TileLayers.Count - 1]
                                                    .Tiles[PillTileX][PillTileY].SourceID;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            
                            //throw;
                        }
                        if (PillSourceID != player.SourceID)
                        {
                            map.DrawMapObject(spriteBatch, 0, i, mapView, 0.0f);
                        }
                        else
                        {
                            map.DrawMapObject(spriteBatch, 0, i, mapView, 0.5f);
                        }
                    }
                    //map.DrawObjectLayer(spriteBatch, 0, mapView, 0.0f);
                    player.Draw(spriteBatch);
                    foreach (var enemy in enemies)
                    {
                        var enemyTileX = (int)Math.Floor(enemy.position.X / map.TileWidth);
                        var enemyTileY = (int)Math.Floor((enemy.position.Y + 45)/ map.TileHeight);
                        enemy.SourceID = 0;
                        try
                        {
                            enemy.SourceID = map.TileLayers[map.TileLayers.Count - 1]
                                                    .Tiles[enemyTileX][enemyTileY].SourceID;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            enemy.SourceID = -1;
                            //throw;
                        }
                        if (enemy.SourceID == player.SourceID)
                        {
                            enemy.Draw(spriteBatch); 
                        }
                    }

                    spriteBatch.End();

                    break;
                case State.End:
                    spriteBatch.Begin();


                    spriteBatch.Draw(End3, Vector2.Zero, Color.White);
                    if (330 > endnumber)
                    {
                        spriteBatch.Draw(End2, Vector2.Zero, Color.White); 
                    }
                    if (300 > endnumber)
                    {
                        spriteBatch.Draw(End1, Vector2.Zero, Color.White); 
                    }

                    spriteBatch.End();
                    break;
                case State.Menu:
                    this.IsMouseVisible = true;
                    spriteBatch.Begin();
                    spriteBatch.Draw(Menu, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    //Debug.Write(Mouse.GetState().X + " ");
                    //Debug.Write(Mouse.GetState().Y + "\n");
                    break;
                case State.Credits:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Credits, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;
                default:
                    break;
            }


                        
            base.Draw(gameTime);
        }

        public void DrawLayer(SpriteBatch spriteBatch, Map map, Int32 layerID, ref Rectangle region, Single layerDepth, int SourceID)
        {

            // Tiles are stored in a multidimensional array.
            // By converting the map coordinates to tile coordinates 
            // we can eliminate the need for bound checking
            Int32 txMin = region.X / map.TileWidth;
            Int32 txMax = (region.X + region.Width) / map.TileWidth;
            Int32 tyMin = region.Y / map.TileHeight;
            Int32 tyMax = (region.Y + region.Height) / map.TileHeight;

            for (int y = tyMin; y <= tyMax; y++)
            {
                for (int x = txMin; x <= txMax; x++)
                {

                    // check that we aren't going outside the map, and that there is a tile at this location
                    if (x < map.TileLayers[layerID].Tiles.Length && y < map.TileLayers[layerID].Tiles[x].Length
                        && map.TileLayers[layerID].Tiles[x][y] != null)
                    {
                        if (map.TileLayers[map.TileLayers.Count - 1].Tiles[x][y] != null
                            && map.TileLayers[map.TileLayers.Count - 1].Tiles[x][y].SourceID != SourceID)
                        {
                            map.TileLayers[layerID].OpacityColor = Color.Black; 
                        }
                        else
                        {
                            map.TileLayers[layerID].OpacityColor = Color.White;
                        }
                        // adjust the tiles map coordinates to screen space
                        Rectangle tileTarget = map.TileLayers[layerID].Tiles[x][y].Target;
                        tileTarget.X = tileTarget.X - region.X;
                        tileTarget.Y = tileTarget.Y - region.Y;

                        spriteBatch.Draw(
                            // the texture (image) of the tile sheet is mapped by
                            // Tile.SourceID -> TileLayers.TilesetID -> Map.Tileset.Texture
                            map.Tilesets[map.SourceTiles[map.TileLayers[layerID].Tiles[x][y].SourceID].TilesetID].Texture,

                            // screen space of the tile
                            tileTarget,

                            // source of the tile in the tilesheet
                            map.SourceTiles[map.TileLayers[layerID].Tiles[x][y].SourceID].Source,

                            // layers can have an opacity value, this property is Color.White at the opacity of the layer
                            map.TileLayers[layerID].OpacityColor,

                            // tile rotation value
                            map.TileLayers[layerID].Tiles[x][y].Rotation,

                            // origin of the tile, this is always the center of the tile
                            map.SourceTiles[map.TileLayers[layerID].Tiles[x][y].SourceID].Origin,

                            // tile horizontal or vertical flipping value
                            map.TileLayers[layerID].Tiles[x][y].Effects,

                            // depth for SpriteSortMode
                            layerDepth);
                    }
                }
            }
        }

        void Reset()
        {
            //reset tiles back to normal
            DrugsCount = -1;
            CollectDrug();
            player = new Player(PlayerTexture, new Vector2(55));
            enemies = new List<Enemy> 
            {
                new Enemy(NurseTexture, new Vector2(550, 255), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(2090, 933), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(1645, 735), Enemy.EnemyType.Nurse),
                new Enemy(NurseTexture, new Vector2(1027, 1042), Enemy.EnemyType.Nurse),

                new Enemy(DoctorTexture, new Vector2(2133, 254), Enemy.EnemyType.Doctor),
                new Enemy(DoctorTexture, new Vector2(1197, 638), Enemy.EnemyType.Doctor),
            };

            for (var i = 0; i < map.ObjectLayers[0].MapObjects.Count<MapObject>(); i++)
            {
                if (pillLocations[i] != Rectangle.Empty)
                {
                    map.ObjectLayers[0].MapObjects[i].Bounds = pillLocations[i];
                    pillLocations[i] = Rectangle.Empty;
                }
            }

            Data.GameState = State.Menu;
        }

        void CollectDrug()
        {
            DrugsCount++;
            switch (DrugsCount)
            {
                case 1: // change props


                    foreach (var tileset in map.Tilesets)
                    {
                        if (tileset.Name == "propsit")
                        {
                            tileset.Texture = PropsTileSheetDark;
                        }
                    }
                    break;
                case 2: // change enemies
                    
                        instance.Stop(true);
                        instance = Musa2.CreateInstance();
                        instance.IsLooped = true;
                        instance.Play();


                    break;
                case 3: // change player
                    
                        instance.Stop(true);
                        instance = Musa3.CreateInstance();
                        instance.IsLooped = true;
                        instance.Play();

                    player.ChangeTexture(PlayerTextureDark);
                    break;
                case 4: // change maptextures
                    
                        instance.Stop(true);
                        instance = Musa4.CreateInstance();
                        instance.IsLooped = true;
                        instance.Play();

                    foreach (var enemy in enemies)
                    {
                        if (enemy.Type == Enemy.EnemyType.Nurse)
                        {
                            enemy.ChangeTexture(NurseTextureDark);
                        }
                        else
                        {
                            enemy.ChangeTexture(DoctorTextureDark);
                        }
                    }

                    foreach (var tileset in map.Tilesets)
                    {
                        if (tileset.Name == "tilesetti3")
                        {
                            tileset.Texture = WorldTileSheetDark;
                        }
                    }
                    break;
                case 5: // win game
                    Data.GameState = State.End;
                    break;
                default:
                    foreach (var tileset in map.Tilesets)
                    {
                        if (tileset.Name == "propsit")
                        {
                            tileset.Texture = PropsTileSheet;
                        }
                    }
                    foreach (var enemy in enemies)
                    {
                        if (enemy.Type == Enemy.EnemyType.Nurse)
                        {
                            enemy.ChangeTexture(NurseTexture);
                        }
                        else
                        {
                            enemy.ChangeTexture(DoctorTexture);
                        }
                    }
                    player.ChangeTexture(PlayerTexture);
                    foreach (var tileset in map.Tilesets)
                    {
                        if (tileset.Name == "tilesetti3")
                        {
                            tileset.Texture = WorldTileSheet;
                        }
                    }

                    break;
            }
        }
    }
}
