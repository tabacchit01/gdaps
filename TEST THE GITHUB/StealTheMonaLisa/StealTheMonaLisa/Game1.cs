using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace StealTheMonaLisa
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState kbstate;
        Texture2D testImage;
        Player1 p1;
        GameState CurrentState;
        GameStats gstats;

        // basic physics values
        bool isMidair = false;
        int gravity = 1;
        int startingY = 0;



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
            kbstate = Keyboard.GetState();

            // Testing player update, veritcal direction does not work if mid-air
            MovePlayer();

            // Tests for jumping (Player jump returns the starting Y value)
            if (isMidair == false && kbstate.IsKeyDown(Keys.Space))
            {

            }
            else if (isMidair == true)
            {

            }

            base.Update(gameTime);
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
            spriteBatch.Draw(p1.CurrentTexture, p1.box, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void MovePlayer()
        {
            if(isMidair == false)
            {
                if (kbstate.IsKeyDown(Keys.W))
                {
                    p1.Y -= 4;
                }
                if (kbstate.IsKeyDown(Keys.S))
                {
                    p1.Y += 4;
                }
            }
            if (kbstate.IsKeyDown(Keys.A))
            {
                p1.X -= 4;
            }
            if (kbstate.IsKeyDown(Keys.D))
            {
                p1.X += 4;
            }

        }

    }
}
