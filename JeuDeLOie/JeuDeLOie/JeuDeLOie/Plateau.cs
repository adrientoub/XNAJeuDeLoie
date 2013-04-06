using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JeuDeLOie
{
    public enum Dirct
    {
        Droite,
        Bas,
        Gauche,
        Haut,
    }

    public class Plateau
    {
        #region FIELDS
        Case[] tab;
        public Case[] Tab { get { return tab; } }
        #endregion

        #region CONSTRUCTOR
        public Plateau()
        {
            tab = new Case[63];
            ConstructionPositionSpirale(63);
            ConstructionCaseGoose();
            ConstructionCaseEvent();
        }
        #endregion

        #region METHODS
        bool tourne;
        void ConstructionPositionSpirale(int i)
        {
            Dirct dir = Dirct.Droite;
            Vector2 pos = new Vector2(GameData.PreferredBackBufferWidth/4, GameData.PreferredBackBufferHeight / 2.5f);
            int j = 0;
            while(i >= 0)
            {
                j++;
                for (int k = 0; k < j; k++)
                {
                    tab[i - 1] = new Case(new Rectangle((int)pos.X,(int)pos.Y, GameData.CaseWidth, GameData.CaseHeight), Event.Nothing, i-1, tourne);
                    tourne = false;
                    switch ((int)dir)
                    {
                        case 0: pos.X += GameData.CaseWidth + 2;
                            break;
                        case 1: pos.Y += GameData.CaseHeight + 2;
                            break;
                        case 2: pos.X -= GameData.CaseWidth + 2;
                            break;
                        case 3: pos.Y -= GameData.CaseHeight + 2;
                            break;
                    }
                    i--;
                    if (i == 0)
                        return;
                }
                tourne = true;
                switch ((int)dir)
                {
                    case 0: dir = Dirct.Bas;
                        break;
                    case 1: dir = Dirct.Gauche;
                        break;
                    case 2: dir = Dirct.Haut;
                        break;
                    case 3: dir = Dirct.Droite;
                        break;
                } 
            }
        
        }
        void ConstructionCaseGoose()
        {
            for (int i = 1; i < 55; i++)
                if (i % 9 == 0)
                    tab[i] = new Case(tab[i].Position, Event.Oie, i, tab[i].isTurning);
        }
        void ConstructionCaseEvent()
        {
            tab[0] = new Case(tab[0].Position, Event.CaseDep, 0, tab[0].isTurning);
            tab[0].Change4CaseDep();
            tab[6] = new Case(tab[6].Position, Event.Pont, 6, tab[6].isTurning);
            tab[19] = new Case(tab[19].Position, Event.Hotel, 19, tab[19].isTurning);
            tab[31] = new Case(tab[31].Position, Event.Puits, 31, tab[31].isTurning);
            tab[41] = new Case(tab[41].Position, Event.Labyrinthe, 41, tab[41].isTurning);
            tab[52] = new Case(tab[52].Position, Event.Prison, 52, tab[52].isTurning);
            tab[58] = new Case(tab[58].Position, Event.Mort, 58, tab[58].isTurning);
            tab[62] = new Case(tab[62].Position, Event.CaseArr, 62, tab[62].isTurning);
        }
        #endregion

        #region UPDATE & DRAW
        public void Update()
        {
            foreach (Case Cases in tab)
            {
                Cases.Update();
            }
        }

        public void Draw()
        {
            foreach (Case Cases in tab)
            {
                Cases.Draw();
            }
        }
        #endregion
    }
}
