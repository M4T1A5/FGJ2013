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

namespace FGJ2013
{
    class Player
    {
        public Vector2 position;
        private Animator playerAnimator;
        private float speed = 2;
        private float fps = 5;

        public enum Direction
        {
            None = 0,
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }
        
        private Random rand;

        public Direction direction = Direction.None;

        private List<int> randomizator;

        public Player(Texture2D Texture, Vector2 Position)
        {
            position = Position;
            playerAnimator = new Animator(Texture, 1, 35, 55, 1, 2);
        }

        public void Update(KeyboardState KeyboardInput, GameTime gameTime)
        {
            keyboardController(KeyboardInput);

            Camera.Position = new Vector2(1280/2, 720/2) - position;

            //if (KeyboardInput.IsKeyDown(Keys.Space))
            //{
            //    for (int i = 0; i < 8; i++)
            //    {
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //        randomizator.Add(rand.Next(1, 999999));
            //    }
            //}



            playerAnimator.Update(gameTime);
        }

        public void Draw(SpriteBatch SB)
        {
            playerAnimator.Draw(SB, position);
        }

        private void keyboardController(KeyboardState KeyboardInput)
        {
            if (!KeyboardInput.IsKeyDown(Keys.Up) && !KeyboardInput.IsKeyDown(Keys.Right) && !KeyboardInput.IsKeyDown(Keys.Down) && !KeyboardInput.IsKeyDown(Keys.Left))
            {
                if ((direction == Direction.NE || direction == Direction.N || direction == Direction.NW))
                    playerAnimator.ChangeAnimation(8, 8, 1, fps);
                if ((direction == Direction.SE || direction == Direction.S || direction == Direction.SW))
                    playerAnimator.ChangeAnimation(2, 2, 1, fps);
                if (direction == Direction.E)
                    playerAnimator.ChangeAnimation(15, 15, 1, fps);
                if (direction == Direction.W)
                    playerAnimator.ChangeAnimation(19, 19, 1, fps);

                direction = Direction.None;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up) && KeyboardInput.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
                position.Y -= 1 * speed;
                if (!(direction == Direction.NE || direction == Direction.N || direction == Direction.NW))
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                direction = Direction.NE;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up) && KeyboardInput.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
                position.Y -= 1 * speed;
                if (!(direction == Direction.NE || direction == Direction.N || direction == Direction.NW))
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                direction = Direction.NW;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Up))
            {
                position.Y -= 1 * speed;
                if (!(direction == Direction.NE || direction == Direction.N || direction == Direction.NW))
                {
                    playerAnimator.ChangeAnimation(7, 7, 4, fps);
                }
                direction = Direction.N;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down) && KeyboardInput.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
                position.Y += 1 * speed;
                if (!(direction == Direction.SE || direction == Direction.S || direction == Direction.SW))
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                direction = Direction.SE;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down) && KeyboardInput.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
                position.Y += 1 * speed;
                if (!(direction == Direction.SE || direction == Direction.S || direction == Direction.SW))
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                direction = Direction.SW;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Down))
            {
                position.Y += 1 * speed;
                if (!(direction == Direction.SE || direction == Direction.S || direction == Direction.SW))
                {
                    playerAnimator.ChangeAnimation(1, 1, 4, fps);
                }
                direction = Direction.S;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
                if (direction != Direction.E)
                {
                    playerAnimator.ChangeAnimation(13, 16, 6, fps);
                }
                direction = Direction.E;
            }
            else if (KeyboardInput.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
                if (direction != Direction.W)
                {
                    playerAnimator.ChangeAnimation(19, 20, 6, fps);
                }
                direction = Direction.W;
            }
        }
    }
}