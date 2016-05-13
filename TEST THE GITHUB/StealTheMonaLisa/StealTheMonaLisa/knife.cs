using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class knife : Item
    {
        private string itemDescrpition;
        Random rgen;

        public knife(Texture2D itIG) : base("A Knife", 20, 100, 3.0, 1, itIG)
        {
            rgen = new Random();
        }
        public knife(string nm, int mny, int dur, double wght, int infm, Texture2D itIg) : base(nm, mny, dur, wght, infm, itIg)
        {

        }


        public override string ItemDescription
        {
            get { return itemDescrpition; }
        }

        /// <summary>
        /// Randomizes the desription for the items
        /// </summary>
        /// <param name="num">the number to decide which description to use</param>
        /// <returns>returns a description for the item</returns
        public override string RandomDesc(int num)
        {
            switch (num)
            {
                case 1:
                    {
                        string newDescr = "That bitch named Kathy next door stole\nmy kife. Can you go steal it back for\nme. If you kill her in the process\ni dont mind either";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 2:
                    {

                        string newDescr = "I want to become a chef but\n I don't\nhave the tools to do so. Can you steal a\nknife for me?";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 3:
                    {
                        string newDescr = "wrtwergwer";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 4:
                    {
                        string newDescr = "wretwert";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                default:
                    {
                        string newDescr = "wrtwrtwe";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }

            }
        }
    }
}
