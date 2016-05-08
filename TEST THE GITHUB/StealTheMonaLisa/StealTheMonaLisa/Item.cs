using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class Item
    {
        Texture2D itemImage;
        private string name;
        private int money;
        private int durability;
        private double weight;
        private int infamy;


        public Item(string nm, int mny, int dur, double wght, int infm, Texture2D itIg)
        {
            name = nm;
            money = mny;
            durability = dur;
            weight = wght;
            infamy = infm;
            itemImage = itIg;
        }


        public string Name
        {
            get { return name; }
        }
        public int Money
        {
            get { return money; }
        }
        public int Durability
        {
            get { return durability; }
        }
        public double Weight
        {
            get { return weight; }
        }
        public int Infamy
        {
            get { return infamy; }
        }
        public Texture2D ItemImage
        {
            get { return itemImage; }
        }
        virtual public string ItemDescription
        {
            get { return ""; }
        }


        virtual public string RandomDesc(int num)
        {
            return "";
        }
    }
}
