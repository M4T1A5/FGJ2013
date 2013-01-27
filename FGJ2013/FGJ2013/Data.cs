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
using FuncWorks.XNA.XTiled;
using System.Diagnostics;

namespace FGJ2013
{
    public enum State
    {
        None = 0,
        Start,
        Play,
        End
    }

    public static class Data
    {
        public static State GameStates = State.Play;

        public static int DrugsCount = 0;

        public static void AddDrug()
        {
            DrugsCount++;
            switch (DrugsCount)
            {
                case 1: // change props
                    break;
                case 2: // change enemies
                    break;
                case 3: // change player
                    break;
                case 4: // change maptextures
                    break;
                case 5: // win game
                    break;
                default:
                    break;
            }
        }

        public static Texture2D PlayerTexture;
        public static Texture2D PlayerTextureDark;
        public static Texture2D DoctorTexture;
        public static Texture2D DoctorTextureDark;
        public static Texture2D NurseTexture;
        public static Texture2D NurseTextureDark;

        public static Texture2D WorldTileSheet;
        public static Texture2D WorldTileSheetDark;
        public static Texture2D PropsTileSheet;
        public static Texture2D PropsTileSheetDark;

    }
}
