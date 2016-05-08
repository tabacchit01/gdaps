using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class MonaLisa : Item
    {
        private string itemDescrpition;
        Random rgen;

        public MonaLisa(Texture2D itIG) : base("Mona Lisa", 10000000, 100, 12.0, 3, itIG)
        {
            rgen = new Random();
        }
        public MonaLisa(string nm, int mny, int dur, double wght, int infm, Texture2D itIg) : base(nm, mny, dur, wght, infm, itIg)
        {

        }

        public override string ItemDescription
        {
            get { return itemDescrpition; }
        }

        public override string RandomDesc(int num)
        {
            switch (num)
            {
                case 1:
                    {
                        string newDescr = "It's time to redeem yourself and get\nback what was stolen from you, your\nglory of stealing the Mona Lisa. It's\ngoing to cost a shit ton though...";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 2:
                    {

                        string newDescr = "So i know you onced tried and attempted\nstealing the mona lisa but ultimately\nfailed. But as i can see you are\nback in your prime, so i want to give\nyou another chance to attain your glory.\nSteal the Mona Lisa";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 3:
                    {
                        string newDescr = "OK SOLIDER, I WANT THAT MONA LISA IN MY\nPOSESSION TONIGHT. THAT IS AN ORDER!.\nIF YOU GET CAUGHT DO NOT MENTION\nME AT ALL. THAT IS ALSO AN ORDER!";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 4:
                    {
                        string newDescr = "My passion for art has grown so much ove\nthe years and i've collected so man art\npieces, but one thing that bothers\nme is that i do not have the Mona Lisa.\nI want you to steal it for me, the\npay out will be handsome";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                default:
                    {
                        string newDescr = "Alright sonny it's time for you to get\nback on your feet and make that attempt\nagain, we've worked so hard to get to\nthis point so i want you to give\nit your all, or you'll probably die...";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }

            }
        }
    }
}
