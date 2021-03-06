﻿using System;
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
        End,
        Menu,
        Credits,
    }

    public static class Data
    {
        public static State GameState = State.Menu;
    }
}
