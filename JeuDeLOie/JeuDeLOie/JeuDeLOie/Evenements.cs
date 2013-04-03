using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuDeLOie
{
    public enum Event
    {
        CaseDep, // case 0
        Nothing,
        Oie, // toutes les 9 cases, avance du meme nombre de case
        Pont, // case 6, avance jusqu'au 12
        Hotel, // case 19, passe 2 tour
        Puits, // case 31, reste bloqué 2 tours sauf si un joueur arrive sur cette case avant : ce dernier prend sa place et celui qui était tombé retourne à la case d'où le nouveau vient
        Labyrinthe, // case 41, retourne à la case 30
        Prison, // case 52, reste bloque jusqu'a ce qu'un joueur arrive sur cette case et prenne sa place
        Mort, // case 58, retourne à la case de départ
        CaseArr, // case 63, pour gagner il faut arriver pile poil sur cette case
    }

    class Evenements
    {
    }
}
