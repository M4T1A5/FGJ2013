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
        //Texture2D maptexture;
        float shortestDistance;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "My heart will go on";
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
            player = new Player(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDark"), new Vector2(350));
            enemies = new List<Enemy> 
            {
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimations"), new Vector2(55)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDark"), new Vector2(80)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctor"), new Vector2(130)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctorDark"), new Vector2(170)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsu"), new Vector2(230)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsuDark"), new Vector2(260))
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

            player.Update(KeyboardInput, gameTime);

            player.position += hitbox.MapHit(player.position);

            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, player.position);
                enemy.position += hitbox.MapHit(enemy.position);
                if (hitbox.PlayerHit(new Rectangle((int)player.position.X + 10, (int)player.position.Y + 45, 35, 35), new Rectangle((int)enemy.position.X + 10, (int)enemy.position.Y + 45, 35, 35)))
                {

                }
            }
            if (KeyboardInput.IsKeyDown(Keys.W) && GameEventHandler.currentEvent != GameEvents.DoorOpen)
            {
                GameEventHandler.currentEvent = GameEvents.DoorOpen;
                hitbox.AtDoor(player); 
            }
            else if (KeyboardInput.IsKeyUp(Keys.W))
            {
                GameEventHandler.currentEvent = GameEvents.None;
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
            var playerSourceID = 0;
            try
            {
                playerSourceID = map.TileLayers[map.TileLayers.Count - 1]
                        .Tiles[playerTileX][playerTileY].SourceID;
            }
            catch (NullReferenceException)
            {
                playerSourceID = 0;
                //throw;
            }
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));
            
            // TODO: Add your drawing code here
            //map.Draw(spriteBatch, mapView);
            DrawLayer(spriteBatch, map, 0, ref mapView, 0.1f, playerSourceID);
            DrawLayer(spriteBatch, map, 1, ref mapView, 0.2f, playerSourceID);
            DrawLayer(spriteBatch, map, 2, ref mapView, 0.3f, playerSourceID);
            DrawLayer(spriteBatch, map, 3, ref mapView, 0.4f, playerSourceID);
            for (var i = 0; i < map.ObjectLayers[0].MapObjects.Count<MapObject>(); i++ )
            {
                var pill = map.ObjectLayers[0].MapObjects[i];
                var PillTileX = (int)Math.Floor((double)pill.Bounds.X / map.TileWidth);
                var PillTileY = (int)Math.Floor((double)pill.Bounds.Y / map.TileHeight);
                var PillSourceID = map.TileLayers[map.TileLayers.Count - 1]
                        .Tiles[PillTileX][PillTileY].SourceID;
                if (PillSourceID != playerSourceID)
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
                var enemySourceID = 0;
                try
                {
                    enemySourceID = map.TileLayers[map.TileLayers.Count - 1]
                                            .Tiles[enemyTileX][enemyTileY].SourceID;
                }
                catch (IndexOutOfRangeException)
                {
                    enemySourceID = -1;
                    //throw;
                }
                if (enemySourceID == playerSourceID)
                {
                    enemy.Draw(spriteBatch); 
                }
            }            
            spriteBatch.End();
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
    }
}
