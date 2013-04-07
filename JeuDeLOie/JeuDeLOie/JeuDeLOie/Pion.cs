using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JeuDeLOie
{
    public class Pion
    {

        #region FIELDS
        /* Position du Pion */
        Rectangle position;
        public Rectangle Position { get { return position; } set { position = value; } }

        /* Dimensions du Pion */
        int widthpion, heightpion;
        public int WidthPion { get { return widthpion; } }
        public int HeightPion { get { return heightpion; } }

        /* Caractéristiques visuelles du Pion, selon le personnage qu'il représente */
        Texture2D pionPerso;
        public Texture2D PionPerso { get { return pionPerso; } }
        Texture2D imagePerso;
        public Texture2D ImagePerso { get { return imagePerso; } }
        Rectangle portrait;
        public Rectangle Portrait { get { return portrait; } }

        /* Permettent de savoir où le pion va se diriger */
        Dirct direction;
        Case positionCase;
        Case previousCase;
        Case nextCase;
        Case newCase;

        /* Variables utilisées pour l'animation du Pion */
        int line, column;
        public int Line { get { return line; } }
        public int Column { get { return column; } }
        int casedepX, casedepY;

        /* Permettent de connaître l'état du Pion */
        bool isDeplacing;
        public bool IsDeplacing { get { return isDeplacing; } }
        bool isMoving;
        public bool IsMoving { get { return isMoving; } }

        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Construit un Pion associé au personnage donné en paramètre, ainsi que son tour de jeu
        /// </summary>
        /// <param name="namePerso">nom du personnage que Pion représentera</param>
        /// <param name="tour">tour auquel Pion jouera</param>
        public Pion(string namePerso, int tour)
        {
            // Initialise les bonnes images et les rectangles selon le perso choisi
            imagePerso = GameData.Content.Load<Texture2D>("Personnages/" + namePerso);
            switch (namePerso)
            {
                case "Crocodile":
                    pionPerso = ContentLoad.personnages[1];
                    widthpion = 34; heightpion = 45;
                    portrait = new Rectangle(100, 0, 440, 440);
                    break;
                case "Mai":
                    pionPerso = ContentLoad.personnages[2];
                    widthpion = 22; heightpion = 34;
                    portrait = new Rectangle(0, 0, 300, 300);
                    break;
                case "Moogle":
                    pionPerso = ContentLoad.personnages[3];
                    widthpion = 18; heightpion = 24;
                    portrait = new Rectangle(0, 0, 388, 388);
                    break;
                case "Conan":
                    pionPerso = ContentLoad.personnages[0];
                    widthpion = 23; heightpion = 32;
                    portrait = new Rectangle(100, 0, 900, 900);
                    break;
                default: pionPerso = ContentLoad.personnages[0];
                    widthpion = 23; heightpion = 32;
                    portrait = new Rectangle(0, 0, 388, 388);
                    break;
            }

            // Initialisation des cases
            positionCase = Game1.plate.Tab[0]; // case où est actuellement le pion
            nextCase = Game1.plate.Tab[1]; // case qui suit positionCase
            previousCase = null; // case qui précède positionCase

            // on place le pion au bon endroit sur la case de départ
            casedepY = nextCase.Position.Y + GameData.CaseHeight / 2;
            casedepX = nextCase.Position.X - 2 - GameData.CaseWidth + GameData.CaseWidth / 2;
            position = new Rectangle(casedepX, casedepY, widthpion, heightpion);

            // Initialisation pour l'animation et la direction du déplacement
            line = 3; column = 1;
            direction = Dirct.Droite; // on commence par aller à droite
        }
        #endregion

        #region METHODS
        #region AVANCE
        /* Méthodes et variable qui permettront la progression avant du Pion */

        bool isDeplacingAvance;
        public bool IsDeplacingAvance { get { return isDeplacingAvance; } }

        /// <summary>
        ///  On donne au pion sa prochaine destination
        /// </summary>
        /// <param name="newcase">case de destination</param>
        public void ChangeCaseAvance(int newcase)
        {
            if (!isDeplacing)
            {
                isDeplacingAvance = true;
                newCase = Game1.plate.Tab[newcase];
                timer = 0;
                column = 1;
            }
        }

        /// <summary>
        /// Lance une progression avant du Pion, si il doit la faire
        /// </summary>
        public void AnimationChangeCaseAvance()
        {// tant que le pion n'est pas sur la nouvelle case, on continue d'avancer
            if (isDeplacingAvance) // si le pion avance
            {
                BougeLesPieds(); // animation

                // On avance selon la direction
                switch (direction)
                {
                    case Dirct.Bas: position.Y++;
                        break;
                    case Dirct.Droite: position.X++;
                        break;
                    case Dirct.Gauche: position.X--;
                        break;
                    case Dirct.Haut: position.Y--;
                        break;
                }

                // lorsqu'on a atteint une nouvelle case
                if (nextCase != null && position.X == nextCase.Position.X + GameData.CaseWidth / 2 && position.Y == nextCase.Position.Y + GameData.CaseHeight / 2)
                {
                    /* On change les données de la case où il se situe, et celle qui sont adjacentes */
                    // si on est arrivé sur une case, la prochaine est la suivante, et l'actuelle est la précédente
                    previousCase = positionCase; // la case previous est la précedente
                    positionCase = nextCase;
                    if (nextCase.Numero != Game1.plate.Tab.Length - 1)
                    {
                        nextCase = Game1.plate.Tab[nextCase.Numero + 1];
                    }
                    else
                    { // cas où il arrive sur l'arrivée
                        isDeplacingAvance = false;
                        nextCase = null;
                    }                    

                    // et si elle est à un virage, on change de direction
                    if (positionCase.isTurning)
                    {
                        switch ((int)direction)
                        {
                            case 0: direction = Dirct.Haut;
                                line = 1;
                                break;
                            case 1: direction = Dirct.Droite;
                                line = 3;
                                break;
                            case 2: direction = Dirct.Bas;
                                line = 0;
                                break;
                            case 3: direction = Dirct.Gauche;
                                line = 2;
                                break;
                        }
                    }

                    // Lorsqu'on est arrivé, on s'arrête
                    if (positionCase == newCase)
                    {
                        isDeplacingAvance = false;
                        column = 1;
                    }
                }
            }
        }
        #endregion

        #region RECULE (pour les petites distances)
        /* Méthodes et variable qui permettront la progression avant du Pion */

        bool isDeplacingRecule;
        public bool IsDeplacingRecule { get { return isDeplacingRecule; } }

        /// <summary>
        ///  On donne au pion sa prochaine destination
        /// </summary>
        /// <param name="newcase">case de destination</param>
        public void ChangeCaseRecule(int newcase)
        {
            if (!isDeplacing)
            {
                isDeplacingRecule = true;
                newCase = Game1.plate.Tab[newcase];
                timer = 0;
                column = 1;

                // et on lui donne une nouvelle direction
                if (positionCase.isTurning)
                {
                    switch (direction)
                    {
                        case Dirct.Bas: direction = Dirct.Droite;
                            line = 2;
                            break;
                        case Dirct.Droite: direction = Dirct.Bas;
                            line = 1;
                            break;
                        case Dirct.Gauche: direction = Dirct.Haut;
                            line = 0;
                            break;
                        case Dirct.Haut: direction = Dirct.Gauche;
                            line = 3;
                            break;
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case Dirct.Bas: direction = Dirct.Haut;
                            line = 0;
                            break;
                        case Dirct.Droite: direction = Dirct.Gauche;
                            line = 3;
                            break;
                        case Dirct.Gauche: direction = Dirct.Droite;
                            line = 2;
                            break;
                        case Dirct.Haut: direction = Dirct.Bas;
                            line = 1;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Lance une progression arrière du Pion, si il doit la faire
        /// </summary>
        public void AnimationChangeCaseRecule()
        {// tant que le pion n'est pas sur la nouvelle case, on continue d'reculer
            if (isDeplacingRecule)
            {
                BougeLesPieds();
                // on recule selon la direction
                switch (direction)
                {
                    case Dirct.Bas: position.Y++;
                        break;
                    case Dirct.Droite: position.X++;
                        break;
                    case Dirct.Gauche: position.X--;
                        break;
                    case Dirct.Haut: position.Y--;
                        break;
                }
                if (previousCase != null && position.X == previousCase.Position.X + GameData.CaseWidth / 2 && position.Y == previousCase.Position.Y + GameData.CaseHeight / 2)
                {
                    // si on est arrivé sur une case, la prochaine est la suivante, et l'actuelle est la précédente
                    nextCase = positionCase;
                    positionCase = previousCase;
                    if (previousCase.Numero != 0)
                        previousCase = Game1.plate.Tab[previousCase.Numero - 1];
                    else
                    {// cas où on arrive sur la première case
                        isDeplacingRecule = false;
                        previousCase = null;
                    }
                    

                    // et si elle est à un virage, on change de direction
                    if (positionCase.isTurning)
                    {
                        switch (direction)
                        {
                            case Dirct.Bas: direction = Dirct.Gauche;
                                line = 3;
                                break;
                            case Dirct.Droite: direction = Dirct.Bas;
                                line = 1;
                                break;
                            case Dirct.Gauche: direction = Dirct.Haut;
                                line = 0;
                                break;
                            case Dirct.Haut: direction = Dirct.Droite;
                                line = 2;
                                break;
                        }
                    }

                    // Lorsqu'on est arrivé, on s'arrête et on se "tourne" vers la bonne direciton
                    if (positionCase == newCase)
                    {
                        isDeplacingRecule = false;
                        column = 1;
                        if (positionCase.isTurning)
                        {
                            switch (direction)
                            {
                                case Dirct.Bas: direction = Dirct.Gauche;
                                    line = 3;
                                    break;
                                case Dirct.Droite: direction = Dirct.Bas;
                                    line = 1;
                                    break;
                                case Dirct.Gauche: direction = Dirct.Haut;
                                    line = 0;
                                    break;
                                case Dirct.Haut: direction = Dirct.Droite;
                                    line = 2;
                                    break;
                            }
                        }
                        else
                            switch (direction)
                            {
                                case Dirct.Bas: direction = Dirct.Haut;
                                    break;
                                case Dirct.Droite: direction = Dirct.Gauche;
                                    break;
                                case Dirct.Gauche: direction = Dirct.Droite;
                                    break;
                                case Dirct.Haut: direction = Dirct.Bas;
                                    break;
                            }
                    }
                }
            }
        }
        #endregion

        #region RETOUR CASE DEP (quasi téléportation)
        /* Méthodes et variable qui renvoient le Pion à la case de départ */

        bool retourDep;
        public bool RetourDep { get { return retourDep; } }

        /// <summary>
        /// Enclenche le retour à la case de départ du Pion
        /// </summary>
        public void ChangeCaseRetourCaseDep()
        {
            if (!isDeplacing)
            {
                retourDep = true;
                line = 0;
                column = 1;
            }
        }

        /// <summary>
        /// Lance le retour du Pion à la case de départ, si il doit le faire
        /// </summary>
        public void AnimationChangeCaseRetourCaseDep()
        {
            if (retourDep)
            {
                if (position.X != casedepX)
                    position.X--;
                if (position.Y != casedepY)
                    position.Y++;

                // Lorsqu'on y est, on réinitialise les variables du Pion, pour qu'elles correspondent à celle de la case de départ
                if (position.X == casedepX && position.Y == casedepY)
                {
                    retourDep = false;
                    line = 3;
                    column = 1;
                    nextCase = Game1.plate.Tab[1];
                    positionCase = Game1.plate.Tab[0];
                }
            }
        }

        #endregion

        /// <summary>
        /// Enclenche le changement de case, selon s'il faut avancer ou reculer
        /// </summary>
        /// <param name="newcase">case de destination</param>
        public void ChangeCase(int newcase)
        {
            if ((positionCase != null && newcase == positionCase.Numero) || (newcase == 0 && positionCase == null))
            {            }
            else if (newcase == 0)
            {
                ChangeCaseRetourCaseDep();
            }
            else if (positionCase == null || positionCase.Numero < newcase)
            {
                ChangeCaseAvance(newcase);
            }
            else if (positionCase.Numero > newcase)
            {
                ChangeCaseRecule(newcase);
            }

            isMoving = isDeplacingAvance || isDeplacingRecule || retourDep;
        }

        #region ANIMATION
        /* Méthode et variable qui permmettent l'animation du Pion, lors de son déplacement */

        int timer;

        /// <summary>
        /// Lance l'animation du Pion
        /// </summary>
        public void BougeLesPieds()
        {
            timer++;
            if (timer % 5 == 0)
                column = (column + 1) % 3;
        }
        #endregion
        #endregion

        #region UPDATE & DRAW
        /// <summary>
        /// Update du Pion, selon s'il change de position ou pas
        /// </summary>
        public void Update()
        {
            AnimationChangeCaseAvance();
            AnimationChangeCaseRetourCaseDep();
            AnimationChangeCaseRecule();

            isDeplacing = isDeplacingAvance || isDeplacingRecule || retourDep;
            isMoving = isDeplacing;
        }

        /// <summary>
        /// Dessine le Pion sur la case ou l'endroit où il doit être
        /// </summary>
        public void Draw()
        {
            GameData.SpriteBatch.Draw(pionPerso, new Rectangle(position.X - widthpion / 2, position.Y - heightpion / 2, position.Width, position.Height),
                new Rectangle(widthpion * column, heightpion * line, widthpion, heightpion), Color.White);
        }
        #endregion
    }
}
