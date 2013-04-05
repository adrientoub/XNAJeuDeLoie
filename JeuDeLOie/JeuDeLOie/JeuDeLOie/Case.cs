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
        public Rectangle Position { get { return position; } }
        Rectangle OuSurLaTexture;
        public Color color;
        public int Numero { get; set; }

        bool intersectsMouse;
        string infos;
        #endregion

        #region CONSTRUCTOR
        public Case(Rectangle position, Event evenement, int numero)
        {
            this.evenement = evenement;
            Numero = numero;
            infos = "Case number " + Numero + "\nInformations : ";
            InitInfos();
            this.position = position;
            color = Color.White;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialise le rectangle de destination de la texture, selon l'event
        /// </summary>
        void InitInfos()
        {
            switch (evenement)
            {
                case Event.CaseArr:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 0, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case d'arrivee\n  Votre but dans la vie, c'est d'arriver \n  pile poil sur cette case pour gagner.";
                    break;
                case Event.CaseDep:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 1, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case de depart\n  Tous les joueurs commencent ici, \n  mais peuvent aussi y retourner \n  au cours de leur aventure.";
                    break;
                case Event.Hotel:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 2, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Hôtel\nOuhlàlà, les bonnes choses à manger du buffet et les lits douillets ! \n  Vous devez passer 2 tours si vous vous arrêtez ici.";
                    break;
                case Event.Labyrinthe:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 3, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Labyrinthe\n  C'était à gauche qu'il fallait prendre ! Vous vous êtes perdu. \n  Retournez donc à la case 30.";
                    break;
                case Event.Mort:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 4, GameData.CaseHeight * 0, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Bad Oie\n  ...Malheureuse rencontre ! \n  Vous êtes mort. \n  Retournez sur la case de départ.";
                    break;
                case Event.Nothing:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 0, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Vide\nC'est totalement inutile de s'attarder ici, il ne s'y passe jamais rien.";
                    break;
                case Event.Oie:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 1, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Oie\nC'est votre tour de chance ! Vous rencontrez une oie et avancez encore du même nombre de case.";
                    break;
                case Event.Pont:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 2, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Pont\nSerait-ce un raccourci ? Vous empruntez le pont et atterrissez en case 12.";
                    break;
                case Event.Prison:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 3, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Prison\n  Vous n'auriez pas du le faire...\n  Vous devez maintenant attendre que quelqu'un passe prendre votre place.";
                    break;
                case Event.Puits:
                    OuSurLaTexture = new Rectangle(GameData.CaseWidth * 4, GameData.CaseHeight * 1, GameData.CaseWidth, GameData.CaseHeight);
                    infos += "Case Puits\n  Vous n'avez pas regardé où vous alliez, et hop! dans le puits. \n  Vous y resterez 2 tours sauf si quelqu'un y tombe avant, \n  prenant votre place au fond du trou.";
                    break;

            }
        }
        #endregion

        #region UPDATE & DRAW

        public void Update()
        {
            // Si la souris est placée sur une case, cette dernière change de couleur
            if (!intersectsMouse
                && position.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)))
            {
                color.R -= 50;
                color.G -= 75;
                intersectsMouse = true;
            }
            else if (!position.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)))
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
            {
                GameData.SpriteBatch.Draw(ContentLoad.InfosTexture, new Vector2(GameData.PreferredBackBufferWidth / 2 + 2, position.Y), Color.White);
                GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, infos, new Vector2(GameData.PreferredBackBufferWidth / 2 + 12, position.Y+10), Color.Indigo);
                
            }
            /* Information a poper :
             * - image++ de la case
             * - numéro de la case
             * - event de la case
             * - eventuellement la personne qui est dessus et si elle a un problème (ex : attendre 2 tours)
             * */

        }
        #endregion
    }
}
