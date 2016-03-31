using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class buildingTile : tileClass
    {

        Rectangle itemFrame; //the following rectangles are used to compatre the collision of rectnagles present in the code so that the game can adequatley handle collision

        Rectangle Obj; //comparison rectangle

        Rectangle coli; //rectangle3 creted in the collision

        private Texture2D imageA; //items default texture
        public Texture2D ImageA
        {

            get { return imageA; }

        }

        private bool colState; //bool used to check state of object
        public bool ColState
        {

            get { return colState; }

        }
        public buildingTile(bool Colis, int x, int y, int wdth, int hght, Texture2D tile) : base(x, y, wdth, hght, tile) //default constructor
        {

            colState = Colis;

            imageA = tile;

            itemFrame = new Rectangle(x, y, wdth, hght);

        }

        public bool CheckCollision(tileClass obj) //checks collision
        {

            int PosXAy = obj.TileX;

            int PosYAy = obj.TileY;

            Obj = new Rectangle(PosXAy, PosYAy, 50, 50);

            coli = Rectangle.Intersect(itemFrame, Obj);

            if (!coli.IsEmpty)
            {

                colState = true;

            }

            return colState;

        }

        public override void GameObjectDraw(SpriteBatch sprtBt) //the draw method for collectibles determining whether they are drawn or not dpepngin on collision
        {

            if (colState == false)
            {

                sprtBt.Draw(imageA, itemFrame, Color.White);

            }

        }

    }
}
