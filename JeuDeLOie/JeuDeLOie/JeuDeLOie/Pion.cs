using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JeuDeLOie
{
    class Pion
    {

        #region FIELDS
        Rectangle position;
        public Rectangle Position { get { return position; } set { position = value; } }
        int widthpion, heightpion;

        Texture2D pionPerso;
        Texture2D imagePerso;

        Dirct direction;
        Case positionCase;
        Case nextCase;
        Case newCase;
        int line, column;

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
                    break;
                case "Mai":
                    pionPerso = ContentLoad.personnages[2];
                    widthpion = 34; heightpion = 45;
                    break;
                case "Moogl":
                    pionPerso = ContentLoad.personnages[3];
                    widthpion = 34; heightpion = 45;
                    break;
                case "conan":
                    pionPerso = ContentLoad.personnages[0];
                    widthpion = 23; heightpion = 32;
                    break;
            }
            positionCase = Game1.plate.Tab[0];
            nextCase = Game1.plate.Tab[1];

            // on place le pion au bon endroit sur la case de départ
            position = new Rectangle(positionCase.Position.X + ((tour) % 2)*45 + (widthpion / 2)+ 3, positionCase.Position.Y + ((tour + 1) % 2)*45 + 3 + heightpion, widthpion, heightpion);
            line = 2; column = 1;
            direction = Dirct.Droite; // on commence par aller à droite
        }
        #endregion

        #region METHODS
        int newX, newY;
        bool isDeplacing;
        public bool IsDeplacing { get { return isDeplacing; } }

        // On donne au pion sa prochaine destination
        public void ChangeCase(int newcase)
        {
            isDeplacing = true;
            newCase = Game1.plate.Tab[newcase];
            newX = newCase.Position.X + GameData.CaseWidth / 2;
            newY = newCase.Position.Y + GameData.CaseHeight / 2;
            timer = 0;
            column = 1;
        }

        // tant que le pion n'est pas sur la nouvelle case, on continue d'avancer
        public void AnimationChangeCase()
        {
            if (isDeplacing)
            {
                BougeLesPieds();
                if (position.X == nextCase.Position.X + GameData.CaseWidth/2|| position.Y == nextCase.Position.Y+GameData.CaseHeight/2)
                {
                    positionCase = nextCase;
                    nextCase = Game1.plate.Tab[nextCase.Numero + 1];
                    // si on est arrivé sur une case, la prochaine est la suivante, et l'actuelle est la précédente
                    isDeplacing = false;
                }
                if (positionCase != newCase)
                {
                    // si c'est une case tournante, on change de direction
                    if (positionCase.isTurning)
                    {
                        switch ((int)direction)
                        {
                            case 0: direction = Dirct.Bas;
                                line = 0;
                                break;
                            case 1: direction = Dirct.Gauche;
                                line = 3;
                                break;
                            case 2: direction = Dirct.Haut;
                                line = 1;
                                break;
                            case 3: direction = Dirct.Droite;
                                line = 2;
                                break;
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

        }

        // animation des pieds
        int timer;
        public void BougeLesPieds()
        {
            timer++;
            if (timer % 20 == 0)
                column = (column + 1) % 3;
        }
        #endregion

        #region UPDATE & DRAW
        public void Update()
        {
            AnimationChangeCase();
        }

        public void Draw()
        {
            GameData.SpriteBatch.Draw(pionPerso, new Rectangle(position.X - widthpion/2, position.Y- heightpion, position.Width, position.Height), 
                new Rectangle(widthpion * column, heightpion * line, widthpion, heightpion), Color.White);
        }
        #endregion
    }
}
