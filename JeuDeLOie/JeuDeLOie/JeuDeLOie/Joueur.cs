using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    public class Joueur
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
        public int CoolDown { get { return cooldown; } }
        int lastDiceLaunch;

        Pion pion;
        public Pion Pion { get { return pion; } }

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
            pion = new Pion(name, tour);
            _case = 0;
            cooldown = 0;
            lastDiceLaunch = 0;

            // on place son pion au bon endroit
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
                    for (int i = 0; i < Game1.joueurs.Length; i++)
                    {
                        if (i != tour && Game1.joueurs[i].Case == 51)
                        {
                            Game1.joueurs[i].cooldown = 0; // Ajouter quelque chose dans l'interface qui explique qu'il est libre :)
                            break;
                        }
                    }
                    cooldown = int.MaxValue; 
                    break;
                case Event.Puits:
                    for (int i = 0; i < Game1.joueurs.Length; i++)
                    {
                        if (i != tour && Game1.joueurs[i].Case == 31)
                        {
                            Game1.joueurs[i].cooldown = 0; // Ajouter quelque chose dans l'interface qui explique qu'il est libre :)
                            break;
                        }
                    }
                    cooldown = 2;
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

            pion.Update();
        }

        public void Draw()
        {
            pion.Draw();
        }
        #endregion
    }
}
