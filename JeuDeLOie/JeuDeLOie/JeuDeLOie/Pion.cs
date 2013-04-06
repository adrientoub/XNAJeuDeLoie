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
        public Rectangle Position { get { return position; } }
        Rectangle ouSurLaTexture;

        Texture2D pionPerso;
        Texture2D imagePerso;

        int line, column;

        #endregion

        #region CONSTRUCTOR
        public Pion(string namePerso)
        {
            // Initialise les bonnes images et les rectangles selon le perso choisi
            switch (namePerso)
            {
                case "Crocodile":
                    pionPerso = ContentLoad.personnages[1];
                    imagePerso = GameData.Content.Load<Texture2D>("namePerso");
              //      position = new Rectangle(0,0, 
                    break;
                case "Mai":
                    pionPerso = ContentLoad.personnages[2];
                    imagePerso = GameData.Content.Load<Texture2D>("namePerso");
                    break;
                case "Moogl":
                    pionPerso = ContentLoad.personnages[3];
                    imagePerso = GameData.Content.Load<Texture2D>("namePerso");
                    break;
                case "conan":
                    pionPerso = ContentLoad.personnages[0];
                    imagePerso = GameData.Content.Load<Texture2D>("namePerso");
                    break;
            }
            line = 0; column = 1;
        }
        #endregion

        #region METHODS
        #endregion

        #region UPDATE & DRAW
        #endregion
    }
}
