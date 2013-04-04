using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JeuDeLOie
{
    public class Case
    {
        #region FIELDS
        Event evenement;
        public Event Evenement { get { return evenement; } set { evenement = value; } }

        Rectangle position; // position du sprite de la case pour l'affichage
        public Rectangle Position { get { return position;} }
        Rectangle OuSurLaTexture;
        public Color color;

        bool intersectsMouse;
        #endregion

        #region CONSTRUCTOR
        public Case(Rectangle position, Event evenement)
        {
            this.evenement = evenement;
            InitOuSurLaTexture();
            this.position = position;
            color = Color.White;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialise le rectangle de destination de la texture, selon l'event
        /// </summary>
        void InitOuSurLaTexture()
        {
            switch (evenement)
            {
                case Event.CaseArr: 
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 0, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.CaseDep :
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 1, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Hotel:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 2, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Labyrinthe:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 3, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Mort:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 4, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Nothing:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 0, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Oie:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 1, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Pont:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 2, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Prison:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 3, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    break;
                case Event.Puits:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 4, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    break;

            }
        }
        #endregion

        #region UPDATE & DRAW

        public void Update()
        {
            // Si la souris est placée sur une case, cette dernière change de couleur
            if (!intersectsMouse
                && position.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 1, 1)))
            {
                color.R -= 50;
                color.G -= 75;
                intersectsMouse = true;
            }
            else if(!position.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 1, 1)))
            {
                intersectsMouse = false;
                color = Color.White;
            }

            
        }

        public void Draw()
        {
            GameData.SpriteBatch.Draw(ContentLoad.CaseTexture, position, OuSurLaTexture, color);
            // si la souris est sur une case, on affiche ses propriétés
            if (intersectsMouse)
                GameData.SpriteBatch.Draw(ContentLoad.CaseTexture, new Vector2(GameData.PreferredBackBufferWidth / 2, GameData.PreferredBackBufferHeight/ 10), Color.White);
        }
        #endregion
    }
}
