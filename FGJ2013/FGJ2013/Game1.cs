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
        Map map;
        Player player;
        List<Enemy> enemies;
        Hitbox hitbox;
        SoundEffect heartbeat;
        SoundEffectInstance heartbeatInstance;

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
            player = new Player(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(200));
            enemies = new List<Enemy> 
            {
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(50)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(80)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(130)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(170)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(230)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(260)),
                new Enemy(Content.Load<Texture2D>("AllCharacterAnimations"), new Vector2(300))
            };
            map = Content.Load<Map>("Maps/Harjoituz");
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
            }
            if (heartbeatInstance.State != SoundState.Playing)
            {
                heartbeatInstance.IsLooped = true;
                heartbeatInstance.Play();
            }
            heartbeatInstance.Pitch = (1000 - (player.position - enemies[0].position).Length()) / 1000 - 0.4f;
            heartbeatInstance.Volume = 0.7f;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
       {
            GraphicsDevice.Clear(new Color(183,183,183));
            map.Draw(spriteBatch, player.position);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
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
