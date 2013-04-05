using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    class Interface
    {
        #region FIELDS
        

        #endregion

        #region CONSTRUCTORS
        public Interface()
        {
           
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

    /* Classe qui code pour l'interface, avec laquelle le joueur interagit.
     * Elle se situera à droite de l'écran de jeu et sera divisée en deux grandes parties.
     * Elle contient les informations sur la partie : 
     *      PARTIE GENERALE (en haut)
     *      - nombre de joueurs
     *      - temps de jeu
     *      - le classement des joueurs par rapport à l'arrivée 
     *      (si on passe sa souris sur un joueur, on accède à certaines de ses infos :  - nom, pion
     *                                                                                  - position dans le classement, case
     *                                                                                  - si il a un malus)
     *      PARTIE STATUT du current joueur (en bas)
     *      - nom, pion
     *      - position dans le classement, case
     *      - si il a un malus
     *      - lancer de dés (bouton)
     *      - fin de tour (bouton)
     *      - abandonner la partie (bouton) (?)
     * 
     * */

    /* Doit-elle etre mise à joueur à chaque tour de joueur ou utilise-t-elle directemement les données des joueurs ?
     * */
}
