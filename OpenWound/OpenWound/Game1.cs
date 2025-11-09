using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OpenWound
{
    public class Game1 : Game
    {
        Texture2D knightTexture;
        Vector2 knightPos;
        float knightSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            knightPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            knightSpeed = 50f;

            base.Initialize();
        }

        protected override void LoadContent() // Called once per game, within the Initialize method, before the main game loop starts.
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            knightTexture = Content.Load<Texture2D>("tempKnight");
        }

        protected override void Update(GameTime gameTime)
        {
            // Exiting game 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            float updatedKnightSpeed = knightSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                knightPos.Y -= updatedKnightSpeed;
            if (kstate.IsKeyDown(Keys.Down))
                knightPos.Y += updatedKnightSpeed;
            if (kstate.IsKeyDown(Keys.Left))
                knightPos.X -= updatedKnightSpeed;
            if (kstate.IsKeyDown(Keys.Right))
                knightPos.X += updatedKnightSpeed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(knightTexture,                                                    // texture
                              knightPos,                                                        // position
                              null,                                                             // rect - for spritesheets
                              Color.White,                                                      // tint
                              0f,                                                               // rotation
                              new Vector2(knightTexture.Width / 2, knightTexture.Height / 2),   // origin
                              Vector2.One,                                                      // scale
                              SpriteEffects.None,                                               // effects
                              0f);                                                              // layer
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
