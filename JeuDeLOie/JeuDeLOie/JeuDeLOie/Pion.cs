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
        Rectangle position;
        public Rectangle Position { get { return position; } set { position = value; } }
        int widthpion, heightpion;
        public int WidthPion { get { return widthpion; } }
        public int HeightPion { get { return heightpion; } }

        Texture2D pionPerso;
        public Texture2D PionPerso { get { return pionPerso; } }
        Texture2D imagePerso;
        public Texture2D ImagePerso { get { return imagePerso; } }
        Rectangle portrait;
        public Rectangle Portrait { get { return portrait; } }

        Dirct direction;
        Case positionCase;
        Case previousCase;
        Case nextCase;
        Case newCase;

        int line, column;
        public int Line { get { return line; } }
        public int Column { get { return column; } }
        int casedepX, casedepY;

        bool isDeplacing;
        public bool IsDeplacing { get { return isDeplacing; } }
        bool isMoving;
        public bool IsMoving { get { return isMoving; } }

        #endregion

        #region CONSTRUCTOR
        public Pion(string namePerso, int tour)
        {
            // Initialise les bonnes images et les rectangles selon le perso choisi
            imagePerso = GameData.Content.Load<Texture2D>(namePerso);
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
            positionCase = Game1.plate.Tab[0];
            nextCase = Game1.plate.Tab[1];
            previousCase = null;

            // on place le pion au bon endroit sur la case de départ
            casedepY = nextCase.Position.Y + GameData.CaseHeight / 2;
            casedepX = nextCase.Position.X - 2 - GameData.CaseWidth + GameData.CaseWidth / 2;
            position = new Rectangle(casedepX, casedepY, widthpion, heightpion);
            line = 3; column = 1;
            direction = Dirct.Droite; // on commence par aller à droite
        }
        #endregion

        #region METHODS
        #region AVANCE
        int newX, newY;
        bool isDeplacingAvance;
        public bool IsDeplacingAvance { get { return isDeplacingAvance; } }

        // On donne au pion sa prochaine destination
        public void ChangeCaseAvance(int newcase)
        {
            if (!isDeplacing)
            {
                isDeplacingAvance = true;
                newCase = Game1.plate.Tab[newcase];
                newX = newCase.Position.X + GameData.CaseWidth / 2;
                newY = newCase.Position.Y + GameData.CaseHeight / 2;
                timer = 0;
                column = 1;
            }
        }

        // tant que le pion n'est pas sur la nouvelle case, on continue d'avancer
        public void AnimationChangeCaseAvance()
        {
            if (isDeplacingAvance)
            {
                BougeLesPieds();
                // lorsqu'on est sur une nouvelle case
                if (nextCase != null && position.X == nextCase.Position.X + GameData.CaseWidth / 2 && position.Y == nextCase.Position.Y + GameData.CaseHeight / 2)
                {
                    previousCase = positionCase; // la case previous est la précedente
                    positionCase = nextCase;
                    if (nextCase.Numero != Game1.plate.Tab.Length - 1)
                    {
                        nextCase = Game1.plate.Tab[nextCase.Numero + 1];
                    }
                    else
                    {
                        isDeplacingAvance = false;
                        nextCase = null;
                    }
                    // si on est arrivé sur une case, la prochaine est la suivante, et l'actuelle est la précédente

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

                    // Lorsqu'on est arrivé, on s'arrête et on se "tourne" du bon sens
                    if (positionCase == newCase)
                    {
                        isDeplacingAvance = false;
                        column = 1;
                    }
                }


                // puis on avance selon la direction
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

            }

        }
        #endregion

        #region RECULE (pour les petites distances)
        bool isDeplacingRecule;
        public bool IsDeplacingRecule { get { return isDeplacingRecule; } }

        // On donne au pion sa prochaine destination
        public void ChangeCaseRecule(int newcase)
        {
            if (!isDeplacing)
            {
                isDeplacingRecule = true;
                newCase = Game1.plate.Tab[newcase];
                newX = newCase.Position.X + GameData.CaseWidth / 2;
                newY = newCase.Position.Y + GameData.CaseHeight / 2;
                timer = 0;
                column = 1;
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

        // tant que le pion n'est pas sur la nouvelle case, on continue d'Reculer
        public void AnimationChangeCaseRecule()
        {
            if (isDeplacingRecule)
            {
                BougeLesPieds();
                if (previousCase != null && position.X == previousCase.Position.X + GameData.CaseWidth / 2 && position.Y == previousCase.Position.Y + GameData.CaseHeight / 2)
                {
                    nextCase = positionCase;
                    positionCase = previousCase;

                    if (previousCase.Numero != 0)
                        previousCase = Game1.plate.Tab[previousCase.Numero - 1];
                    else
                    {
                        isDeplacingRecule = false;
                        previousCase = null;
                    }
                    // si on est arrivé sur une case, la prochaine est la suivante, et l'actuelle est la précédente

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


                // puis on Recule selon la direction
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

            }

        }
        #endregion

        #region RETOUR CASE DEP (quasi téléportation)
        bool retourDep;
        public bool RetourDep { get { return retourDep; } }
        public void ChangeCaseRetourCaseDep()
        {
            if (!isDeplacing)
            {
                retourDep = true;
                line = 0;
                column = 1;
            }
        }
        public void AnimationChangeCaseRetourCaseDep()
        {
            if (retourDep)
            {
                if (position.X != casedepX)
                    position.X--;
                if (position.Y != casedepY)
                    position.Y++;
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

        public void ChangeCase(int newcase)
        {
            if ((positionCase != null && newcase == positionCase.Numero) || (newcase == 0 && positionCase == null))
            {

            }
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

        // animation des pieds
        int timer;
        public void BougeLesPieds()
        {
            timer++;
            if (timer % 5 == 0)
                column = (column + 1) % 3;
        }
        #endregion

        #region UPDATE & DRAW
        public void Update()
        {
            AnimationChangeCaseAvance();
            AnimationChangeCaseRetourCaseDep();
            AnimationChangeCaseRecule();

            isDeplacing = isDeplacingAvance || isDeplacingRecule || retourDep;
            isMoving = isDeplacing;
        }

        public void Draw()
        {
            GameData.SpriteBatch.Draw(pionPerso, new Rectangle(position.X - widthpion / 2, position.Y - heightpion / 2, position.Width, position.Height),
                new Rectangle(widthpion * column, heightpion * line, widthpion, heightpion), Color.White);
        }
        #endregion
    }
}
