using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    class Plateau
    {
        #region FIELDS
        Case[] tab;
        public Case[] Tab { get { return tab; } }
        #endregion

        #region CONSTRUCTOR
        public Plateau()
        {
            tab = new Case[63];
            for (int i = 0; i < 63; i++)
            {
                tab[i] = new Case();
            }
            
        }
        #endregion

        #region METHODS
        #endregion

        #region UPDATE & DRAW
        public void Update()
        {
            
        }

        public void Draw()
        {
        }
        #endregion
    }
}
