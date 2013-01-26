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
    class Hitbox
    {
        public List<Rectangle> hitboxes;
        public Hitbox(Map map)
        {
            hitboxes = new List<Rectangle>();
            foreach (var tile in map.Layers[1].Tiles)
            {
                if (tile.SourceRectangle != new Rectangle(99999999, 99999999, 1, 1))
	            {
                    hitboxes.Add(tile.DestinationRectangle);
	            }
            }
        }

        public void Hit(Vector2 PlayerPosition)
        {
            var playerRectangle = new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, 64, 64);
            foreach (var hitbox in hitboxes)
            {
                if (hitbox.Intersects(playerRectangle))
                {
                    Debug.WriteLine("Räjähdys " + hitbox.Location);
                }
            }
        }
    }
}
