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
        int sprint;
        int stamina;
        int runspeed;
        int gravity;
        int flip;

        bool MoveLeft;
        bool MoveRight;
        bool onGround;
        bool isJumping;
        bool falling;

        Rectangle box;
        Rectangle prevBox;

        List<Rectangle> tileBox;
        List<Rectangle> contact;

        Texture2D currentTexture;

        #region Properties
        public int Flip
        {
            get { return flip; }
            set { flip = value; }
        }

        public int X
        {
            get { return box.X; }
            set { box = new Rectangle(value, box.Y, box.Width, box.Height);}
        }
        public int Y
        {
            get { return box.Y; }
            set { box = new Rectangle(box.X, value, box.Width, box.Height);}
        }

        public int Width
        {
            get { return box.Width; }
            set { box = new Rectangle(box.X, box.Y, value, box.Height);}
        }

        public int Height
        {
            get { return box.Height; }
            set { box = new Rectangle(box.X, box.Y, box.Width, value);}
        }
        public Texture2D CurrentTexture
        {
            get { return currentTexture; }
            set { currentTexture = value; }
        }

        public bool RunLeft
        {
            get { return MoveLeft; }
            set { MoveLeft = value; }
        }

        public bool RunRight
        {
            get { return MoveRight; }
            set { MoveRight = value; }
        }
        
        public int Runspeed
        {
            get { return runspeed; }
            set { runspeed = value; }
        }

        public int Sprint
        {
            get { return sprint; }
            set { sprint = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Stamina
        {
            get { return stamina; }
            set { stamina = value; }
        }

        public Rectangle PrevBox
        {
            get { return prevBox; }
            set { prevBox = value; }
        }

        public Rectangle Box
        {
            get { return box; }
            set { box = value; }
        }

        public bool OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }

        public bool IsJumping
        {
            get { return isJumping; }
            set { isJumping = value; }
        }
        #endregion

        public Character(int x, int y, int wdth, int hght, int hlth, int strngth, int spd)
        {
            box = new Rectangle(x, y, wdth, hght);
            health = hlth;
            Strength = strngth;
            speed = spd;

            sprint = 0;
            stamina = 200;
            runspeed = 0;
            gravity = 0;
            onGround = false;
            falling = false;
            flip = 0;

            tileBox = new List<Rectangle>();
            contact = new List<Rectangle>();
        }

        /// <summary>
        /// Used to move a character left.
        /// </summary>
        /// <param name="v">The max speed going left.</param>
        public void Left(int v)
        {
            MoveLeft = true;
            MoveRight = false;
            runspeed++;

            if (runspeed >= v)
            {
                runspeed = v;
            }

            this.X -= runspeed + sprint;
        }

        /// <summary>
        /// Used to move a character right.
        /// </summary>
        /// <param name="v">The max speed going right.</param>
        public void Right(int v)
        {
            MoveRight = true;
            MoveLeft = false;
            runspeed++;

            if(runspeed >= v)
            {
                runspeed = v;
            }

            this.X += runspeed + sprint;
        }

        /// <summary>
        /// Used to handle jumping (Set IsJumping to true before calling this method).
        /// </summary>
        public void Jump(int s)
        {
            if (isJumping == true)
            {
                onGround = false;
                box.Y -= s - (gravity / 2);
                gravity++;
            }
        }

        public void fall()
        {
            if(falling == true && isJumping == false)
            {
                box.Y += (gravity / 2);
                gravity++;
            }
        }

        public void CollideX(List<Rectangle> list)
        {
            foreach(Rectangle r in list)
            {
                if(Box.Intersects(r) && (r.Y > box.Y && r.Y < box.Y+Height))
                {
                    // Right
                    if(prevBox.X > r.X)
                    {
                        box.X = r.X + r.Width + 1;
                        runspeed = 0;
                    }

                    // Left
                    if(prevBox.X < r.X)
                    {
                        box.X = r.X - Width - 1;
                        runspeed = 0;
                    }
                }
                else if(!box.Intersects(r) && (r.X > box.X && r.X < box.X + Width) && (isJumping == false))
                {
                    falling = true;
                }
            }
        }

        public void CollideY(List<Rectangle> list)
        {
            foreach (Rectangle r in list)
            {
                // if the play has vertically collided
                if (box.Intersects(r) && (r.X > box.X && r.X < box.X + Width))
                {
                    // Up
                    if (prevBox.Y < r.Y)
                    {
                        box.Y = r.Y - Height - 1;
                        isJumping = false;
                        onGround = true;
                        falling = false;
                        gravity = 0;
                    }

                    // Down
                    if (prevBox.Y > r.Y)
                    {
                        box.Y = r.Y + Height + 1;
                        Jump(0);
                    }
                }
            }
        }

        public bool IsCollidingX(List<Rectangle> list)
        {
            foreach (Rectangle r in list)
            {
                if (Box.Intersects(r) && (r.Y > box.Y && r.Y < box.Y + Height))
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckFall(List<Rectangle> list)
        {
            foreach(Rectangle r in list)
            {
                if (!box.Intersects(r) && (r.X > box.X && r.X < box.X + Width) && (isJumping == false))
                {
                    falling = true;
                }
            }
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(currentTexture, box, Color.White);
        }
    }
}
