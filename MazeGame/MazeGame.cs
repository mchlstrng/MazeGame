using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class MazeGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Maze maze;
        private Player player;
        private InputManager inputManager;

        public MazeGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;  // set this to the desired width
            _graphics.PreferredBackBufferHeight = 600; // set this to the desired height
        }

        protected override void Initialize()
        {
            maze = new Maze(10, 10);
            var playerPosition = new Vector2(1.5f, 1.5f);
            player = new Player(playerPosition);
            inputManager = new InputManager();

            maze.GenerateMaze();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            maze.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            maze.Draw(_spriteBatch, player);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
