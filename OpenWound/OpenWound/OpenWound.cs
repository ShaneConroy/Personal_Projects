using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenWound
{
    public class OpenWound : Game
    {
        Texture2D knightTexture;
        Texture2D cinemaTexture;
        Texture2D mallTexture;
        Texture2D mainMenuTexture;
        Texture2D startButtonTexture;
        Texture2D quitButtonTexture;

        Rectangle startButton;
        Rectangle quitButton;

        Vector2 knightPos;
        float knightSpeed;

        int deadZone = 4096; // Joystick dead zone threshold

        bool isGame = false;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public OpenWound()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent() // Called once per game, within the Initialize method, before the main game loop starts.
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            knightTexture = Content.Load<Texture2D>("tempKnight");
            mallTexture = Content.Load<Texture2D>("mallOverworld");
            cinemaTexture = Content.Load<Texture2D>("cinemaOverworld");
            mainMenuTexture = Content.Load<Texture2D>("tempMainScreen");
            startButtonTexture = Content.Load<Texture2D>("startButton");
            quitButtonTexture = Content.Load<Texture2D>("startButton");

            startButton = new Rectangle((_graphics.PreferredBackBufferWidth - startButtonTexture.Width) / 2, (_graphics.PreferredBackBufferHeight / 2) - 150, startButtonTexture.Width, startButtonTexture.Height);
            quitButton = new Rectangle((_graphics.PreferredBackBufferWidth - quitButtonTexture.Width) / 2, (_graphics.PreferredBackBufferHeight / 2) + 100, quitButtonTexture.Width, quitButtonTexture.Height);
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.IsFullScreen = false;

            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true;

            knightPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            knightSpeed = 500f;
            

            _graphics.ApplyChanges();
            base.Initialize();
        }

        // Game Loop
        protected override void Update(GameTime gameTime)
        {
            // Exiting game 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Before games started
            if(!isGame)
            {
                // Check if the mouse is inside start button, tehn check if clicked
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    var mousePos = Mouse.GetState().Position;
                    if (startButton.Contains(mousePos))
                    {
                        isGame = true; // Start the game
                    }
                    if( quitButton.Contains(mousePos))
                    {
                        Exit(); // Exit the game
                    }
                }
            }
            else // Game's main update loop
            {
                float updatedKnightSpeed = knightSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Keyboard Movement
                var kstate = Keyboard.GetState();
                if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
                    knightPos.Y -= updatedKnightSpeed;
                if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
                    knightPos.Y += updatedKnightSpeed;
                if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
                    knightPos.X -= updatedKnightSpeed;
                if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
                    knightPos.X += updatedKnightSpeed;

                // Controller Movement
                if (Joystick.LastConnectedIndex == 0)
                {
                    JoystickState jstate = Joystick.GetState((int)PlayerIndex.One);

                    if (jstate.Axes[1] < -deadZone)
                        knightPos.Y -= updatedKnightSpeed;
                    else if (jstate.Axes[1] > deadZone)
                        knightPos.Y += updatedKnightSpeed;
                    if (jstate.Axes[0] < -deadZone)
                        knightPos.X -= updatedKnightSpeed;
                    else if (jstate.Axes[0] > deadZone)
                        knightPos.X += updatedKnightSpeed;
                }

                // Boundary Check
                if (knightPos.X > _graphics.PreferredBackBufferWidth - knightTexture.Width / 2)
                    knightPos.X = _graphics.PreferredBackBufferWidth - knightTexture.Width / 2;
                else if (knightPos.X < knightTexture.Width / 2)
                    knightPos.X = knightTexture.Width / 2;
                if (knightPos.Y > _graphics.PreferredBackBufferHeight - knightTexture.Height / 2)
                    knightPos.Y = _graphics.PreferredBackBufferHeight - knightTexture.Height / 2;
                else if (knightPos.Y < knightTexture.Height / 2)
                    knightPos.Y = knightTexture.Height / 2;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Main Menu's draw call
            if (!isGame)
            {
                _spriteBatch.Draw(mainMenuTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(startButtonTexture, startButton, Color.White);
                _spriteBatch.Draw(quitButtonTexture, quitButton, Color.White);
            }
            else // Main Game's draw call
            {
                

                _spriteBatch.Draw(knightTexture,                                                    // texture
                                  knightPos,                                                        // position
                                  null,                                                             // rect - for spritesheets
                                  Color.White,                                                      // tint
                                  0f,                                                               // rotation
                                  new Vector2(knightTexture.Width / 2, knightTexture.Height / 2),   // origin
                                  Vector2.One,                                                      // scale
                                  SpriteEffects.None,                                               // effects
                                  0f);                                                              // layer



            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        // Will load the overworld map everytime the player enters the overworld
        void loadOverworld()
        {

        }
        // Will spawn buildings in the overworld
        void spawningBuildings()
        {
            // Randomly select building type
            // Get random position on screen
            // Check if position is possible
            // - Terrain, Other buildings, mountains
            // Check if building in correct terrain
            // - Cinema in cities, Ranger station in forest
            // Spawn building
        }
    }
}
