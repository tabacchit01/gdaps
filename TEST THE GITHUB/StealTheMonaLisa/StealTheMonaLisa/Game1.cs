﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace StealTheMonaLisa
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState kbstate;
        KeyboardState previouskbstate;
        MouseState mstate;
        MouseState previousmState;
        Texture2D testImage;
        Player1 p1;
        GameStats gstats;
        GameMenus GMenus;

        //Menu Textures
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
        SpriteFont text;
        SpriteFont desText;
        SpriteFont priceText;

        //Mission Items
        MonaLisa monaLisa;
        Dog dog;
        Documents doc;
        knife knyfe;

        // Mission Stuff
        Mission[] mis;
        Vector2 camPos;

        // Start Menu Stuff
        bool end; // Tells me when the line text has finished typing
        int framesM; // how im keeping track of frames (Michael)
        int letter; // The position of a character in a string
        string title; // The string i want to type (aka the title)

        // World Map Stuff
        int numOfMis;
        int misGen = 0;
        int misNum;
        int time = 0;
        int colorSwitch;
        bool openClose = false;
        Color highLight = Color.White;

        // Creating a array of items that holds lists 
        //of items based on their infamy lvl
        List<Item>[] items;
        List<Item> itemsLv1;
        List<Item> itemsLv2;
        List<Item> itemsLv3;

        StreamReader level;

        //Camera
        Camera camera = new Camera();

        //Enemy AI
        Character enemy1;
        Character enemy2;
        int spawnCount;

        private WalkState currentState;

        enum WalkState
        {
            FaceLeft,
            WalkLeft,
            FaceRight,
            WalkRight
        }


        Texture2D tileA;
        Texture2D tileB;
        Texture2D tileC;
        Texture2D spriteSheet;
        Random rng = new Random();
        Vector2 playerLOC;

        int frame;              // The current animation frame
        double timeCounter;     // The amount of time that has passed
        double fps;             // The speed of the animation
        double timePerFrame;

        const int walkFrameCount = 12;
        const int rectOffset = 0;
        const int rectHeight = 125;
        const int rectWidth = 125;

        Rectangle rect;
        List<tileClass> rects; //a list of collectibles
        List<tileClass> tempRects;
        List<Rectangle> tileRect; //list of all tile rectangles

        // Enums
        GameState CurrentState = GameState.StartMenu;
        CharacterState CharState = CharacterState.FaceRight;

        // basic physics values (Remove tempGround when we have solid ground blocks)
        bool isJumping = false;
        int Sprint = 0;

        // Handles the various states the game will be in
        enum GameState
        {
            StartMenu,
            ExitTutorialMenu,
            WorldMapMenu,
            ExitWorldMapMenu,
            MissionMenu,
            Game,
            PauseMenu,
            ExitGameConfirmMenu,
            MissionSuccessMenu,
            MissionFailure
        }

        // Handles each state for the character
        enum CharacterState
        {
            FaceLeft,
            FaceRight,
            MoveLeft,
            MoveRight,
            Jump,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            rects = new List<tileClass>();

            playerLOC = new Vector2();

            spawnCount = 1;

            fps = 10.0;
            timePerFrame = 1.0 / fps;

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Testing Player Load
            p1 = new Player1(GraphicsDevice.Viewport.Width/2-125, GraphicsDevice.Viewport.Height - 175, 125, 125, 3, 2, 1);
            gstats = new GameStats(100000, 5, 30, 90.0, 50.0);
            spriteSheet = Content.Load<Texture2D>("spriteSheetB.png");
            testImage = Content.Load<Texture2D>("Pizza.png");
            p1.CurrentTexture = testImage;

            // Loading in all the menu textures
            #region Menu Textures

            startButton = Content.Load<Texture2D>("basicButton.png");
            yesButton = Content.Load<Texture2D>("basicButton.png");
            noButton = Content.Load<Texture2D>("basicButton.png");
            worldMapTexture = Content.Load<Texture2D>("WorldMap.jpg");
            contractPin = Content.Load<Texture2D>("RedPin.png");
            contractMenu = Content.Load<Texture2D>("MenuBackground.png");
            contractItem = Content.Load<Texture2D>("Item1.jpg");
            contractExitButton = Content.Load<Texture2D>("ExitButton.png");
            buyButton = Content.Load<Texture2D>("basicButton.png");
            continueButton = Content.Load<Texture2D>("basicButton.png");
            abortButton = Content.Load<Texture2D>("basicButton.png");
            inButton = Content.Load<Texture2D>("InButton.jpg");
            outButton = Content.Load<Texture2D>("OutButton.jpg");
            steel = Content.Load<Texture2D>("steel.jpg");
            text = Content.Load<SpriteFont>("SimplifiedArabicFixed_14");
            desText = Content.Load<SpriteFont>("SimplifiedArabicFixed_10");
            priceText = Content.Load<SpriteFont>("SimplifiedArabicFixed_22");
            faceGui = Content.Load<Texture2D>("guiFace.png");
            healthBlock = Content.Load<Texture2D>("health block.png");
            #endregion

            //Loading in all the items
            items = new List<Item>[3];
            itemsLv1 = new List<Item>();
            itemsLv2 = new List<Item>();
            itemsLv3 = new List<Item>();

            monaLisa = new MonaLisa(Content.Load<Texture2D>("Item1.jpg"));
            dog = new Dog(Content.Load<Texture2D>("Dog.jpg"));
            doc = new Documents(Content.Load<Texture2D>("documents.jpg"));
            knyfe = new knife(Content.Load<Texture2D>("Knife.jpg "));

            itemsLv1.Add(dog);
            itemsLv1.Add(knyfe);
            itemsLv2.Add(doc);
            itemsLv3.Add(monaLisa);

            items[0] = itemsLv1;
            items[1] = itemsLv2;
            items[2] = itemsLv3;

            mis = null;
            numOfMis = 39;

            // plugging textures into the Game Menu Class
            GMenus = new GameMenus(startButton, yesButton, noButton, worldMapTexture, contractPin, contractMenu, contractItem, contractExitButton, buyButton, continueButton, abortButton, inButton, outButton, steel, faceGui, healthBlock);


            camPos = new Vector2(0, 0); // Setting up to pulling the camera position from the Camera class

            // Start Menu Stuff
            framesM = 10;
            letter = 0;
            end = false;
            title = "Stealing\nThe\nMona\nLisa";


            tileA = Content.Load<Texture2D>("tileA.png");
            tileB = Content.Load<Texture2D>("tileB.png");
            tileC = Content.Load<Texture2D>("tileC.png");

            textTile();

            //enemy textures
            enemy1.CurrentTexture = testImage;
            enemy2.CurrentTexture = testImage;

            tileRect = new List<Rectangle>();
            tempRects = new List<tileClass>();

            foreach(tileClass t in rects)
            {
                rect = t.HitBox;
                tileRect.Add(rect);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateAnimation(gameTime);

            // Getting keyboard state
            kbstate = Keyboard.GetState();
            mstate = Mouse.GetState();

            // Should switch between Game States
            switch (CurrentState)
            {
                #region Start Menu

                case GameState.StartMenu:
                    {
                        bool cont;
                        Keys enter = Keys.Enter;

                        cont = SingleKeyPress(enter);

                        bool hoveringStart = MousePosistion(GMenus.Box1.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        if (hoveringStart)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.WorldMapMenu;
                            }
                        }
                        if (cont)
                        {
                            CurrentState = GameState.WorldMapMenu;
                        }
                        break;
                    }
                #endregion

                #region World Map Menu

                case GameState.WorldMapMenu:
                    {
                        //highLight = Color.White; 
                        bool exit = false;
                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);
                        bool hoveringStart;
                        for (int i = 0; i < mis.Length; i++)
                        {
                            hoveringStart = MousePosistion(mis[i].Rec.X, mis[i].Rec.Y, mis[i].Rec.Height, mis[i].Rec.Width);
                            //highLight = Color.White; 
                            if (hoveringStart == true)
                            {
                                //highLight = Color.Turquoise;

                                colorSwitch = i;

                                if (mstate.LeftButton == ButtonState.Pressed)
                                {
                                    misNum = i;
                                    CurrentState = GameState.MissionMenu;
                                }
                            }
                            else if (i == colorSwitch && hoveringStart == false)
                            {
                                colorSwitch = -1;
                            }


                        }
                        bool butHoveringStart = MousePosistion(GMenus.Box3.X, GMenus.Box3.Y, GMenus.Box3.Height, GMenus.Box3.Width);


                        if (butHoveringStart == true)
                        {
                            highLight = Color.Turquoise;
                            if (previousmState.LeftButton == ButtonState.Pressed)
                            {

                            }
                            else if (mstate.LeftButton == ButtonState.Pressed && openClose == false)
                            {
                                openClose = true;

                            }

                            else if (mstate.LeftButton == ButtonState.Pressed && openClose == true)
                            {
                                openClose = false;
                            }
                        }
                        //bool hoveringStart;

                        if (exit)
                        {
                            CurrentState = GameState.ExitWorldMapMenu;
                        }
                        previouskbstate = kbstate;

                        break;
                    }
                #endregion

                #region Exit World Map Menu

                case GameState.ExitWorldMapMenu:
                    {
                        bool exit = false;

                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);

                        bool hoveringYes = MousePosistion(GMenus.Box1.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        bool hoveringNo = MousePosistion(GMenus.Box2.X, GMenus.Box2.Y, GMenus.Box2.Height, GMenus.Box2.Width);
                        if (hoveringYes)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                Exit();
                            }
                        }
                        if (hoveringNo)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.WorldMapMenu;
                            }
                        }

                        if (exit)
                        {
                            CurrentState = GameState.ExitGameConfirmMenu;
                        }
                        previouskbstate = kbstate;

                        break;
                    }
                #endregion

                #region Mission Menu

                case GameState.MissionMenu:
                    {
                        bool exit = false;

                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);

                        bool hoveringStart = MousePosistion(GMenus.Box1.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        bool hoveringExit = MousePosistion(GMenus.Box2.X, GMenus.Box2.Y, GMenus.Box2.Height, GMenus.Box2.Width);
                        if (hoveringStart)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                misGen = 0;
                                CurrentState = GameState.Game;
                            }
                        }
                        if (hoveringExit)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.WorldMapMenu;
                            }
                        }

                        if (exit)
                        {
                            CurrentState = GameState.WorldMapMenu;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

                #region Game

                case GameState.Game:
                    { 
                        // Moves the player
                        MovePlayer();

                        #region WalkStates
                    switch (currentState)
                    {
                        case WalkState.FaceRight: //checks direction starting from right

                            if (kbstate.IsKeyDown(Keys.D))
                            {

                                currentState = WalkState.WalkRight; //walks right

                            }

                            else if (kbstate.IsKeyDown(Keys.A))
                            {

                                currentState = WalkState.WalkLeft; //walks left

                            }

                            break;

                        case WalkState.FaceLeft: //checks direction starting from left

                            if (kbstate.IsKeyDown(Keys.A))
                            {

                                currentState = WalkState.WalkLeft; //walks left

                            }

                            else if (kbstate.IsKeyDown(Keys.D))
                            {

                                currentState = WalkState.WalkRight; //walks right

                            }

                            break;

                        case WalkState.WalkRight:

                            if (kbstate.IsKeyDown(Keys.D)) //continues moving mario across screen to right
                            {

                                currentState = WalkState.WalkRight;

                            }

                            else
                            {

                                currentState = WalkState.FaceRight; //postions mario to face right

                            }

                            break;

                        case WalkState.WalkLeft:


                            if (kbstate.IsKeyDown(Keys.A)) //comntinues moving mario across screen to left
                            {

                                currentState = WalkState.WalkLeft;

                            }

                            else
                            {

                                currentState = WalkState.FaceLeft; //poistions mario to face left

                            }

                            break;



                    }
                        #endregion

                        // Hanldes enemy AI
                        EnemyAI(enemy1);
                        EnemyAI(enemy2);


                        // Handles gravity
                        Gravity(p1);
                        Gravity(enemy1);
                        Gravity(enemy2);

                        // Handles the camera
                        camPos = camera.Update(p1.X,p1.Y);
                           

                        bool exit = false;

                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);

                        if (exit)
                        {
                        CurrentState = GameState.PauseMenu;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

                #region Pause Menu

                case GameState.PauseMenu:
                    {
                        bool exit = false;

                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);

                        bool hoveringContinue = MousePosistion(GMenus.Box1.X - (int)camPos.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        bool hoveringAbort = MousePosistion(GMenus.Box2.X - (int)camPos.X, GMenus.Box2.Y, GMenus.Box2.Height, GMenus.Box2.Width);
                        if (hoveringContinue)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.Game;
                            }
                        }
                        if (hoveringAbort)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.ExitGameConfirmMenu;
                            }
                        }

                        if (exit)
                        {
                            CurrentState = GameState.Game;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

                #region Exit Game Conform Menu
                case GameState.ExitGameConfirmMenu:
                    {
                        bool exit = false;

                        Keys escape = Keys.Back;

                        exit = SingleKeyPress(escape);

                        bool hoveringYes = MousePosistion(GMenus.Box1.X - (int)camPos.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        bool hoveringNo = MousePosistion(GMenus.Box2.X - (int)camPos.X, GMenus.Box2.Y, GMenus.Box2.Height, GMenus.Box2.Width);
                        if (hoveringYes)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                camPos = new Vector2(0, 0);
                                CurrentState = GameState.WorldMapMenu;
                            }
                        }
                        if (hoveringNo)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.PauseMenu;
                            }
                        }

                        if (exit)
                        {
                            CurrentState = GameState.ExitGameConfirmMenu;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

                #region Mission Success Menu
                case GameState.MissionSuccessMenu:
                    {
                        bool cont = false;

                        Keys enter = Keys.Enter;

                        cont = SingleKeyPress(enter);

                        bool hoveringContinue = MousePosistion(GMenus.Box1.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        if (hoveringContinue)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.Game;
                            }
                        }

                        if (cont)
                        {
                            CurrentState = GameState.WorldMapMenu;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

                #region Mission Failure

                case GameState.MissionFailure:
                    {
                        bool cont = false;

                        Keys enter = Keys.Enter;

                        cont = SingleKeyPress(enter);

                        bool hoveringContinue = MousePosistion(GMenus.Box1.X, GMenus.Box1.Y, GMenus.Box1.Height, GMenus.Box1.Width);
                        if (hoveringContinue)
                        {
                            if (mstate.LeftButton == ButtonState.Pressed)
                            {
                                CurrentState = GameState.Game;
                            }
                        }

                        if (cont)
                        {
                            CurrentState = GameState.WorldMapMenu;
                        }
                        previouskbstate = kbstate;
                        break;
                    }
                #endregion

            }
            previouskbstate = kbstate;
            previousmState = mstate;

            base.Update(gameTime);

        }


        /// <summary>
        /// This method should build a basic level for now
        /// </summary>
        private void textTile()
        {

            int XX = 0;

            int YY = 0;

            tileClass t1;

            rects.Clear();

            try
            {

                level = new StreamReader("Content/levelText.txt");

                string line = null;

                int count = File.ReadLines("Content/levelText.txt").Count();

                YY = GraphicsDevice.Viewport.Height - (count * 50);

                while ((line = level.ReadLine()) != null)
                {

                    XX = 0;

                    int length = line.Length;

                    for (int i = 0; i < length; i++)
                    {

                        //int tileRNG = rng.Next(1, 4);

                        if (line[i] == 'A')
                        {

                            t1 = new tileClass(XX, YY, 50, 50, tileA);

                            rects.Add(t1);
                        }
                        if (line[i] == 'B')
                        {
                            if(spawnCount == 1)
                            {
                                enemy1 = new Character(XX, YY, 100, 100, 1, 1, 1);
                            }
                            else if(spawnCount == 2)
                            {
                                enemy2 = new Character(XX, YY, 100, 100, 1, 1, 1);
                            }

                            spawnCount++;
                        }
                        if (line[i] == 'C')
                        {

                            

                        }

                        XX += 50;

                    }

                    YY += 50;

                }

                level.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine("File is icompatible; Loop may not work");
                Console.WriteLine(e.Message);

            }

        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            switch (CurrentState)
            {
                #region StartMenu

                case GameState.StartMenu:
                    {
                        spriteBatch.Begin();
                        framesM -= 1;
                        if (framesM == 0)
                        {    
                            if(letter < title.Length - 1)
                            {
                                GMenus.TitleMenu(spriteBatch, text, priceText, frame, end, title[letter]);
                                framesM = 10;
                                letter += 1;
                            }
                            else
                            {
                                framesM = 50;
                                end = true;
                            }
                            
                        }
                        GMenus.TitleMenu(spriteBatch, text, priceText, framesM, end, title[letter]);
                        spriteBatch.End();
                        break;
                    }
#endregion

                #region World Map Menu

                case GameState.WorldMapMenu:
                    {
                        spriteBatch.Begin();

                        if (openClose)
                        {
                            if (GMenus.Box4.X != 0)
                            {
                                GMenus.Box3 = new Rectangle(GMenus.Box3.X + 2, GMenus.Box3.Y, GMenus.Box3.Width, GMenus.Box3.Height);
                                GMenus.Box4 = new Rectangle(GMenus.Box4.X + 2, GMenus.Box4.Y, GMenus.Box4.Width, GMenus.Box4.Height);
                            }
                        }
                        else
                        {
                            if (GMenus.Box3.X != 0)
                            {
                                GMenus.Box3 = new Rectangle(GMenus.Box3.X - 2, GMenus.Box3.Y, GMenus.Box3.Width, GMenus.Box3.Height);
                                GMenus.Box4 = new Rectangle(GMenus.Box4.X - 2, GMenus.Box4.Y, GMenus.Box4.Width, GMenus.Box4.Height);
                            }

                        }
                        GMenus.WorldMap(spriteBatch, highLight, openClose, gstats, priceText);

                        //Wanted to make clouds but mono games is a douche

                        //Cloud1 = new Rectangle(Cloud1.X - 1, Cloud1.Y, Cloud1.Width, Cloud1.Height);
                        //Cloud2 = new Rectangle(Cloud2.X - 1, Cloud2.Y, Cloud2.Width, Cloud2.Height);
                        //Cloud3 = new Rectangle(Cloud3.X - 1, Cloud3.Y, Cloud3.Width, Cloud3.Height);
                        //Cloud4 = new Rectangle(Cloud4.X - 1, Cloud4.Y, Cloud4.Width, Cloud4.Height);

                        //spriteBatch.Draw(cloud1, Cloud1, Color.White);
                        //spriteBatch.Draw(cloud2, Cloud2, Color.Transparent);
                        //spriteBatch.Draw(cloud3, Cloud3, Color.Transparent);
                        //spriteBatch.Draw(cloud4, Cloud4, Color.Transparent);



                        if (misGen == 0)
                        {
                            mis = null;
                            mis = new Mission[numOfMis];

                            for (int i = 0; i < mis.Length; i++)
                            {
                                int region = rng.Next(0, 101);
                                Rectangle rec;
                                if (region < 35)
                                {
                                    if (region < 17)
                                    {
                                        rec = new Rectangle(rng.Next(160, 251), rng.Next(100, 201), 0, 0);
                                    }
                                    else
                                    {
                                        rec = new Rectangle(rng.Next(251, 300), rng.Next(200, 301), 0, 0);
                                    }

                                }
                                else
                                {
                                    if (region < 65)
                                    {
                                        rec = new Rectangle(rng.Next(400, 500), rng.Next(100, 301), 0, 0);
                                    }
                                    else if (region > 89)
                                    {
                                        rec = new Rectangle(rng.Next(700, 740), rng.Next(280, 331), 0, 0);
                                    }
                                    else
                                    {
                                        rec = new Rectangle(rng.Next(500, 611), rng.Next(100, 201), 0, 0);
                                    }
                                    //rec = new Rectangle(rng.Next(400, 611), rng.Next(100, 201), 0, 0);
                                }


                                int lv = rng.Next(0, 3);
                                int itm = rng.Next(0, items[lv].Count);


                                mis[i] = new Mission(items[lv].ElementAt<Item>(itm));
                                mis[i].RandomRec(rec);


                            }
                            for (int i = 0; i < mis.Length; i++)
                            {
                                int ran = rng.Next(1, 6);
                                mis[i].MissionItem.RandomDesc(ran);
                            }
                            misGen += 1;
                        }

                        for (int i = 0; i < mis.Length; i++)
                        {
                            if (time == 0)
                            {
                                if (mis[i].Rec.Height < 20 && mis[i].Rec.Width < 20)
                                {
                                    mis[i].Rec = new Rectangle(mis[i].Rec.X - 1, mis[i].Rec.Y - 1, mis[i].Rec.Height + 1, mis[i].Rec.Width + 1);

                                }
                                time = 4;
                            }
                            else
                            {
                                time -= 1;
                            }

                            if (i == colorSwitch)
                            {
                                highLight = Color.Turquoise;
                                GMenus.Mission(spriteBatch, mis[i].Rec, i, highLight);
                            }
                            else
                            {
                                highLight = Color.White;
                                GMenus.Mission(spriteBatch, mis[i].Rec, i, highLight);
                            }

                        }

                        spriteBatch.End();

                        break;
                    }
                #endregion

                #region Exit World Map Menu

                case GameState.ExitWorldMapMenu:
                    {
                        spriteBatch.Begin();

                        GMenus.WorldMap(spriteBatch, highLight, openClose, gstats, priceText);
                        GMenus.ConfirmMenu(spriteBatch, priceText, camPos);

                        spriteBatch.End();

                        break;
                    }
                #endregion

                #region Mission Menu

                case GameState.MissionMenu:
                    {
                        spriteBatch.Begin();

                        GMenus.WorldMap(spriteBatch, highLight, openClose, gstats, priceText);
                        GMenus.ContractMenu(spriteBatch, mis[misNum], text, desText, priceText);

                        spriteBatch.End();

                        break;
                    }
                #endregion

                #region Game

                case GameState.Game:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

                        // Draws for the main game
                        foreach (tileClass col in rects) //draws each collectible that is present in the array
                        {

                            col.GameObjectDraw(spriteBatch);

                        }
                        GMenus.Game(spriteBatch, p1.Health, p1.Stamina, camPos);
                        

                        //spriteBatch.Draw(p1.CurrentTexture, p1.box, Color.White);

                        switch (currentState) //flips sprite accoring to state

                        {

                            case WalkState.FaceLeft:

                                DrawCharacterStanding(SpriteEffects.FlipHorizontally);

                                break;

                            case WalkState.FaceRight:

                                DrawCharacterStanding(SpriteEffects.None);

                                break;

                            case WalkState.WalkRight:

                                DrawCharacterWalking(SpriteEffects.None);

                                break;

                            case WalkState.WalkLeft:

                                DrawCharacterWalking(SpriteEffects.FlipHorizontally);

                                break;

                        }

                        enemy1.Draw(spriteBatch);
                        enemy2.Draw(spriteBatch);
                        /*
                        if (enemy2.Flip == 1)
                        {
                            DrawEnemyWalking(SpriteEffects.None);
                        }
                        else if (enemy2.Flip == 0)
                        {
                            DrawEnemyWalking(SpriteEffects.FlipHorizontally);
                        }*/
                        

                        spriteBatch.End();
                        break;
                    }
                #endregion

                #region Pause Menu

                case GameState.PauseMenu:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

                        enemy1.Draw(spriteBatch);
                        enemy2.Draw(spriteBatch);
                        spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(0, rectOffset, rectWidth, rectHeight), Color.White);
                        GraphicsDevice.Clear(Color.Black);
                        foreach (tileClass col in rects) //draws each collectible that is present in the array
                        {

                            col.GameObjectDraw(spriteBatch);

                        }
                        spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(0, rectOffset, rectWidth, rectHeight), Color.White);
                        GMenus.GamePauseMenu(spriteBatch, priceText, camPos);

                        spriteBatch.End();

                        break;
                    }
                #endregion

                #region Exit Game Confirm Menu

                case GameState.ExitGameConfirmMenu:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

                        enemy1.Draw(spriteBatch);
                        enemy2.Draw(spriteBatch);
                        spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(0, rectOffset, rectWidth, rectHeight), Color.White);
                        GraphicsDevice.Clear(Color.Black);
                        foreach (tileClass col in rects) //draws each collectible that is present in the array
                        {

                            col.GameObjectDraw(spriteBatch);

                        }
                        spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(0, rectOffset, rectWidth, rectHeight), Color.White);
                        GMenus.ConfirmMenu(spriteBatch, priceText, camPos);

                        spriteBatch.End();

                        break;
                    }
                #endregion

                #region Mission Success Menu

                case GameState.MissionSuccessMenu:
                    {
                        spriteBatch.Begin();

                        GMenus.MissionSuccess(spriteBatch);

                        spriteBatch.End();
                        break;
                    }
                #endregion

                #region Mission Fail
                case GameState.MissionFailure:
                    {
                        spriteBatch.Begin();

                        GMenus.MissionFail(spriteBatch);

                        spriteBatch.End();
                        break;
                    }
                #endregion

            }        

            base.Draw(gameTime);
        }

        /// <summary>
        /// This should update the animation based on
        /// how many frames have passed
        /// </summary>
        /// <param name="gameTime">getting the in game frames</param>
        private void UpdateAnimation(GameTime gameTime)
        {

            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounter >= timePerFrame)
            {

                frame += 1;

                if (isJumping == true)
                {

                    frame = 1;

                }

                if (frame > walkFrameCount)

                    frame = 1;

                timeCounter -= timePerFrame;

            }

        }


        /// <summary>
        /// Draws the main character standing
        /// </summary>
        /// <param name="flipSprite">used to flip the sprite the opposite direction</param>
        private void DrawCharacterStanding(SpriteEffects flipSprite)
        {

            spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(0, rectOffset, rectWidth, rectHeight), Color.White, 0, Vector2.Zero, 1.0f, flipSprite, 0);

        }


        /// <summary>
        /// Draws the main character walking
        /// </summary>
        /// <param name="flipSprite">used to flip the sprite the opposite direction</param>
        private void DrawCharacterWalking(SpriteEffects flipSprite)
        {

            spriteBatch.Draw(spriteSheet, playerLOC, new Rectangle(frame * rectWidth, rectOffset, rectWidth, rectHeight), Color.White, 0, Vector2.Zero, 1.0f, flipSprite, 0);

        }


        /// <summary>
        /// Draws the enemy walking
        /// </summary>
        /// <param name="flipSprite">used to flip the sprite the opposite direction</param>
        private void DrawEnemyWalking(SpriteEffects flipSprite)
        {

            spriteBatch.Draw(spriteSheet, new Vector2 (enemy2.Box.X, enemy2.Box.Y), new Rectangle(frame * rectWidth, 125, rectWidth, rectHeight), Color.White, 0, Vector2.Zero, 1.0f, flipSprite, 0);

        }



        /// <summary>
        /// Helper method
        /// moves the player if keys are pressed
        /// </summary>
        public void MovePlayer()
        {
            // Sets Previous Position for collision purposes
            p1.PrevBox = p1.Box;

            #region Sprinting
            // Handles spriting (if shift is held, speed is increased)
            if (kbstate.IsKeyDown(Keys.LeftShift) || kbstate.IsKeyDown(Keys.RightShift))
            {
                if (p1.Stamina > 0)
                {
                    p1.Sprint++;
                    if (p1.Sprint >= 5)
                    {
                        p1.Sprint = 5;
                    }
                    p1.Stamina--;
                }
                else
                {
                    p1.Sprint--;
                    if (p1.Sprint <= 0)
                    {
                        p1.Sprint = 0;
                        p1.Stamina++;
                        if (p1.Stamina >= 200)
                        {
                            p1.Stamina = 200;
                        }
                    }
                }
            }
            else
            {
                p1.Sprint--;
                if (p1.Sprint <= 0)
                {
                    p1.Sprint = 0;
                    p1.Stamina++;
                    if (p1.Stamina >= 200)
                    {
                        p1.Stamina = 200;
                    }
                }
            }
            #endregion

            // Handles moving left and right as well as jumping

            if (kbstate.IsKeyDown(Keys.W))
            {
                isJumping = true;
                p1.IsJumping = true;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                p1.Left(9);
                p1.CollideX(tileRect);
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                p1.Right(9);
                p1.CollideX(tileRect);
            }

            // Friction

            #region Friction
            if (kbstate.IsKeyUp(Keys.D) && kbstate.IsKeyUp(Keys.A) && p1.RunRight == true)
            {
                p1.Runspeed--;
                if (p1.Runspeed <= 0)
                {
                    p1.Runspeed = 0;
                    p1.RunRight = false;
                }
                p1.X += p1.Runspeed + Sprint;
            }
            if (kbstate.IsKeyUp(Keys.A) && kbstate.IsKeyUp(Keys.D) && p1.RunLeft == true)
            {
                p1.Runspeed--;
                if (p1.Runspeed <= 0)
                {
                    p1.Runspeed = 0;
                    p1.RunLeft = false;
                }
                p1.X -= p1.Runspeed + Sprint;
            }
            #endregion

            playerLOC.X = p1.X;

        }


        /// <summary>
        /// Creates gravitiy so character stays grounded
        /// </summary>
        /// <param name="c">Inputs a player character</param>
        public void Gravity(Character c)
        {
            // Only for player
            if (p1.IsJumping == true)
            {
                isJumping = true;
            }
            if (p1.IsJumping == false)
            {
                isJumping = false;
            }

            c.fall();
            c.Jump(13);
            c.CollideY(tileRect);

            // Only for player
            playerLOC.Y = p1.Y;

        }


        /// <summary>
        /// Helper Method
        /// Checks if a key is pressed
        /// </summary>
        /// <param name="letter">The key that you want to check</param>
        /// <returns>Returns true if the key asked for is being pressed</returns>
        public bool SingleKeyPress(Keys letter)
        {
            // Checks if the key is being pressed
            if (kbstate.IsKeyDown(letter))
            {
                // Checks if the last key the was pressed was the same key being checked
                if (previouskbstate.IsKeyUp(letter))
                {
                    return true;
                }
            }
            return false;

        }


        /// <summary>
        /// Helper Method
        /// Checks where the mouse cusor is on the screen 
        /// </summary>
        /// <param name="x">the x mouse position</param>
        /// <param name="y">the y mouse position</param>
        /// <param name="boxHeight">the box height region</param>
        /// <param name="boxWitdh">the box witdh region</param>
        /// <returns>returns true if the mouse is hovering over the box region</returns>
        public bool MousePosistion(int x, int y, int boxHeight, int boxWitdh)
        {
            if (mstate.Position.Y > y && mstate.Position.Y < (y + boxHeight) && mstate.Position.X > x && mstate.Position.X < (x + boxWitdh))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Handles a very basic enemy AI
        /// </summary>
        /// <param name="c">Should input a enemy character</param>
        public void EnemyAI(Character c)
        {
            c.PrevBox = c.Box;

            if(c.Flip == 0)
            {
                c.Left(5);
                c.CheckFall(tileRect);
                if (c.IsCollidingX(tileRect))
                {
                    c.CollideX(tileRect);
                    c.Flip = 1;
                }
            }
            if (c.Flip == 1)
            {
                c.Right(5);
                c.CheckFall(tileRect);
                if (c.IsCollidingX(tileRect))
                {
                    c.CollideX(tileRect);
                    c.Flip = 0;
                }
            }
        }
    } 
}
