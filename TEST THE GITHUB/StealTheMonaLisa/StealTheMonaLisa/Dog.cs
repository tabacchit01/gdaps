using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class Dog : Item
    {
        private string itemDescrpition;
        Random rgen;

        public Dog(Texture2D itIG) : base("The\nNeighboors Dog", 350, 100, 50.0, 1, itIG)
        {
            rgen = new Random();

        }
        public Dog(string nm, int mny, int dur, double wght, int infm, Texture2D itIg) : base(nm, mny, dur, wght, infm, itIg)
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
                        string newDescr = "So i hate my neighboor so can you please steal his stupid dog for me please, it never SHUTS UP!";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 2:
                    {

                        string newDescr = "So I really want a dog but I dont have\nenough money so could you please steal\none for me?";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 3:
                    {
                        string newDescr = "Swoop";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 4:
                    {
                        string newDescr = "Swag";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                default:
                    {
                        string newDescr = "Swippy";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }

            }
        }

    }
}
