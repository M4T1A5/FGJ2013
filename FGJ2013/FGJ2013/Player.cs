using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace FGJ2013
{
    class Player
    {
        public Vector2 position;
        private Animator playerAnimator;
        private float speed = 5;
        private float fps = 5;
        public int SourceID = 0;

        private List<Vector2> directionList = new List<Vector2> 
        { 
            Vector2.Zero, new Vector2(-1,1), new Vector2(0,1), 
            new Vector2(1,1), new Vector2(-1,0), new Vector2(1,-1), 
            new Vector2(1,0), new Vector2(-1,-1), new Vector2(0,-1)
        };
        
        private Random rand = new Random();

        public Vector2 Direction = Vector2.Zero;

        //private List<int> randomizator;

        public Player(Texture2D Texture, Vector2 Position)
        {
            position = Position;
            playerAnimator = new Animator(Texture, 1, 35, 55, 1, 2);
            for (int i = 1; i < directionList.Count; i++)
            {
                directionList[i] = Vector2.Normalize(directionList[i]);
            }
        }  

        public void Update(KeyboardState KeyboardInput, GameTime gameTime)
        {
            keyboardController(KeyboardInput);

            //Camera.Position = new Vector2(1280 / 2, 720 / 2) - new Vector2((position.X), (position.Y));

            if (KeyboardInput.IsKeyDown(Keys.Space))
            {
                int r = ((rand.Next(1, 99)) * 2 + 1);
                int p = r;
                var multiplier = (3f / 4f) * Math.PI;
                Vector2 randvect = directionList[rand.Next(1, 8)];

                for (int i = 1; i < 9; i++)
                {
                    directionList[i] = new Vector2((float)Math.Cos(i * p * multiplier) * randvect.X - (float)Math.Sin(i * p * multiplier) * randvect.Y, (float)Math.Sin(i * p * multiplier) * randvect.X + (float)Math.Cos(i * p * multiplier) * randvect.Y);

                }
            }

            //Debug.WriteLine(Direction);

            playerAnimator.Update(gameTime);

            position += Direction * speed;
        }

        public void ChangeTexture(Texture2D Texture)
        {
            playerAnimator.texture = Texture;
        }

        public void Draw(SpriteBatch SB)
        {
            playerAnimator.Draw(SB, position);
        }

        private void keyboardController(KeyboardState KeyboardInput)
        {
            if (!KeyboardInput.IsKeyDown(Keys.Up) && !KeyboardInput.IsKeyDown(Keys.Right) && !KeyboardInput.IsKeyDown(Keys.Down) && !KeyboardInput.IsKeyDown(Keys.Left))
            {
                if (Direction.Y < 0)
                    playerAnimator.ChangeAnimation(8, 8, 1, fps);
                if (Direction.Y > 0)
                    playerAnimator.ChangeAnimation(2, 2, 1, fps);
                if (Direction.X > 0 && Direction.Y == 0)
                    playerAnimator.ChangeAnimation(13, 13, 1, fps);
                if (Direction.X < 0 && Direction.Y == 0)
                    playerAnimator.ChangeAnimation(19, 19, 1, fps);

                Direction = directionList[0];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up) && KeyboardInput.IsKeyDown(Keys.Right))
            {
                if (Direction.Y >= 0)
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                Direction = directionList[5];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up) && KeyboardInput.IsKeyDown(Keys.Left))
            {
                if (Direction.Y >= 0)
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                Direction = directionList[7];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up))
            {
                if (Direction.Y >= 0)
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                Direction = directionList[8];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down) && KeyboardInput.IsKeyDown(Keys.Right))
            {
                if (Direction.Y <= 0)
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                Direction = directionList[3];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down) && KeyboardInput.IsKeyDown(Keys.Left))
            {
                if (Direction.Y <= 0)
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                Direction = directionList[1];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down))
            {
                if (Direction.Y <= 0)
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                Direction = directionList[2];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Right))
            {
                if (Direction.X < 0.8f)
                {
                    playerAnimator.ChangeAnimation(13, 16, 6, fps);
                }
                Direction = directionList[6];
            }
            else if (KeyboardInput.IsKeyDown(Keys.Left))
            {
                if (Direction.X > -0.8f)
                {
                    playerAnimator.ChangeAnimation(19, 20, 6, fps);
                }
                Direction = directionList[4];
            }
        }
    }
}