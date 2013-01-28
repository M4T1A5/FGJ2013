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
        //public List<Rectangle> doors1;
        //public List<Rectangle> doors2;
        //public List<Rectangle> doors3;
        //public List<Rectangle> doors4;
        //public List<Rectangle> doors5;
        public List<List<Rectangle>> doors;
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
            doors = new List<List<Rectangle>>
                {
                    new List<Rectangle>(),
                    new List<Rectangle>(),
                    new List<Rectangle>(),
                    new List<Rectangle>(),
                    new List<Rectangle>()
                };



            for (int i = 0; i < 5; i++)
            {
                foreach (var tiles in map.TileLayers[i + 4].Tiles)
                {
                    foreach (var tile in tiles)
                    {
                        if (tile != null)
                        {
                            doors[i].Add(tile.Target);
                        }
                    }
                } 
            }


            //foreach (var tiles in map.TileLayers[2].Tiles)
            //{
            //    foreach (var tile in tiles)
            //    {
            //        if (tile != null)
            //        {
            //            doors1.Add(tile.Target);
            //        }
            //    }
            //}
            //foreach (var tiles in map.TileLayers[3].Tiles)
            //{
            //    foreach (var tile in tiles)
            //    {
            //        if (tile != null)
            //        {
            //            doors2.Add(tile.Target);
            //        }
            //    }
            //}
            //foreach (var tiles in map.TileLayers[4].Tiles)
            //{
            //    foreach (var tile in tiles)
            //    {
            //        if (tile != null)
            //        {
            //            doors3.Add(tile.Target);
            //        }
            //    }
            //}
            //foreach (var tiles in map.TileLayers[5].Tiles)
            //{
            //    foreach (var tile in tiles)
            //    {
            //        if (tile != null)
            //        {
            //            doors4.Add(tile.Target);
            //        }
            //    }
            //}
            //foreach (var tiles in map.TileLayers[6].Tiles)
            //{
            //    foreach (var tile in tiles)
            //    {
            //        if (tile != null)
            //        {
            //            doors5.Add(tile.Target);
            //        }
            //    }
            //}
        }

        public Vector2 MapHit(Vector2 CharacterPosition)
        {
            Vector2 hit = Vector2.Zero;
            var playerRectangle = new Rectangle((int)CharacterPosition.X + 10, (int)CharacterPosition.Y + 45, 35, 35);
            foreach (Rectangle hitbox in hitboxes)
            {
                if (hitbox.Intersects(playerRectangle))
                {
                    Vector2 difference = new Vector2((playerRectangle.Location.X + playerRectangle.Width / 2) - (hitbox.Location.X + hitbox.Width / 2),
                        (playerRectangle.Location.Y + playerRectangle.Height / 2) - (hitbox.Location.Y + hitbox.Height / 2));

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

        public bool PlayerHit(Rectangle ARect,Rectangle BRect)
        {
            if (ARect.Intersects(BRect))
                return true;
            else
                return false;
        }

        public void AtDoor(Player player, int DrugsCount)
        {
            var playerRectangle = new Rectangle((int)player.position.X + 10, (int)player.position.Y + 45, 35, 35);
            foreach (var door in doors[DrugsCount])
            {
                if (door.Intersects(new Rectangle((int)player.position.X + 9,
                    (int)player.position.Y + 44, 37, 37)))
                {
                    Vector2 difference = new Vector2((playerRectangle.Location.X + playerRectangle.Width / 2) - (door.Location.X + door.Width / 2),
                        (playerRectangle.Location.Y + playerRectangle.Height / 2) - (door.Location.Y + door.Height / 2)); // vector from door to player

                    if (Math.Abs(difference.Y) > Math.Abs(difference.X))
                    {
                        if (difference.Y < 0) // above
                        {
                            player.position.Y += door.Height * 4;
                            break;
                        }
                        else if (difference.Y > 0) // below
                        {
                            player.position.Y -= door.Height * 4;
                            break;
                        }

                    }
                    else if (Math.Abs(difference.X) > Math.Abs(difference.Y))
                    {
                        if (difference.X < 0) // left
                        {
                            player.position.X += door.Width * 4;
                            break;
                        }
                        else if (difference.X > 0) // right
                        {
                            player.position.X -= door.Width * 4;
                            break;
                        }
                    }

                    Debug.WriteLine("Suddenly door " + door.Location + player.position);
                }
            } 
        }
    }
}
