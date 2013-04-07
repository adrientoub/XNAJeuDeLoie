using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    /// <summary>
    /// Enumération qui permet de savoir dans quelle direction on va
    /// </summary>
    public enum Dirct
    {
        Droite,
        Bas,
        Gauche,
        Haut,
    }

    /// <summary>
    /// Classe qui crée un Plateau de jeu de l'Oie : 63 Cases, avec Evenements, disposées en spirale
    /// </summary>
    public class Plateau
    {
        #region FIELDS
        // Tableau où seront situées toutes les Cases
        Case[] tab;
        public Case[] Tab { get { return tab; } }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Construit un Plateau de 63 Cases avec leur Evenement associé, disposées en spirale
        /// </summary>
        public Plateau()
        {
            tab = new Case[63];
            ConstructionPositionSpirale(63);
            ConstructionCaseGoose();
            ConstructionCaseEvent();
        }
        #endregion

        #region METHODS
        bool tourne; // permet de savoir si la Case qu'on construit se situe à un virage

       /// <summary>
       /// Construit une spiral de i Cases
       /// </summary>
       /// <param name="i">nombre de Cases à intégrer à la spirale de Cases</param>
        void ConstructionPositionSpirale(int i)
        {
            // Initialisation des variables qui serviront à la construction d'une spirale de Cases
            Dirct dir = Dirct.Droite;
            Vector2 pos = new Vector2(GameData.PreferredBackBufferWidth/4, GameData.PreferredBackBufferHeight / 2.5f);
            int j = 0;

            while(i >= 0)
            {
                j++;

                for (int k = 0; k < j; k++)
                { // contruit une ligne
                    tab[i - 1] = new Case(new Rectangle((int)pos.X,(int)pos.Y, GameData.CaseWidth, GameData.CaseHeight), Event.Nothing, i-1, tourne);
                    tourne = false;
                    switch ((int)dir)
                    {
                        case 0: pos.X += GameData.CaseWidth + 2;
                            break;
                        case 1: pos.Y += GameData.CaseHeight + 2;
                            break;
                        case 2: pos.X -= GameData.CaseWidth + 2;
                            break;
                        case 3: pos.Y -= GameData.CaseHeight + 2;
                            break;
                    }
                    i--;
                    if (i == 0)
                        return;
                }

                // a la fin de la ligne, on change de direction pour la suivante, et la prochaine Case qui sera construire sera spécifiée comme "virage"
                tourne = true;

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

        /// <summary>
        /// Ajoute les Cases à Evenement Oie toutes les 9 Cases du tableau de Cases du Plateau
        /// </summary>
        void ConstructionCaseGoose()
        {
            for (int i = 1; i < 55; i++)
                if (i % 9 == 0)
                    tab[i] = new Case(tab[i].Position, Event.Oie, i, tab[i].isTurning);
        }

        /// <summary>
        /// Ajoute les Cases à Evenement de bonus ou de malus dans le tableau de Cases du Plateau
        /// </summary>
        void ConstructionCaseEvent()
        {
            tab[0] = new Case(tab[0].Position, Event.CaseDep, 0, tab[0].isTurning);
            tab[0].Change4CaseDep();
            tab[6] = new Case(tab[6].Position, Event.Pont, 6, tab[6].isTurning);
            tab[19] = new Case(tab[19].Position, Event.Hotel, 19, tab[19].isTurning);
            tab[31] = new Case(tab[31].Position, Event.Puits, 31, tab[31].isTurning);
            tab[41] = new Case(tab[41].Position, Event.Labyrinthe, 41, tab[41].isTurning);
            tab[52] = new Case(tab[52].Position, Event.Prison, 52, tab[52].isTurning);
            tab[58] = new Case(tab[58].Position, Event.Mort, 58, tab[58].isTurning);
            tab[62] = new Case(tab[62].Position, Event.CaseArr, 62, tab[62].isTurning);
        }
        #endregion

        #region UPDATE & DRAW

        /// <summary>
        /// Update de Plateau update chaque Case contenue dans son tableau de Cases
        /// </summary>
        public void Update()
        {
            foreach (Case Cases in tab)
                Cases.Update();
        }

        /// <summary>
        /// Dessine toutes les Cases contenues dans le tableau de Cases du Plateau
        /// </summary>
        public void Draw()
        {
            foreach (Case Cases in tab)
                Cases.Draw();
        }
        #endregion
    }
}
