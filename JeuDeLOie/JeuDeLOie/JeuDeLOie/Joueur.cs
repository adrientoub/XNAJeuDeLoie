using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    class Joueur
    {
        #region FIELDS
        int _case;
        public int Case
        {
            get { return _case; }
            set
            {
                if (value > 62 || value < 0)
                    return;
                else
                    _case = value;
            }
        }

        int tour; // ce joueur est le tourième joeur à jouer
        public int Tour { get { return tour; } }

        #region Trucs qui ne serviront surement pas
        bool versArr; // direction, si il va vers l'arrivée, ou l'autre sens
        string name;
        public string Name { get { return name; } }
        #endregion
        #endregion

        #region CONSTRUCTOR
        public Joueur(int tour, string name)
        {
            this.tour = tour;
            this.name = name;
            _case = 0;
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
