﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    /* Classe qui code pour l'interface, avec laquelle le joueur interagit.
     * 
     * Elle se situera à droite de l'écran de jeu et sera divisée en deux grandes parties.
     * Elle contient les informations sur la partie : 
     *      PARTIE GENERALE (en haut)
     *      - nombre de joueurs
     *      - temps de jeu
     *      - la progression des joueurs  
     *      
     *      PARTIE STATUT du current joueur (en bas)
     *      - pion
     *      - case
     *      - si il a un malus
     *      - lancer de dés (bouton)
     *      - fin de tour (bouton)
     *      
     * Enfin, le bouton et les dés permettront au joueur de faire progresser son Pion sur le Plateau.
     * 
     * */

    /// <summary>
    /// Classe qui permet d'afficher une Interface comprenant quelques informations sur le jeu en cours
    /// </summary>
    class Interface
    {
        #region FIELDS
        public Vector2 Position { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Construit une Interface pour le jeu
        /// </summary>
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
        /// <summary>
        /// Update des différents éléments de l'Interface
        /// </summary>
        public void Update()
        {
            UpdateChrono();
            UpdateBoutonDes();
        }

        /// <summary>
        /// Dessine des différents éléments de l'Interface
        /// </summary>
        public void Draw()
        {
            GameData.SpriteBatch.Draw(ContentLoad.InterfTexture, Position,
                new Rectangle(0, 0, ContentLoad.InterfTexture.Width, 347), Color.White);

            #region Partie haute
            // Affichage des infos
            DrawPartieH();
            // Affichage du chrono
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, chronos, new Vector2(Position.X + 20, Position.Y + 150), Color.White);
            #endregion
            #region Partie basse
            DrawPartieB();
            #endregion
            // Affichage des dés/bouton
            DrawBoutonDes();

        }
        #endregion



        #region Gestion des données de la partie haute
        /* Méthodes et variables qui gèrent les éléments de la partie haute : données générales sur la partie en cours */

        int nombrePlayer;
        string textenbplayer;
        string texteclassementjoueur;
        
        /// <summary>
        /// Initialise les données générales du jeu, selon la sélection faite par les utilisatuers
        /// </summary>
        public void InitDonneesGen()
        {
            nombrePlayer = Game1.joueurs.Length;
            textenbplayer = "Nombre de joueurs : ";
            for (int i = 0; i < nombrePlayer; i++)
            {
                textenbplayer += "* ";
            }
        }

        /// <summary>
        /// Dessine les éléments concernant les informations générales du jeu en cours sur l'Interface
        /// </summary>
        public void DrawPartieH()
        {
            // Nombre de joueurs
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, textenbplayer, new Vector2(Position.X + 20, Position.Y + 20), Color.White);

            // Progression des joueurs dans la partie en cours
            // On écrit d'abord un texte selon la situation du joueur, puis on affiche le sprite de son pion
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, "Progression actuelle : ", new Vector2(Position.X + 20, Position.Y + 40), Color.White);
            for (int i = 0; i < nombrePlayer; i++)
            {
                GameData.SpriteBatch.Draw(Game1.joueurs[i].Pion.PionPerso, new Rectangle((int)Position.X + 50 + 200 * (i / 2), (int)Position.Y + 60 + 40 * (i % 2), Game1.joueurs[i].Pion.WidthPion, Game1.joueurs[i].Pion.HeightPion),
                    new Rectangle(Game1.joueurs[i].Pion.WidthPion * Game1.joueurs[i].Pion.Column, Game1.joueurs[i].Pion.HeightPion * Game1.joueurs[i].Pion.Line, Game1.joueurs[i].Pion.WidthPion, Game1.joueurs[i].Pion.HeightPion),
                Color.White);
                texteclassementjoueur = "#" + (i + 1) + "               case :" + Game1.joueurs[i].Case;
                if (Game1.joueurs[i].CoolDown != 0)
                {
                    if (Game1.joueurs[i].CoolDown <= 2)
                        texteclassementjoueur += "\n                   Malus : attente de " + Game1.joueurs[i].CoolDown + " tours";
                    else
                        texteclassementjoueur += "\n                   Malus : attente à l'infini";
                }
                GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, texteclassementjoueur, new Vector2((int)Position.X + 20 + 200 * (i / 2), (int)Position.Y + 65 + 40 * (i % 2)), Color.White);
            }

        }
        #endregion

        #region Gestion des données de la partie basse
        /* Méthodes et variables qui gèrent les éléments de la partie bass : informations du joueur dont le tour se passe actuellement */

        Joueur currentPlayer;
        string textejoueur;
        Color colorTextejoueur;

        /// <summary>
        /// Récupération du Joueur qui joue actuellement
        /// </summary>
        /// <param name="player">Joueur dont c'est le tour de jouer</param>
        public void GetCurrentPlayer(Joueur player)
        {
            currentPlayer = player;
            if (currentPlayer.CoolDown == 0)
            { textejoueur = "Joueur " + (currentPlayer.Tour + 1) + ", c'est à ton tour de jouer (:\n"; colorTextejoueur = Color.LimeGreen; }
            else if (currentPlayer.CoolDown <= 2)
            { textejoueur = "Joueur " + (currentPlayer.Tour + 1) + ", dommage, il faut encore attendre " + currentPlayer.CoolDown + " tours avant de pouvoir jouer..."; colorTextejoueur = Color.Gold; }
            else
            { textejoueur = "Joueur " + (currentPlayer.Tour + 1) + ", vous êtes en Prison ! Impossible d'avancer si on ne vous libère pas D:"; colorTextejoueur = Color.Red; }
        }

        /// <summary>
        /// Dessine les informations du Joueur dont c'est le tour de jouer
        /// </summary>
        public void DrawPartieB()
        {
            // Affichage du portrait du joueur
            GetCurrentPlayer(Game1.joueurs[Game1.tourActuel % Game1.nbjoueurs]);
            GameData.SpriteBatch.Draw(currentPlayer.Pion.ImagePerso,
                new Rectangle((int)Position.X + 2, (int)Position.Y + 347 + 10, 200, 200),
                currentPlayer.Pion.Portrait, Color.White);

            // Affichage du pion du joueur
            GameData.SpriteBatch.Draw(currentPlayer.Pion.PionPerso, new Vector2(Position.X + 100, Position.Y + 250),
                new Rectangle(currentPlayer.Pion.WidthPion * currentPlayer.Pion.Column, currentPlayer.Pion.HeightPion * currentPlayer.Pion.Line, currentPlayer.Pion.WidthPion, currentPlayer.Pion.HeightPion),
                Color.White);

            // Affichage d'infos écrites
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, textejoueur, new Vector2(Position.X + 20, Position.Y + 300), colorTextejoueur);
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, Game1.plate.Tab[currentPlayer.Case].Infos, new Vector2(Position.X + 20 + 280, Position.Y + 190), Color.Indigo);
        }



        #endregion

        #region Gestion du chrono de jeu
        /* Méthodes et variables qui gèrent le chronomètre de l'Interface */

        int secondes, minutes, heures, jours;
        float timer;
        string chronos;

        /// <summary>
        /// Initialise le chronomètre
        /// </summary>
        public void InitChronos()
        {
            timer = 0f;
            chronos = "Temps de jeu : " + jours + "jours " + heures + "h " + minutes + "min " + secondes + "s";
        }

        /// <summary>
        /// Update du chronomètre
        /// </summary>
        public void UpdateChrono()
        {
            timer += (float)GameData.GameTime.ElapsedGameTime.TotalMilliseconds;
            // toutes les secondes, on ajoute ce qu'il faut pour que le chronomètre avance
            if (timer >= secondes * 1000 + 1)
            {
                secondes = (int)timer / 1000;
                minutes = secondes / 60;
                heures = minutes / 60;
                minutes %= 60;
                jours = heures / 24;
                heures = heures % 24;
                chronos = "Temps de jeu : " + jours + "jours " + heures + "h " + minutes + "min " + (secondes % 60) + "s";
            }
        }

        #endregion

        #region Gestion du bouton et des dés
        /* Méthodes et variables qui gèrent les dés et le bouton qui leur est associé */

        // Dés
        bool deslances;
        public static Dices dices;
        public int DicesResult { get; set; }
        // Bouton
        Rectangle rectBouton;
        Color colorbouton;
        bool intersectsMouse;
        public static string textebouton;

        /// <summary>
        /// Initialise le bouton et les dés
        /// </summary>
        void InitBoutonDes()
        {
            colorbouton = Color.White;
            textebouton = "     Lancer les dés";
            deslances = false;
            intersectsMouse = false;
            dices = new Dices(new Rectangle(GameData.PreferredBackBufferWidth - 75 * 2 - 60, (int)Position.Y + 347 + 20, 75, 75));
            rectBouton = new Rectangle(GameData.PreferredBackBufferWidth - 75 * 2 - 120 - 20 - 60, (int)Position.Y + 347 + 20, 120, 37);
        }

        /// <summary>
        /// Update des dés et du bouton associé
        /// </summary>
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
                if (!dices.IsRolling)
                    deslances = false;
            }
            #endregion

            // Update des dés
            dices.Update();
            DicesResult = dices.DicesResult;
        }

        /// <summary>
        /// Dessine les dés et le bouton associé où il faut et comme il faut sur l'interface
        /// </summary>
        void DrawBoutonDes()
        {
            GameData.SpriteBatch.Draw(ContentLoad.InterfTexture, rectBouton, new Rectangle(0, 347, rectBouton.Width, rectBouton.Height), colorbouton);
            GameData.SpriteBatch.DrawString(ContentLoad.SpriteFonte, textebouton, new Vector2(rectBouton.X, rectBouton.Y + 7), colorbouton);
            dices.Draw();
        }
        #endregion

    }
}
