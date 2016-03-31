using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class tileClass
    {

        private Rectangle hitBox; //temport rectangle used to compare collsion
        public Rectangle HitBox
        {

            get { return hitBox; }

        }

        private int tileX;

        public int TileX
        {

            get { return tileX; }

            set { tileX = value; }

        }

        private int tileY;

        public int TileY
        {

            get { return tileY; }

            set { tileY = value; }

        }

        //int tileWidth;

        //int tileHeight;

        private Texture2D tileDD;

        public Texture2D TileDD
        {

            get { return tileDD; }

            set { tileDD = value; }

        }

        public tileClass(int x, int y, int wdth, int hght, Texture2D tile)
        {

            tileX = x;

            tileY = y;

            //tileWidth = wdth;

            //tileHeight = hght;

            tileDD = tile;

            Rectangle tileStore = new Rectangle(x, y, wdth, hght);

            hitBox = tileStore;

        }
        public virtual void GameObjectDraw(SpriteBatch sprtBt) //the draw method used to draw the objects to the screen
        {

            Rectangle rekt = hitBox;

            rekt.X = tileX;

            rekt.Y = tileY;

            sprtBt.Draw(tileDD, rekt, Color.White);

        }

    }
}
