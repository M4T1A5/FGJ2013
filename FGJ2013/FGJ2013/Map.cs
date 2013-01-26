using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FGJ2013
{
    // This is not the most efficient way to store the data or perform layer rendering. It is, however,
    // close to the simplest way to interact with TiledLib in order to get data from the Tiled editor
    // to the game using custom game types.

    public static class Camera
    {
        public static Vector2 Position = Vector2.Zero;
    }

    public class Tile
    {
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public SpriteEffects SpriteEffects;
        public Rectangle DestinationRectangle; //= Rectangle.Empty;
    }

    public class Layer
    {
        public int Width;
        public int Height;
        public Tile[] Tiles;
    }

    public class Map
    {
        public int TileWidth;
        public int TileHeight;
        public List<Layer> Layers = new List<Layer>();

        public void Draw(SpriteBatch spriteBatch, Vector2 PlayerPosition)
        {
            var playerTileX = (int)Math.Floor((PlayerPosition.X + 17) / 25); 
            var playerTileY = (int)Math.Floor((PlayerPosition.Y + 50) / 25);

            var drawSource = Rectangle.Empty;
            foreach (var l in Layers)
            {
                drawSource = Layers[Layers.Count - 1].Tiles[playerTileY * l.Width + playerTileX].SourceRectangle; 

                spriteBatch.Begin();
                var colour = (int)Camera.Position.Length();

                for (int y = 0; y < l.Height; y++)
                {
                    for (int x = 0; x < l.Width; x++)
                    {
                        Tile t = l.Tiles[y * l.Width + x];
                        Tile checkTile = Layers[Layers.Count - 1].Tiles[y * l.Width + x];
                        t.DestinationRectangle = new Rectangle(x * TileWidth + (int)Camera.Position.X, y * TileHeight + (int)Camera.Position.Y, TileWidth, TileHeight);

                        if (l != Layers[Layers.Count - 1] && l != Layers[Layers.Count - 2])
                        {
                            if (checkTile.SourceRectangle == drawSource) //|| (l != Layers[Layers.Count - 1] && l != Layers[Layers.Count - 2]))
                            {
                                spriteBatch.Draw(
                                    t.Texture,
                                t.DestinationRectangle,
                                    t.SourceRectangle,
                                    Color.White,
                                    0,
                                    Vector2.Zero,
                                    t.SpriteEffects,
                                    0);
                            }
                            else
                            {
                                spriteBatch.Draw(
                                    t.Texture,
                                t.DestinationRectangle,
                                    t.SourceRectangle,
                                    Color.Black,
                                    0,
                                    Vector2.Zero,
                                    t.SpriteEffects,
                                    0);
                            } 
                        }
                    }                    
                }
                    spriteBatch.End();
            }
        }
    }
}
