using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    /* README
     * 
     * Ceci est une classe test pour afficher les cases en spirales. Sa fonction d'affichage est correcte, il faut juste en faire quelque chose :
     * utilisation d'un tableau de cases, où chaque case à une position définie grâce à une fonctionne qui ressemble à AfficheCarreQuiTourne
     * Anaïs va s'en occuper
     * 
     * */
    class AffichageSpriale
    {
        int[] tab;
        Texture2D tex;
        Vector2 posDep;
        Color color;

        public void LoadContent(ContentManager content, string asset)
        {
            tex = content.Load<Texture2D>(asset);
            tab = new int[63];
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = 0;
            }
            color = Color.Black;
        }



        void DessineCarreQuiTourne(int i, SpriteBatch spb)
        {
            Dirct dir = Dirct.Droite;
            Vector2 pos = new Vector2(350, 200);
            for (int j = 0; j <= i; j++)
            {
                for (int k = 0; k < j; k++)
                {
                    spb.Draw(tex, pos, color);
                    switch ((int)dir)
                    {
                        case 0: pos.X += tex.Width + 2;
                            break;
                        case 1: pos.Y += tex.Height + 2;
                            break;
                        case 2: pos.X -= tex.Width + 2;
                            break;
                        case 3: pos.Y -= tex.Height + 2;
                            break;
                    }
                    color.A += 51;
                    color.A.Equals(100);
                    i--;
                }

                switch ((int)dir)
                {
                    case 0: dir = Dirct.Bas;
                        break;
                    case 1: dir = Dirct.Gauche;
                        break;
                    case 2: dir = Dirct.Haut;
                        break;
                    case 3: dir = Dirct.Droite;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spb)
        {
            DessineCarreQuiTourne(100, spb);
        }


        #region Test de vraie spirale
        // Equation d'une spirale d'Archi : r = a + b(theta)
        // Spirale "simple" : r = - a / (pi)
        public void DrawCaseSpirale(SpriteBatch spriteBath, float a, int i)
        {
          /*  a = a + 5;
            float angle = (float)(a * 2 / Math.PI);
            float x = (float)((-angle* 2 / Math.PI) * Math.Cos(2 / Math.PI));
            float y = (float)((-angle* 2 / Math.PI) * Math.Sin(2 / Math.PI)); */
             // a = a + 20;
           /* float angle = (float)(a * Math.PI / 2); */
            i = i * tex.Height/2;
            float x = (float)((i / a) * Math.Cos(a));
            float y = (float)((i / a) * Math.Sin(a));
            spriteBath.Draw(tex,
                new Vector2(300 + x - tex.Width/2, 300 + y - tex.Height/2), new Rectangle(0, 0, tex.Width, tex.Height),
                Color.White,
               a, new Vector2(tex.Width/2, tex.Height), Vector2.One, SpriteEffects.None, 0f);
            

        }
        public void DrawS(SpriteBatch spriteBatch)
        {
            float angle = 0f;
            for (int i = 0; i < tab.Length; i++)
            {
                DrawCaseSpirale(spriteBatch, angle, i);
                angle += (float)(Math.PI / 12);
            }
        }
        #endregion
    }

    public enum Dirct
    {
        Droite,
        Bas,
        Gauche,
        Haut,
    }
}
