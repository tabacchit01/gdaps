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
        Texture2D faceGui;
        Texture2D healthBlock;
        Rectangle box1;
        Rectangle box2;
        Rectangle box3;
        Rectangle box4;
        int tracker = 0;
        int lttNum = 0;
        int enterNum = 0;
        string ttl = "";
        string bot = "";
        string bar = "|";
        bool on = false;

        public GameMenus(Texture2D startb, Texture2D ybutton, Texture2D nbutton, Texture2D WMtext, Texture2D conPin, Texture2D conM, Texture2D conI, Texture2D conEB, Texture2D bButton, Texture2D conButton, Texture2D abortButt, Texture2D IB, Texture2D OB, Texture2D stl, Texture2D guiFc, Texture2D hltBlc)
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
            faceGui = guiFc;
            healthBlock = hltBlc;
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
        public void TitleMenu(SpriteBatch obj, SpriteFont inner, SpriteFont outer, int frame, bool end, char letter)
        {
            StringBuilder title = new StringBuilder();           
            int x = 30;

            Rectangle StartButtonRec = new Rectangle(30, 310, 300, 50);
            box1 = StartButtonRec;

            if (end)
            {
                if (frame == 1)
                {
                    if (on)
                    {
                        bar = "|";
                        enterNum += 1;
                        on = false;
                    }
                    else
                    {
                        bar = "";
                        on = true;
                    }                  

                }
                if (enterNum >= 1)
                {
                    obj.DrawString(outer, "Press Enter To Begin . . .", new Vector2(30, 380), Color.White); //210
                }
                if (enterNum >= 3)
                {
                    obj.Draw(startButton, StartButtonRec, Color.White);
                    obj.DrawString(outer, "BEGIN", new Vector2(box1.X + (box1.Width / 3), box1.Y + (box1.Height / 4)), Color.Black);
                    obj.DrawString(inner, "Or press this button . . .", new Vector2(30, 420), Color.White);
                    switch (enterNum)
                    {
                        case 4:
                            {
                                bot = "Wait wut?";
                                break;
                            }
                        case 6:
                            {
                                bot = "why do you have a button\n  if you can press ENTER to continue...";
                                break;
                            }
                        case 9:
                            {
                                bot = "Who the fuck made this game anyways";
                                break;
                            }
                        case 11:
                            {
                                bot = "it's almost like two guys made this\n  game alone...";
                                break;
                            }
                        case 14:
                            {
                                bot = "Anyways just go on and play the game";
                                break;
                            }
                        case 18:
                            {
                                bot = "...";
                                break;
                            }
                        case 23:
                            {
                                bot = "WHY ARE YOU STILL HERE????";
                                break;
                            }
                        case 24:
                            {
                                bot = "LEAVE!!!!";
                                break;
                            }

                    }
                    obj.DrawString(inner, "( " + bot + " )", new Vector2(320, 420), Color.White);
                }
                
            }
            else
            {
                if (frame == 1)
                {
                    title.Append(letter);
                    lttNum += 1;
                    ttl = ttl + title.ToString();

                }
            }
            


            obj.DrawString(outer, ttl + bar, new Vector2(30, 60), Color.White);
            /*
            obj.DrawString(outer, "Stealing\nswop", new Vector2(30, 60), Color.White);
            obj.DrawString(outer, "The", new Vector2(30, 100), Color.White);
            obj.DrawString(outer, "Mona", new Vector2(30, 140), Color.White);
            obj.DrawString(outer, "Lisa", new Vector2(30, 180), Color.White);*/
            



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
            obj.DrawString(priceText, "BUY", new Vector2((box1.X + (box1.Width / 3)) + 5, box1.Y + (box1.Height / 4)), Color.Black);


            box2 = ExitButtonRec;
            obj.Draw(contractExitButton, ExitButtonRec, Color.White);
        }
        public void Game(SpriteBatch obj, int health, int stam, Vector2 move)
        {
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle faceGuiRec = new Rectangle(5 + (int)move.X, 5, 270, 122);
            Rectangle healthBarRec1 = new Rectangle(117 + (int)move.X, 33, 50, 37);
            Rectangle healthBarRec2 = new Rectangle(165 + (int)move.X, 33, 50, 37);
            Rectangle healthBarRec3 = new Rectangle(213 + (int)move.X, 33, 40, 37);
            Rectangle stamBarRec = new Rectangle(117 + (int)move.X, 72, 120, 20);

            switch (health)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        obj.Draw(healthBlock, healthBarRec1, Color.Lime);
                        break;
                    }
                case 2:
                    {
                        obj.Draw(healthBlock, healthBarRec1, Color.Lime);
                        obj.Draw(healthBlock, healthBarRec2, Color.Lime);
                        break;
                    }
                case 3:
                    {
                        obj.Draw(healthBlock, healthBarRec1, Color.Lime);
                        obj.Draw(healthBlock, healthBarRec2, Color.Lime);
                        obj.Draw(healthBlock, healthBarRec3, Color.Lime);
                        break;
                    }
            }
            
            obj.Draw(healthBlock, stamBarRec, Color.Blue);
            obj.Draw(faceGui, faceGuiRec, Color.White);

        }
        public void GamePauseMenu(SpriteBatch obj, SpriteFont outer, Vector2 move)
        {
            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle ContinueRec = new Rectangle(50 + (int)move.X, 40, 200, 50);
            Rectangle AbortButtonRec = new Rectangle(50 + (int)move.X, 120, 300, 50);

            box1 = ContinueRec;
            obj.Draw(continueButton, ContinueRec, Color.White);
            obj.DrawString(outer, "COUNTINUE", new Vector2((box1.X + (box1.Width / 8) - 2) , box1.Y + (box1.Height / 4)), Color.Black);


            box2 = AbortButtonRec;
            obj.Draw(abortButton, AbortButtonRec, Color.White);
            obj.DrawString(outer, "ABORT MISSION", new Vector2((box2.X + (box2.Width / 10)) + 10, box2.Y + (box2.Height / 4)), Color.Black);

        }
        public void ConfirmMenu(SpriteBatch obj, SpriteFont outer, Vector2 move)
        {
            tracker = 0;

            box1 = new Rectangle(0, 0, 0, 0);
            box2 = new Rectangle(0, 0, 0, 0);

            Rectangle YesButtonRec = new Rectangle(170 + (int)move.X, 350, 200, 50);
            Rectangle NoButtonRec = new Rectangle(420 + (int)move.X  , 350, 200, 50);

            box1 = YesButtonRec;
            obj.Draw(yesButton, YesButtonRec, Color.White);
            obj.DrawString(outer, "YES", new Vector2((box1.X + (box1.Width / 3)) + 5, box1.Y + (box1.Height / 4)), Color.Black);

            box2 = NoButtonRec;
            obj.Draw(noButton, NoButtonRec, Color.White);
            obj.DrawString(outer, "NO", new Vector2((box2.X + (box2.Width / 3)) + 15, box2.Y + (box2.Height / 4)), Color.Black);
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
