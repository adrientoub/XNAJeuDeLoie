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
                if (!(value > 62 || value < 0))
                    _case = value;
            }
        }

        int tour; // ce joueur est le tourième joueur à jouer
        public int Tour { get { return tour; } }

        int cooldown; // Le nombre de tours que le joueur doit attendre
        int lastDiceLaunch;

        #region Trucs qui ne serviront surement pas
        //bool versArr; // direction, si il va vers l'arrivée, ou l'autre sens
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
            cooldown = 0;
            lastDiceLaunch = 0;
        }
        #endregion

        #region METHODS
        private void ApplyEvent(Evenements e)
        {
            switch (e.E)
            {
                case Event.Hotel:
                    cooldown = 2;
                    break;
                case Event.Labyrinthe:
                    // TODO: Ajouter déplacement fluide du pion
                    _case = 29;
                    break;
                case Event.Mort:
                    _case = 0;
                    break;
                case Event.Oie:
                    _case += lastDiceLaunch; // Valeur du lancé de dés
                    break;
                case Event.Pont:
                    _case = 12;
                    break;
                case Event.Prison:
                    cooldown = int.MaxValue; // Ajouter la libération en cas de nouveau joueur sur la case
                    break;
                case Event.Puits:
                    cooldown = 2;// Ajouter la libération en cas de nouveau joueur sur la case
                    break;
                case Event.CaseArr:
                    // Activer la win
                    break;
            }
        }
        #endregion

        #region UPDATE & DRAW
        public void Update(int tourActuel)
        {
            if (tour == tourActuel)
            {
                if (cooldown >= 0)
                {
                    cooldown--;
                }
                else
                {
                    // Lance les dés.
                    // _case est modifié
                    // lastDiceLaunch est modifié
                    Evenements evenement = new Evenements(Game1.plate.Tab[_case].Evenement);
                    ApplyEvent(evenement);
                }
            }
        }

        public void Draw()
        {
        }
        #endregion
    }
}
