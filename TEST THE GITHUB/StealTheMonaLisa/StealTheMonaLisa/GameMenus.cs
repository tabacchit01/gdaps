using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class GameMenus
    {
        Texture2D startButton;
        Texture2D yesButton;
        Texture2D noButton;
        Texture2D worldMapTexture;
        Texture2D contractPin;
        Texture2D contractMenu;
        Texture2D contractItem;
        Texture2D contractExitButton;
        Texture2D buyButton;
        Texture2D continueButton;
        Texture2D abortButton;
        Texture2D inButton;
        Texture2D outButton;
        Texture2D steel;
        Rectangle box1;
        Rectangle box2;
        Rectangle box3;
        Rectangle box4;
        int tracker = 0;

        public GameMenus(Texture2D startb, Texture2D ybutton, Texture2D nbutton, Texture2D WMtext, Texture2D conPin, Texture2D conM, Texture2D conI, Texture2D conEB, Texture2D bButton, Texture2D conButton, Texture2D abortButt, Texture2D IB, Texture2D OB, Texture2D stl)
        {
            startButton = startb;
            yesButton = ybutton;
            noButton = nbutton;
            worldMapTexture = WMtext;
            contractPin = conPin;
            contractMenu = conM;
            contractItem = conI;
            contractExitButton = conEB;
            buyButton = bButton;
            continueButton = conButton;
            abortButton = abortButt;
            inButton = IB;
            outButton = OB;
            steel = stl;
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);
            box3 = new Rectangle(0, 0, 0, 0);
            box4 = new Rectangle(0, 0, 0, 0);
        }

        public Rectangle Box1
        {
            get { return box1; }
            set { box1 = value; }
        }
        public Rectangle Box2
        {
            get { return box2; }
            set { box2 = value; }
        }
        public Rectangle Box3
        {
            get { return box3; }
            set { box3 = value; }
        }
        public Rectangle Box4
        {
            get { return box4; }
            set { box4 = value; }
        }
        public void TitleMenu(SpriteBatch obj, SpriteFont inner, SpriteFont outer)
        {
            Rectangle StartButtonRec = new Rectangle(30, 300, 300, 50);
            box1 = StartButtonRec;
            obj.DrawString(outer, "Stealing", new Vector2(30, 60), Color.White);
            obj.DrawString(outer, "The", new Vector2(30, 100), Color.White);
            obj.DrawString(outer, "Mona", new Vector2(30, 140), Color.White);
            obj.DrawString(outer, "Lisa", new Vector2(30, 180), Color.White);
            obj.Draw(startButton, StartButtonRec, Color.White);

        }
        public void WorldMap(SpriteBatch obj, Color highLight, bool openClose)
        {
            Rectangle WorldMapTextureRec = new Rectangle(0, 0, obj.GraphicsDevice.Viewport.Width, obj.GraphicsDevice.Viewport.Height);
            if (tracker == 0)
            {
                Rectangle inOutButtonRec = new Rectangle(0, (obj.GraphicsDevice.Viewport.Height / 2) - 28, 30, 56);
                Rectangle steelRec = new Rectangle(-150, 0, 150, obj.GraphicsDevice.Viewport.Height);

                box3 = inOutButtonRec;
                box4 = steelRec;

                tracker += 1;
            }

            //Rectangle ContractPinRec = new Rectangle(200, 110, 10, 10);



            obj.Draw(worldMapTexture, WorldMapTextureRec, Color.White);


            if (openClose == false)
            {
                obj.Draw(steel, box4, Color.White);
                obj.Draw(outButton, box3, highLight);

            }
            else if (openClose == true)
            {
                obj.Draw(steel, box4, Color.White);
                obj.Draw(inButton, box3, highLight);
            }

            //box1 = ContractPinRec;
            //obj.Draw(contractPin, ContractPinRec, Color.White);
        }
        public void Mission(SpriteBatch obj, Rectangle bx, int num, Color highLight)
        {
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);
            int missin = num;
            Rectangle ContractPinRec = bx;


            box2 = ContractPinRec;
            obj.Draw(contractPin, ContractPinRec, highLight);
        }
        public void ContractMenu(SpriteBatch obj, Mission mis, SpriteFont text, SpriteFont desText, SpriteFont priceText)
        {
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle ContractMenuTextureRec = new Rectangle(175, 15, 400, 422);
            Rectangle ContractItemRec = new Rectangle(ContractMenuTextureRec.X + (ContractMenuTextureRec.Width - 150), ContractMenuTextureRec.Y + 50, 100, 100);
            Rectangle BuyButtonRec = new Rectangle(ContractMenuTextureRec.X + (ContractMenuTextureRec.Width - 250), ContractMenuTextureRec.Y + (ContractMenuTextureRec.Height - 50), 200, 50);
            Rectangle ExitButtonRec = new Rectangle(ContractMenuTextureRec.X + ContractMenuTextureRec.Width, ContractMenuTextureRec.Y, 50, 50);

            obj.Draw(contractMenu, ContractMenuTextureRec, Color.White);

            obj.Draw(mis.MissionItem.ItemImage, ContractItemRec, Color.White);

            obj.DrawString(text, "Name: " + mis.MissionItem.Name, new Vector2(ContractItemRec.X - 180, ContractItemRec.Y), Color.Black);
            obj.DrawString(desText, "Durabiliy: " + mis.MissionItem.Durability + "%", new Vector2(ContractItemRec.X - 180, ContractItemRec.Y + 40), Color.Black);
            obj.DrawString(desText, "Weight: " + mis.MissionItem.Weight + "Lbs", new Vector2(ContractItemRec.X - 180, ContractItemRec.Y + 55), Color.Black);
            obj.DrawString(desText, "Market Price: $" + mis.MissionItem.Money, new Vector2(ContractItemRec.X - 180, ContractItemRec.Y + 70), Color.Black);
            obj.DrawString(desText, "Description:\n " + mis.MissionItem.ItemDescription, new Vector2(ContractItemRec.X - 180, ContractItemRec.Y + 110), Color.Black);

            obj.DrawString(priceText, "CONTRACT\nPRICE:  $" + mis.ContractCost, new Vector2(ContractItemRec.X - 180, ContractItemRec.Y + 262), Color.Black);

            box1 = BuyButtonRec;
            obj.Draw(buyButton, BuyButtonRec, Color.White);

            box2 = ExitButtonRec;
            obj.Draw(contractExitButton, ExitButtonRec, Color.White);
        }
        public void GamePauseMenu(SpriteBatch obj)
        {
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle ContinueRec = new Rectangle(50, 40, 200, 50);
            Rectangle AbortButtonRec = new Rectangle(50, 120, 300, 50);

            box1 = ContinueRec;
            obj.Draw(continueButton, ContinueRec, Color.White);

            box2 = AbortButtonRec;
            obj.Draw(abortButton, AbortButtonRec, Color.White);
        }
        public void ConfirmMenu(SpriteBatch obj)
        {
            tracker = 0;

            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle YesButtonRec = new Rectangle(170, 350, 200, 50);
            Rectangle NoButtonRec = new Rectangle(420, 350, 200, 50);

            box1 = YesButtonRec;
            obj.Draw(yesButton, YesButtonRec, Color.White);

            box2 = NoButtonRec;
            obj.Draw(noButton, NoButtonRec, Color.White);
        }
        public void MissionSuccess(SpriteBatch obj)
        {
            tracker = 0;
            box1 = new Rectangle(0, 0, 0, 0);

            Rectangle ContinueRec = new Rectangle(0, 0, 0, 0);
        }
        public void MissionFail(SpriteBatch obj)
        {
            tracker = 0;
            box1 = new Rectangle(0, 0, 0, 0);

            Rectangle ContinueRec = new Rectangle(0, 0, 0, 0);
        }
    }
}
