using System;
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
        KeyboardState previousKbState;
        Texture2D testImage;
        Player1 p1;
        GameStats gstats;

        StreamReader level;

        Texture2D tileA;
        Texture2D tileB;
        Texture2D tileC;
        Random rng = new Random();

        List<tileClass> rects; //a list of collectibles

        // Enums
        GameState CurrentState = GameState.StartMenu;
        CharacterState CharState = CharacterState.FaceRight;

        // basic physics values (Remove tempGround when we have solid ground blocks)
        int tempGround = 360;
        bool isJumping = false;
        bool isOnGround = true;
        int gravity = 0;
        int Runspeed = 0;
        int Sprint = 0;
        int endurance = 200;
        bool RunLeft = false;
        bool RunRight = false;

        // Handles the various states the game will be in
        enum GameState
        {
            StartMenu,
            //ExitTutorialMenu,
            //WorldMapMenu,
            //ExitWorldMapMenu,
            //MissionMenu,
            Game,
            PauseMenu,
            //ExitGameConfirmMenu,
            //MissionSuccessMenu,
            //MissionFailure
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

            rects = new List<tileClass>();

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
            p1 = new Player1(100, 360, 100, 100, 3, 2, 1);
            gstats = new GameStats(0, 0, 0, 0.0, 0.0);
            testImage = Content.Load<Texture2D>("Pizza.png");
            p1.CurrentTexture = testImage;

            tileA = Content.Load<Texture2D>("tileA.png");
            tileB = Content.Load<Texture2D>("tileB.png");
            tileC = Content.Load<Texture2D>("tileC.png");

            textTile();

            // TODO: use this.Content to load your game content here
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

            // Getting keyboard state
            previousKbState = kbstate;
            kbstate = Keyboard.GetState();

            switch (CurrentState)
            {
                case GameState.StartMenu:

                    // Goes to the main game after pressing enter
                    if(kbstate.IsKeyDown(Keys.Enter))
                    {
                        CurrentState = GameState.Game;
                    }
                    break;

                case GameState.Game:

                    // Moves the player
                    MovePlayer();

                    // Handles jumping and gravity for the player
                    PlayerGravity();

                    break;

                case GameState.PauseMenu:

                    break;

            }

            base.Update(gameTime);

        }
        private void textTile()
        {

            int XX = 0;

            int YY = 0;

            tileClass t1;
            tileClass t2;
            tileClass t3;

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

                            t2 = new tileClass(XX, YY, 50, 50, tileB);

                            rects.Add(t2);

                        }
                        if (line[i] == 'C')
                        {

                            t3 = new tileClass(XX, YY, 50, 50, tileC);

                            rects.Add(t3);

                        }

                        XX += 50;

                    }

                    YY += 50;

                }

                level.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine("File is icompatible; Loop may be absolute shite");
                Console.WriteLine(e.Message);

            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Testing player draw
            spriteBatch.Begin();

            switch(CurrentState)
            {
                case GameState.StartMenu:

                    break;

                case GameState.Game:

                    // Draws for the main game
                    foreach (tileClass col in rects) //draws each collectible that is present in the array
                    {

                        col.GameObjectDraw(spriteBatch);

                    }

                    spriteBatch.Draw(p1.CurrentTexture, p1.box, Color.White);

                    break;

                case GameState.PauseMenu:

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Handles basic movement (Left/Right, Sprinting, Jumping)
        // Also handles momentum and endurance for sprinting

        public void MovePlayer()
        {
            // Handles spriting (if shift is held, speed is increased)
            if (kbstate.IsKeyDown(Keys.LeftShift) || kbstate.IsKeyDown(Keys.RightShift))
            {
                if (endurance > 0)
                {
                    Sprint++;
                    if (Sprint >= 6)
                    {
                        Sprint = 6;
                    }
                    endurance--;
                }
                else
                {
                    Sprint--;
                    if (Sprint <= 0)
                    {
                        Sprint = 0;
                        endurance++;
                        if (endurance >= 200)
                        {
                            endurance = 200;
                        }
                    }
                }
            }
            else
            {
                Sprint--;
                if (Sprint <= 0)
                {
                    Sprint = 0;
                    endurance++;
                    if (endurance >= 200)
                    {
                        endurance = 200;
                    }
                }
            }

            // Handles moving left and right as well as jumping

            if (kbstate.IsKeyDown(Keys.W))
            {
                isJumping = true;
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                RunLeft = true;
                RunRight = false;
                Runspeed++;
                if (Runspeed >= 7)
                {
                    Runspeed = 7;
                }
                p1.X -= Runspeed + Sprint;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                RunRight = true;
                RunLeft = false;
                Runspeed++;
                if (Runspeed >= 7)
                {
                    Runspeed = 7;
                }
                p1.X += Runspeed + Sprint;
            }

            // Checks to see if the player has stopped moving
            // if so they are slowed down over time

            if (kbstate.IsKeyUp(Keys.D) && kbstate.IsKeyUp(Keys.A) && RunRight == true)
            {
                Runspeed--;
                if (Runspeed <= 0)
                {
                    Runspeed = 0;
                    RunRight = false;
                }
                p1.X += Runspeed + Sprint;
            }
            if (kbstate.IsKeyUp(Keys.A) && kbstate.IsKeyUp(Keys.D) && RunLeft == true)
            {
                Runspeed--;
                if (Runspeed <= 0)
                {
                    Runspeed = 0;
                    RunLeft = false;
                }
                p1.X -= Runspeed + Sprint;
            }
        }

        public void PlayerGravity()
        {
            if (isJumping == true)
            {
                p1.Y -= 13 - (gravity / 2);
                gravity++;
                if (p1.Y >= tempGround)
                {
                    isJumping = false;
                    gravity = 0;
                }
            }
            if (isOnGround == false)
            {
                p1.Y -= 0 - (gravity / 2);
                gravity++;
                if (p1.Y >= tempGround)
                {
                    isOnGround = true;
                    gravity = 0;
                }
            }
        }

        }
    }
