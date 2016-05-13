using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public int ScreenWidth
        {
            get { return GraphicsDeviceManager.DefaultBackBufferWidth; }
        }

        public int ScreenHeight
        {
            get { return GraphicsDeviceManager.DefaultBackBufferHeight; }
        }

        public Vector2 Update(int x, int y)
        {
            position.X = x - (ScreenWidth / 2);
            //position.Y = y - (ScreenHeight / 2);

            if(position.X < 0)
            {
                position.X = 0;
            }
            /*if(position.Y < 0)
            {
                position.Y = 0;
            }*/

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
            return position;
        }
    }
}
