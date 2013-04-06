using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    class Interface
    {
        #region FIELDS

        public Vector2 Position { get; set; }


        #endregion

        #region CONSTRUCTORS
        public Interface()
        {
            Position = new Vector2(GameData.PreferredBackBufferWidth - ContentLoad.InterfTexture.Width - 50, GameData.PreferredBackBufferHeight / 7);
            InitBoutonDes();
            InitChronos();
        }
        #endregion

        #region METHODS
        #endregion

        #region UPDATE & DRAW
        public void Update()
        {
            UpdateChrono();
            UpdateBoutonDes();
        }

        public void Draw()
        {
            GameData.SpriteBatch.Draw(ContentLoad.InterfTexture, Position,
                new Rectangle(0, 0, ContentLoad.InterfTexture.Width, 347), Color.White);

            #region Partie haute
            // Affichage du chrono
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, chronos, new Vector2(Position.X + 10, Position.Y + 10), Color.White); 
            #endregion
            #region Partie basse
            #endregion
            // Affichage des dés/bouton
            DrawBoutonDes();

        }
        #endregion



        #region Gestion du chrono de jeu
        int secondes, minutes, heures, jours;
        float timer;
        bool chronosIsRunning;
        string chronos;
        public void InitChronos()
        {
            timer = 0f;
            chronosIsRunning = true;
            chronos = "Temps de jeu : " + jours + "jours " + heures + "h " + minutes + "min " + secondes + "s";
        }

        public void UpdateChrono()
        {
            timer += (float)GameData.GameTime.ElapsedGameTime.TotalMilliseconds; 
            if (timer >= secondes*1000 +1)
            {
                secondes = (int)timer / 1000;
                minutes = secondes / 60;
                heures = minutes / 60;
                minutes %= 60;
                jours = heures / 24;
                heures = heures % 24;
                chronos = "Temps de jeu : " + jours + "jours " + heures + "h " + minutes + "min " + (secondes%60) + "s";
            }
        }

        #endregion
        #region Gestion du bouton et des dés
        string textebouton;
        bool deslances;
        Rectangle rectBouton;
        Color colorbouton;
        Dices dices;
        bool intersectsMouse;

        void InitBoutonDes()
        {
            colorbouton = Color.White;
            textebouton = "     Lancer les dés";
            deslances = false;
            intersectsMouse = false;
            dices = new Dices(new Rectangle(GameData.PreferredBackBufferWidth - 75 * 2 - 60, (int)Position.Y + 347 + 20, 75, 75));
            rectBouton = new Rectangle(GameData.PreferredBackBufferWidth - 75 * 2 - 120 - 20 - 60, (int)Position.Y + 347 + 20, 120, 37);
        }

        void UpdateBoutonDes()
        {
            #region Changement de couleurs lorsqu'on passe la souris dessus
            if (!intersectsMouse && !deslances
                && rectBouton.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)))
            {
                colorbouton.R -= 50;
                colorbouton.G -= 75;
                intersectsMouse = true;
            }
            else if (!deslances && !rectBouton.Intersects(new Rectangle(GameData.MouseState.X, GameData.MouseState.Y, 3, 3)))
            {
                intersectsMouse = false;
                colorbouton = Color.White;
            }
            #endregion

            #region Lorsqu'on clique sur le bouton gauche de la souris...
            if (GameData.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameData.PreviousMouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                // si la souris est sur le bouton
                if (intersectsMouse)
                {
                    if (textebouton == "        Fin de tour")
                    {
                        // ici, on appuie pour la fin du tour, les dés sont réinitialisés et on passe au joueur suivant
                        textebouton = "     Lancer les dés";
                        dices.ReInit();
                    }
                    else if (!deslances)
                    {
                        // ici, on appuie pour lancer les dés
                        dices.RollDices();
                        deslances = true;
                        textebouton = "      ...";
                    }
                }
            }
            #endregion

            #region Si les dés ont été lancés
            if (deslances)
            {
                // Lorsque les dés roulent, le bouton est "verouillé", meme si la souris n'est plus sur le bouton
                if (!intersectsMouse)
                {
                    colorbouton.R -= 50;
                    colorbouton.G -= 75;
                }

                // si les dés ont fini de rouler, et de déplacer le perso + faire l'event, on change de texte
                if (!dices.IsRolling /* && FIXME */)
                {
                    deslances = false;
                    textebouton = "        Fin de tour";
                }
            }
            #endregion
            dices.Update();
        }

        void DrawBoutonDes()
        {
            GameData.SpriteBatch.Draw(ContentLoad.InterfTexture, rectBouton, new Rectangle(0, 347, rectBouton.Width, rectBouton.Height), colorbouton);
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, textebouton, new Vector2 (rectBouton.X, rectBouton.Y + 7), colorbouton);
            dices.Draw();
        }
        #endregion

    }

    /* Classe qui code pour l'interface, avec laquelle le joueur interagit.
     * Elle se situera à droite de l'écran de jeu et sera divisée en deux grandes parties.
     * Elle contient les informations sur la partie : 
     *      PARTIE GENERALE (en haut)
     *      - nombre de joueurs
     *      - temps de jeu
     *      - à quel tour sommes-nous
     *      - le classement des joueurs par rapport à l'arrivée 
     *      (si on passe sa souris sur un joueur, on accède à certaines de ses infos :  - nom, pion
     *                                                                                  - position dans le classement, case
     *                                                                                  - si il a un malus)
     *      PARTIE STATUT du current joueur, couleur selon le joueur ? (en bas)
     *      - nom, pion
     *      - position dans le classement, case
     *      - si il a un malus
     *      - lancer de dés (bouton)
     *      - fin de tour (bouton)
     *      - abandonner la partie (bouton) (?)
     * 
     * */

    /* Doit-elle etre mise à joueur à chaque tour de joueur ou utilise-t-elle directemement les données des joueurs ?
     * */
}
