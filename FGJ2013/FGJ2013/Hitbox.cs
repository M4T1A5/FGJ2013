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
using FuncWorks.XNA.XTiled;
using System.Diagnostics;

namespace FGJ2013
{
    class Hitbox
    {
        public List<Rectangle> hitboxes;
        public Hitbox(Map map)
        {
            hitboxes = new List<Rectangle>();
            foreach (var tiles in map.TileLayers[map.TileLayers.Count - 2].Tiles)
            {
                foreach (var tile in tiles)
                {
                    if (tile != null)
                    {
                        hitboxes.Add(tile.Target);  
                    }
                }                
            }
        }

        public Vector2 MapHit(Vector2 CharacterPosition)
        {
            Vector2 hit = Vector2.Zero;
            var playerRectangle = new Rectangle((int)CharacterPosition.X, (int)CharacterPosition.Y + 30, 35, 35);
            foreach (Rectangle hitbox in hitboxes)
            {
                if (hitbox.Intersects(playerRectangle))
                {
                    Vector2 difference = new Vector2((playerRectangle.Location.X + playerRectangle.Width / 2) - (hitbox.Location.X + hitbox.Width / 2), (playerRectangle.Location.Y + playerRectangle.Height / 2) - (hitbox.Location.Y + hitbox.Height / 2));
                    Debug.WriteLine("Räjähdys " + hitbox.Location + ", vektori: "  + difference);


                    if (Math.Abs(difference.Y) > Math.Abs(difference.X))
                    {
                        hit.Y = (((hitbox.Height / 2f) + (playerRectangle.Height / 2f)) - Math.Abs(difference.Y)) * difference.Y / Math.Abs(difference.Y);
                    }
                    else if (Math.Abs(difference.X) > Math.Abs(difference.Y))
                    {
                        hit.X = (((hitbox.Width / 2f) + (playerRectangle.Width / 2f)) - Math.Abs(difference.X)) * difference.X / Math.Abs(difference.X);
                    }
                    else
                    {

                    }

                    playerRectangle.X += (int)(hit.X);
                    playerRectangle.Y += (int)(hit.Y);
                }
            }
            return hit;
        }
    }
}
