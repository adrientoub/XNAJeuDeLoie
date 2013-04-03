using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    class Case
    {
        #region FIELDS
        Event evenement;
        public Event Evenement { get { return evenement;} }
        #endregion

        #region CONSTRUCTOR
        public Case()
        {
            evenement = Event.Nothing;
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
