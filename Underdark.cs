using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Underdark
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Underdark : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        protected Actor playerActor;
        private InputHandler inputHandler;
        private readonly int SCREENBASEHEIGHT = 640;
        private readonly int SCREENBASEWIDTH = 1024;
        private TileMap tileMap;

        private Camera2D camera;
       

        public Underdark() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = SCREENBASEHEIGHT;
            graphics.PreferredBackBufferWidth = SCREENBASEWIDTH;
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
            // TODO: Add your initialization logic here
            playerActor = new Player();
            inputHandler = new InputHandler();
            tileMap = new TileMap();
            camera = new Camera2D(GraphicsDevice.Viewport);
            camera.Initialize();
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

            Vector2 playerStartingPosition = new Vector2(128 * 32 / 2, 128 * 32 / 2);

            playerActor.Initialize(Content.Load<Texture2D>("characterBase"), playerStartingPosition);
            camera.Focus = (IFocusable)playerActor;
            tileMap.Initialize(loadGameTiles());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            ICommand command = inputHandler.handleInput();

            if (command != null)
            {
                command.execute(playerActor);
            }

            camera.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,null,null,null,null,camera.Transform);
            tileMap.Draw(spriteBatch);

            playerActor.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Dictionary<int, Texture2D> loadGameTiles()
        {
            Dictionary<int,Texture2D> gameTiles = new Dictionary<int,Texture2D>();
            gameTiles.Add(Tile.Black, Content.Load<Texture2D>("tiles\\black"));
            gameTiles.Add(Tile.White, Content.Load<Texture2D>("tiles\\white"));

            return gameTiles;
        }
    }
}
