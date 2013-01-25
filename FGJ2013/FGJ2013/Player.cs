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
        private float speed = 5;
        public enum Direction
        {
            None = 0,
            Right,
            Left,
            Up,
            Down
        }
        public Direction direction = Direction.None;

        public Player(Texture2D Texture, Vector2 Position)
        {
            position = Position;
            playerAnimator = new Animator(Texture, 4, 64, 64, 1);
        }

        public void Update(KeyboardState KeyboardInput, GameTime gameTime)
        {
            if (KeyboardInput.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
                if (direction != Direction.Left)
                {
                    direction = Direction.Left;
                    playerAnimator.ChangeAnimation(4,4,1,1);
                }
            }
            if (KeyboardInput.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
                if (direction != Direction.Right)
                {
                    direction = Direction.Right;
                    playerAnimator.ChangeAnimation(2, 2, 1, 1);
                }
            }
            if (KeyboardInput.IsKeyDown(Keys.Up))
            {
                position.Y -= 1 * speed;
                if (direction != Direction.Up)
                {
                    direction = Direction.Up;
                    playerAnimator.ChangeAnimation(1, 1, 1, 1);
                }
            }
            if (KeyboardInput.IsKeyDown(Keys.Down))
            {
                position.Y += 1 * speed;
                if (direction != Direction.Down)
                {
                    direction = Direction.Down;
                    playerAnimator.ChangeAnimation(3, 3, 1, 1);
                }
            }

            playerAnimator.Update(gameTime);
        }

        public void Draw(SpriteBatch SB)
        {
            playerAnimator.Draw(SB, position);
        }
    }
}