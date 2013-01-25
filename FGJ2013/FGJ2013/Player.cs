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

        public Player(Texture2D Texture, Vector2 Position)
        {
            position = Position;
            playerAnimator = new Animator(Texture, 4, 64, 64, 1);
        }

        public void Update(KeyboardState KI, GameTime gameTime)
        {
            if (KI.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
            }
            if (KI.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
            }
            if (KI.IsKeyDown(Keys.Up))
            {
                position.Y -= 1 * speed;
            }
            if (KI.IsKeyDown(Keys.Down))
            {
                position.Y += 1 * speed;
            }

            playerAnimator.Update(gameTime);
        }

        public void Draw(SpriteBatch SB)
        {
            playerAnimator.Draw(SB, position);
        }
    }
}