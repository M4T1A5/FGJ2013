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
    class Enemy
    {
        public Vector2 position;
        private Vector2 face;
        private Animator animator;
        public Vector2 Direction = Vector2.Zero;
        private float speed = 1;
        private float fps = 5;
        //private Color color;
        private Random rand = new Random();

        enum Look
        {
            None = 0,
            Up,
            Down,
            Left,
            Right
        }

        Look look = Look.None;

        public Enemy(Texture2D Texture, Vector2 Position) //Color Color)
        {
            position = Position;
            animator = new Animator(Texture, 2, 35, 55, 1, 2);
            //color = Color;
        }

        public void Update(GameTime gameTime, Vector2 PlayerPosition)
        {
            face = Vector2.Normalize(PlayerPosition - position);

            if (Direction == Vector2.Zero) // if enemy is not moving
            {
                if (face.Y < 0) // if player is above the enemy
                    animator.ChangeAnimation(8, 8, 1, fps);
                if (face.Y > 0) // if player is below the enemy
                    animator.ChangeAnimation(2, 2, 1, fps);
                if (face.X > 0.8f) // if player on the right side of the enemy
                    animator.ChangeAnimation(13, 13, 1, fps);
                if (face.X < -0.8f) // if player on the left side of the enemy
                    animator.ChangeAnimation(19, 19, 1, fps);
                look = Look.None;
            }
            else if (Direction.X > 0.8f) // if player on the right side of the enemy
            {
                if (look != Look.Right)
                animator.ChangeAnimation(13, 16, 6, fps);
                look = Look.Right;
            }
            else if (Direction.X < -0.8f) // if player on the left side of the enemy
            {
                if (look != Look.Left)
                animator.ChangeAnimation(19, 20, 6, fps);
                look = Look.Left;
            }
            else if (Direction.Y > 0) // if player below the enemy
            {
                if (look != Look.Down)
                animator.ChangeAnimation(1, 1, 4, fps);
                look = Look.Down;
            }
            else if (Direction.Y < 0) // if player above the enemy
            {
                if (look != Look.Up)
                animator.ChangeAnimation(7, 7, 4, fps);
                look = Look.Up;
            }

            if ((PlayerPosition - position).Length() < 500)
            {
                Direction = Vector2.Normalize(PlayerPosition - position);
            }
            else
            {
                Direction = Vector2.Zero;
            }

            animator.Update(gameTime);

            position += Direction * speed;
        }

        public void Draw(SpriteBatch SB)
        {
            animator.Draw(SB, position);
        }
    }
}
