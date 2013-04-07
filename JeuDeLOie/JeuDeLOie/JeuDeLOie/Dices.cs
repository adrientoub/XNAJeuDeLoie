using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JeuDeLOie
{
    public struct Dice
    {
        int i, jet, Timer;
        int result;
        public int Result { get { return result; } }
        public bool isRolling { get; set; }
        Rectangle ousurlatextureRef;

        #region Gestion des rectangleDestination de chaque face possible

        #endregion

        #region METHODS
        public void InitDice()
        {
            result = 1;
            ousurlatextureRef = new Rectangle(75, 0, 75, 75);
        }

        public void ReInit()
        {
            Interface.textebouton = "     Lancer les dés";
            result = 1;
            jet = 0;
            i = 0;
        }

        public void RollDice()
        {
            isRolling = true;
            jet = GameData.Random.Next(20, 42);
            Timer = 1;
        }
        #endregion

        #region Update & Draw
        // On lance les dés, ils roulent sur la face rnd.Next(1,7) pendant i = 0 à j = rnd.Next(20,42) en changeant l'image selon la face.
        // Lorsque i == rnd.Next, face arrêtée sur laquelle le jet est stoppée devient result

        public void Update()// sera appelé seulement si le dé est en train de rouler
        {
            if (isRolling)
            {
                Timer++;
                if (jet > i && (Timer % 5 == 0)) // permet que i ne change pas à tous les update, laissant plus de suspense..
                {
                    result = GameData.Random.Next(1, 7);
                    i++;
                }
                else if (jet == i)
                    isRolling = false;
            }
        }


        public void Draw(Rectangle position)
        {
            GameData.SpriteBatch.Draw(ContentLoad.DiceTexture,
                position, new Rectangle(ousurlatextureRef.X * (result - 1), ousurlatextureRef.Y, 75, 75),
                Color.White);
        }
        #endregion
    }

    class Dices
    {
        #region FIELDS
        static List<Dice> dices;
        Dice d1, d2;
        int Result;
        public int DicesResult { get { return Result; } }
        bool isRolling, isInit;
        public bool IsRolling { get { return isRolling; } }
        public bool IsInit { get { return isInit; } }
        Rectangle position, position2; // position2 est celle du deuxième dé
        #endregion

        #region CONSTRUCTORS
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
        public void ReInit()
        {
            if (!isRolling)
            {
                d1.ReInit(); d2.ReInit();
                isInit = true;
            }
        }

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
        { Result = /*d1.Result + d2.Result*/ 41; }
        #endregion

        #region UPDATE & DRAW
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

        public void Draw()
        {
            d1.Draw(position);
            d2.Draw(position2);
        }
        #endregion
    }
}
