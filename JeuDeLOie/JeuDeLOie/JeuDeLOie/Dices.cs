using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    enum Face : int
    {
        
    }

    public struct Dice
    {
        int jet;
        int result;
        public int Result { get { return result; } }
        public bool isRolling { get; set; }

        #region METHODS
        public void ReInit()
        { result = 1; }

        public void RandomResult()
        {
            
        }
        #endregion

        #region Update & Draw
        // On lance les dés, ils roulent sur la face rnd.Next(1,7) pendant i = 0 à rnd.Next(10,42) en changeant l'image selon la face.
        // Lorsque i == rnd.Next, face arrêtée sur laquelle le jet est stoppée devient result

        public void RollDice()
        {

        }

        public void Draw()
        {
        }
        #endregion
    }

    class Dices
    {
        #region FIELDS
        List<Dice> dices;
        int Result;
        bool isRolling;
        #endregion

        #region CONSTRUCTORS
        public Dices()
        {
            this.dices = new List<Dice>() { new Dice(), new Dice() };
        }
        #endregion

        #region METHODS
        #endregion

        #region UPDATE & DRAW
        public void RollDices()
        {
            foreach (var dice in dices)
                dice.RandomResult();
            Result = dices[0].Result + dices[1].Result;
        }
        #endregion
    }
}
