using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace JeuDeLOie
{
    public class ContentLoad
    {
        #region Textures cases
        public static Texture2D CaseTexture; // Texture qui contient tous les sprites des cases
     
        #endregion


        public static void Load()
        {
            CaseTexture = GameData.Content.Load<Texture2D>("texturecaseJDlO");

        }
    }
}
