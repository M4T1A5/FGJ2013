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
        Texture2D maptexture;
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
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimations"), new Vector2(50)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDark"), new Vector2(80)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctor"), new Vector2(130)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsDoctorDark"), new Vector2(170)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsu"), new Vector2(230)),
                new Enemy(Content.Load<Texture2D>("Animations/AllCharacterAnimationsHoitsuDark"), new Vector2(260))
            };
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
                if (hitbox.PlayerHit(new Rectangle((int)player.position.X + 10, (int)player.position.Y + 45, 35, 35),new Rectangle((int)enemy.position.X + 10, (int)enemy.position.Y + 45, 35, 35)))
                {
                    enemy.position = new Vector2(350);
                }
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
           spriteBatch.Begin(SpriteSortMode.Deferred,
                       BlendState.AlphaBlend,
                       null,
                       null,
                       null,
                       null,
                       camera.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));
            
            // TODO: Add your drawing code here
            map.Draw(spriteBatch, mapView);
            player.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
