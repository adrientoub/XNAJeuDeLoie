using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace JeuDeLOie
{
    public class ContentLoad
    {
        /* TEXTURE CASE */  
        public static Texture2D CaseTexture; // Texture qui contient tous les sprites des cases
        /* TEXTURE DES */
        public static Texture2D DiceTexture;

        public static void Load()
        {
            CaseTexture = GameData.Content.Load<Texture2D>("texturecaseJDlO");
            DiceTexture = GameData.Content.Load<Texture2D>("texturedesJDlO");
        }
    }
}
