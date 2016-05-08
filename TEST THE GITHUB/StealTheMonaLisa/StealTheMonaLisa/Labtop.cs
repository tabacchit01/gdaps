using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class Labtop : Item
    {
        private string itemDescrpition;
        Random rgen;

        public Labtop(Texture2D itIG) : base("Mac Labtop", 2000, 100, 10.5, 2, itIG)
        {
            rgen = new Random();
        }
        public Labtop(string nm, int mny, int dur, double wght, int infm, Texture2D itIg) : base(nm, mny, dur, wght, infm, itIg)
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
                        string newDescr = "It's time to redeem yourself and\nget back what was stolen from you, your glory of stealing the Mona Lisa. It's going to cost a shit ton though...";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 2:
                    {

                        string newDescr = "What are you doing?";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 3:
                    {
                        string newDescr = "Are you rdy";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 4:
                    {
                        string newDescr = "Swoop";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                default:
                    {
                        string newDescr = "Swiggity";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }

            }
        }
    }
}
