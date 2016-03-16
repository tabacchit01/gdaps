using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    public class Character
    {
        int health;
        int Strength;
        int speed;
        public Rectangle box;
        Texture2D currentTexture;

        public Character(int x, int y, int wdth, int hght, int hlth, int strngth, int spd)
        {
            box = new Rectangle(x, y, wdth, hght);
            health = hlth;
            Strength = strngth;
            speed = spd;
        }

        public int X
        {
            get { return box.X; }
            set { box = new Rectangle(value, box.Y, box.Width, box.Height); }
        }
        public int Y
        {
            get { return box.Y; }
            set { box = new Rectangle(box.X, value, box.Width, box.Height); }
        }
        public Texture2D CurrentTexture
        {
            get { return currentTexture; }
            set { currentTexture = value; }
        }
    }
}
