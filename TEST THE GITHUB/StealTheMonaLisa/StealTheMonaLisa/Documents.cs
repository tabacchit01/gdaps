using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class Documents : Item
    {
        private string itemDescrpition;
        Random rgen;

        public Documents(Texture2D itIG) : base("Top Secret\nDocuments", 100000, 100, 1.1, 2, itIG)
        {
            rgen = new Random();

        }
        public Documents(string nm, int mny, int dur, double wght, int infm, Texture2D itIg) : base(nm, mny, dur, wght, infm, itIg)
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
                        string newDescr = "The government is hiding something and\ni know it. So can you go and steal\nsome documents for me please";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 2:
                    {

                        string newDescr = "This guy in the CIA has pictures of me\nthat i'd rather not have seen to the\npublic eye can you get them for me?";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 3:
                    {
                        string newDescr = "I was searching through the internet\nand discovered that there are some\ndocumentsin the pentagon that might\nprove bush did 9/11 and i want you to go\nit for me";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                case 4:
                    {
                        string newDescr = "My dad you to work for the CIA and then one\nday my dad went on a 'business trip' and\nnever came. I am calling bull shit\non that. I want you to go and get\nsome files on my dad please.";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }
                default:
                    {
                        string newDescr = "This is definantely not Bernie Sanders\nbut recently there has been really sketchy\nthings happening with voters votes the\nother day. Now most of my... i mean\nBernie's supporters are the ones who \ncouldn't vote. I want you to dig up\nsome files on what's going on.";
                        itemDescrpition = newDescr;

                        return newDescr;
                    }

            }
        }
    }
}
