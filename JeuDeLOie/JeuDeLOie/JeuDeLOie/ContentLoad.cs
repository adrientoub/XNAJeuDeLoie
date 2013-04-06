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
        public static Texture2D InfosTexture;
        /* TEXTURE DES */
        public static Texture2D DiceTexture;
        /* TEXURE INTERFACE */
        public static Texture2D InterfTexture;
        /* FONTS */
        public static SpriteFont SpriteFonte;

        /* Sprites personnages */
        public static List<Texture2D> personnages;

        public static void Load()
        {
            CaseTexture = GameData.Content.Load<Texture2D>("texturecaseJDlO");
            DiceTexture = GameData.Content.Load<Texture2D>("texturedesJDlO");
            InfosTexture = GameData.Content.Load<Texture2D>("infosJDlO");
            InterfTexture = GameData.Content.Load<Texture2D>("textureinfJDlO");
            SpriteFonte = GameData.Content.Load<SpriteFont>("SpriteFont");
            
            personnages = new List<Texture2D>();
            personnages.Add(GameData.Content.Load<Texture2D>("BoyPion"));
            personnages.Add(GameData.Content.Load<Texture2D>("CrocoPion"));
            personnages.Add(GameData.Content.Load<Texture2D>("GirlyPion"));
            personnages.Add(GameData.Content.Load<Texture2D>("Moogle"));
        }
    }
}
