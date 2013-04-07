using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    public class Joueur : IComparable
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
            firstLaunchOfTurn = true;
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
                    _case = 29;
                    break;
                case Event.Mort:
                    _case = 0;
                    break;
                case Event.Oie:
                    if (_case + lastDiceLaunch >= 63)
                    {
                        nbCaseToGoBack = _case + lastDiceLaunch - 62;
                        _case = 62;
                    }
                    else
                    {
                        if (Game1.plate.Tab[_case + lastDiceLaunch].Evenement == Event.Nothing)
                        {
                            for (int i = 0; i < Game1.joueurs.Length; i++)
                            {
                                if (Game1.joueurs[i].Case == _case + lastDiceLaunch)
                                {
                                    return;
                                }
                            }
                        }
                        _case += lastDiceLaunch;
                    } 
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
                    Game1.CurrentGameState = Game1.GameState.Victory;
                    break;
            }
            pion.ChangeCase(_case);
        }
        #endregion

        #region UPDATE & DRAW
        bool notDisplacedYet;
        bool firstLaunchOfTurn;
        bool eventApplyed;
        int nbCaseToGoBack;

        public bool Update(int tourActuel)
        {
            if (tour == tourActuel % Game1.nbjoueurs)
            {
                if (firstLaunchOfTurn)
                {
                    notDisplacedYet = true;
                    firstLaunchOfTurn = false;
                    eventApplyed = false;
                    nbCaseToGoBack = 0;
                }

                if (cooldown > 0)
                {
                    cooldown--;
                    firstLaunchOfTurn = true;
                    Interface.textebouton = "Lancer les dés";
                    Interface.dices.ReInit();
                    return true;
                }
                else
                {
                    if (notDisplacedYet && !Interface.dices.IsRolling && !Interface.dices.IsInit)
                    {
                        lastDiceLaunch = Interface.dices.DicesResult;
                        if (_case + lastDiceLaunch >= 63)
                        {
                            nbCaseToGoBack = _case + lastDiceLaunch - 62;
                            _case = 62;
                        }
                        else
                        {
                            if (Game1.plate.Tab[_case + lastDiceLaunch].Evenement == Event.Nothing)
                            {
                                for (int i = 0; i < Game1.joueurs.Length; i++)
                                {
                                    if (Game1.joueurs[i].Case == _case + lastDiceLaunch)
                                    {
                                        // Ne pas se déplacer.
                                        notDisplacedYet = false;
                                        eventApplyed = true;
                                    }
                                }
                            }
                            _case += lastDiceLaunch;
                        }
                        pion.ChangeCase(_case);
                        notDisplacedYet = false;
                    }
                    
                    if (!pion.IsMoving && !notDisplacedYet && nbCaseToGoBack > 0)
                    {
                        _case -= nbCaseToGoBack;
                        nbCaseToGoBack = 0;
                        pion.ChangeCase(_case);
                    }
                    
                    if (!pion.IsMoving && !notDisplacedYet && !eventApplyed)
                    {
                        Evenements evenement = new Evenements(Game1.plate.Tab[_case].Evenement);
                        ApplyEvent(evenement);
                        eventApplyed = true;
                    }

                    if (!pion.IsMoving && eventApplyed)
                    {
                        Evenements evenement = new Evenements(Game1.plate.Tab[_case].Evenement);
                        if (evenement.E != Event.Nothing || evenement.E != Event.CaseDep)
                        {
                            eventApplyed = false;
                            pion.Update();
                            return false;
                        }
                        firstLaunchOfTurn = true;
                        Interface.textebouton = "Lancer les dés";
                        Interface.dices.ReInit();
                        // Ajouter dans l'interface quelque chose qui dise qu'il change de case
                        return true;
                    }
                }
            }

            pion.Update();
            return false;
        }

        public void Draw()
        {
            pion.Draw();
        }
        #endregion

        int IComparable.CompareTo(object obj)
        {
            return _case.CompareTo(((Joueur)obj)._case);
        }
    }
}
