using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    /// <summary>
    /// Classe qui regroupe les propriétés statiques du jeu
    /// </summary>
    public static class GameData
    {
        public static ContentManager Content;

        public static SpriteBatch SpriteBatch;

        public static int CaseWidth = 50;
        public static int CaseHeight = 50;

        public static MouseState MouseState;
        public static MouseState PreviousMouseState;

        public static int PreferredBackBufferHeight = 9 * 80;
        public static int PreferredBackBufferWidth = 16 * 80;

        public static Random Random = new Random();

        public static GameTime GameTime;

        public static KeyboardState presentKey, pastKey;
 
    }
}
