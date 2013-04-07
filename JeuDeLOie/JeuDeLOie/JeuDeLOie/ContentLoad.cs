using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

        public static int screenWidth, screenHeight;
        public static cButton btnPlay, btnQuit, btn2, btn3, btn4;

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
            personnages.Add(GameData.Content.Load<Texture2D>("GirlPion"));
            personnages.Add(GameData.Content.Load<Texture2D>("MooglePion"));

            
            screenHeight = Game1.graphics.PreferredBackBufferHeight;
            screenWidth = Game1.graphics.PreferredBackBufferWidth;

            //Button
            btnPlay = new cButton(GameData.Content.Load<Texture2D>("PlayButton"), Game1.graphics.GraphicsDevice, 100, 75);
            btnPlay.setPosition(new Vector2(screenWidth / 2 - btnPlay.size.X / 2, screenHeight / 2 - 150));
            btnQuit = new cButton(GameData.Content.Load<Texture2D>("QuitButton"), Game1.graphics.GraphicsDevice, 100, 75);
            btnQuit.setPosition(new Vector2(screenWidth / 2 - btnQuit.size.X / 2, screenHeight / 2 - 50));
            btn2 = new cButton(GameData.Content.Load<Texture2D>("Button2"), Game1.graphics.GraphicsDevice, 150, 150);
            btn2.setPosition(new Vector2( 225, screenHeight / 2 - 75 ));
            btn3 = new cButton(GameData.Content.Load<Texture2D>("Button3"), Game1.graphics.GraphicsDevice, 150, 150);
            btn3.setPosition(new Vector2(screenWidth / 2 - 195 , screenHeight / 2 + 125 ));
            btn4 = new cButton(GameData.Content.Load<Texture2D>("Button4"), Game1.graphics.GraphicsDevice, 150, 150);
            btn4.setPosition(new Vector2(screenWidth / 2 - btn4.size.X / 2 + 300, screenHeight / 2 + 50));

        }
    }
}
