using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JeuDeLOie
{
    /// <summary>
    /// Structure qui représente un dé, par une image et le nombre associé à la face visible
    /// </summary>
    public struct Dice
    {
        int i, jet, Timer;
        int result;
        public int Result { get { return result; } }
        public bool isRolling { get; set; }
        Rectangle ousurlatextureRef;

        #region METHODS
        /// <summary>
        /// Initialise le dé à la face 1
        /// </summary>
        public void InitDice()
        {
            result = 1;
            ousurlatextureRef = new Rectangle(75, 0, 75, 75);
        }

        /// <summary>
        /// Réinitialise le dé
        /// </summary>
        public void ReInit()
        {
            Interface.textebouton = "     Lancer les dés";
            result = 1;
            jet = 0;
            i = 0;
        }

        /// <summary>
        /// Lance le dé
        /// </summary>
        public void RollDice()
        {
            isRolling = true;
            jet = GameData.Random.Next(20, 42); // représente la "durée" du jet
            Timer = 1;
        }
        #endregion

        #region Update & Draw
        // On lance les dés, ils roulent sur la face rnd.Next(1,7) pendant i = 0 à j = rnd.Next(20,42) en changeant l'image selon la face.
        // Lorsque i == rnd.Next, face arrêtée sur laquelle le jet est stoppée devient result

        /// <summary>
        /// Change la face sur laquelle le dé est posé lorsque ce dernier est en train de rouler
        /// </summary>
        public void Update()// sera appelé seulement si le dé est en train de rouler
        {
            if (isRolling)
            {
                Timer++;
                if (jet > i && (Timer % 5 == 0)) // permet que i ne change pas à tous les update, laissant plus de suspense..
                {
                    result = GameData.Random.Next(1, 7); // donne une face au hasard
                    i++;
                }
                else if (jet == i)
                    isRolling = false;
            }
        }

        /// <summary>
        /// Dessine le dé à la position donnée
        /// </summary>
        /// <param name="position">Position où doit être dessiné le dé</param>
        public void Draw(Rectangle position)
        {
            GameData.SpriteBatch.Draw(ContentLoad.DiceTexture,
                position, new Rectangle(ousurlatextureRef.X * (result - 1), ousurlatextureRef.Y, 75, 75),
                Color.White);
        }
        #endregion
    }

    /// <summary>
    /// Classe qui regroupe deux dés (Dice)
    /// </summary>
    class Dices
    {
        #region FIELDS
        static List<Dice> dices;
        Dice d1, d2;
        int Result; // résultat, qui est la somme des deux dés
        public int DicesResult { get { return Result; } }
        bool isRolling, isInit; // permettent de connaître l'état des dés
        public bool IsRolling { get { return isRolling; } }
        public bool IsInit { get { return isInit; } }
        Rectangle position, position2; // position est la position du premier dé, position2 est celle du deuxième dé
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Construit deux dés et les initialises comme il faut
        /// </summary>
        /// <param name="position"></param>
        public Dices(Rectangle position)
        {
            this.position = position;
            position2 = position;
            position2.X += 75 + 5;
            dices = new List<Dice>() { new Dice(), new Dice() };
            d1 = new Dice();
            d1.InitDice();
            d2 = new Dice();
            d2.InitDice();
            isInit = true;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Rénitialise les deux dés contenu dans Dices en même temps
        /// </summary>
        public void ReInit()
        {
            if (!isRolling)
            {
                d1.ReInit(); d2.ReInit();
                isInit = true;
            }
        }

        /// <summary>
        /// Lance les deux dés de Dices
        /// </summary>
        public void RollDices()
        {
            if (isInit && !isRolling)
            {
                isRolling = true;
                d1.RollDice();
                d2.RollDice();
                isInit = false;
            }
        }

        /// <summary>
        /// Renvoit le résultat de l'addition des deux dés
        /// </summary>
        void NewResult()
        { Result = d1.Result + d2.Result; }
        #endregion

        #region UPDATE & DRAW
        /// <summary>
        /// Update des deux dés
        /// </summary>
        public void Update()
        {
            foreach (var dice in dices)
                dice.Update();

            if (isRolling)
            {
                d1.Update(); d2.Update();
                isRolling = d1.isRolling || d2.isRolling;
                if (!isRolling)
                { NewResult(); }
            }
        }

        /// <summary>
        /// Dessine les deux dés de Dices l'un à côté de l'autre (horizontalement), avec la face qu'ils montrent
        /// </summary>
        public void Draw()
        {
            d1.Draw(position);
            d2.Draw(position2);
        }
        #endregion
    }
}
